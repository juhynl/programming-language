namespace FMinus

open AST

// Type.infer() must raise this if the input program seems to have a type error.
exception TypeError

// The types available in our F- language.
type Type =
  | Int
  | Bool
  | TyVar of string
  | Func of Type * Type

type TypeEnv = Map<string, Type>

module Type =

  // Convert the given 'Type' to a string.
  let rec toString (typ: Type): string =
    match typ with
    | Int -> "int"
    | Bool -> "bool"
    | TyVar s -> s
    | Func (t1, t2) -> sprintf "(%s) -> (%s)" (toString t1) (toString t2)

  // Return the inferred type of the input program.
  let infer (prog: Program): Type =  
    let tryFind (tyvar: string) (subst: Map<string, Type>) : Type =
      Map.tryFind tyvar subst |> Option.defaultValue (TyVar tyvar)
      
    let numTyVar = ref -1
    let genTyVar (): Type =
      numTyVar := !numTyVar + 1
      TyVar ("t" + string !numTyVar)
    
    let numIorBTyVar = ref -1
    let genIorBTyVar (): Type =
      numIorBTyVar := !numIorBTyVar + 1
      TyVar ("iorb" + string !numIorBTyVar)

    let rec gen (typenv: TypeEnv) (exp: Exp) (typ: Type): list<Type * Type> =
      match exp with
      | Num n -> [(typ, Int)]
      | True | False -> [(typ, Bool)]
      | Var x -> [(typ, tryFind x typenv)]
      | Neg e -> (typ, Int) :: gen typenv e Int
      | Add(e1, e2) | Sub(e1, e2) -> (typ, Int) :: gen typenv e1 Int @ gen typenv e2 Int
      | LessThan(e1, e2) | GreaterThan(e1, e2) -> (typ, Bool) :: gen typenv e1 Int @ gen typenv e2 Int
      | Equal(e1, e2) | NotEq(e1, e2) -> 
        // let newTyVar = genTyVar()
        let newTyVar = genIorBTyVar()
        (typ, Bool) :: gen typenv e1 newTyVar @ gen typenv e2 newTyVar
      | IfThenElse(e1, e2, e3) -> gen typenv e1 Bool @ gen typenv e2 typ @ gen typenv e3 typ
      | LetIn(x, e1, e2) -> 
        let newTyVar = genTyVar()
        let newTypenv = Map.add x newTyVar typenv
        gen typenv e1 newTyVar @ gen newTypenv e2 typ 
      | LetFunIn(f, x, e1, e2) -> 
        let newTyVar1 = genTyVar()
        let newTyVar2 = genTyVar()
        let newTypenv1 = Map.add x newTyVar1 typenv
        let newTypenv2 = Map.add f (Func(newTyVar1, newTyVar2)) typenv
        gen newTypenv1 e1 newTyVar2 @ gen newTypenv2 e2 typ
      | LetRecIn(f, x, e1, e2) -> 
        let newTyVar1 = genTyVar()
        let newTyVar2 = genTyVar()
        let newTypenv1 = Map.add x newTyVar1 typenv |> Map.add f (Func(newTyVar1, newTyVar2))
        let newTypenv2 = Map.add f (Func(newTyVar1, newTyVar2)) typenv
        gen newTypenv1 e1 newTyVar2 @ gen newTypenv2 e2 typ
      | Fun(x, e) -> 
        let newTyVar1 = genTyVar()
        let newTyVar2 = genTyVar()
        let newTypenv = Map.add x newTyVar1 typenv
        (typ, Func(newTyVar1, newTyVar2)) :: gen newTypenv e newTyVar2
      | App(e1, e2) ->
        let newTyVar = genTyVar() in
        gen typenv e1 (Func(newTyVar, typ)) @ gen typenv e2 newTyVar 

    let rec app (subst: Map<string, Type>) (typ: Type): Type =
      match typ with
      | Int | Bool -> typ
      | TyVar s -> tryFind s subst
      | Func (t1, t2) -> Func (app subst t1, app subst t2)   

    let replace (subst: Map<string, Type>) (typvar: string) (typ: Type): Map<string, Type> =
      let newSubst = Map.empty<string, Type> |> Map.add typvar typ
      let replacedSubst =  Map.map (fun tv t -> app newSubst t) subst
      Map.add typvar typ replacedSubst

    let rec unify (t1: Type) (t2: Type) (subst: Map<string, Type>): Map<string, Type> = 
      extend (app subst t1) (app subst t2) subst

    and extend (t1: Type) (t2: Type) (subst: Map<string, Type>): Map<string, Type> =  
      match (t1, t2) with
      | (Int, Int) | (Bool, Bool) -> subst
      | (Func(tx, ty), Func(tx1, ty1)) -> unify ty ty1 (extend tx tx1 subst)
      | (TyVar s, typ) ->
        match typ with
        | TyVar s1 -> if s = s1 then subst else replace subst s typ
        | Int | Bool -> replace subst s typ
        | Func(t1, t2) -> 
          if t1 = TyVar s || t2 = TyVar s then raise TypeError 
          else replace subst s (Func(t1, t2))
      | (typ, TyVar s) -> extend (TyVar s) typ subst
      | _ -> raise TypeError
    
    let rec solveEqns (eqns: list<Type * Type>) (subst: Map<string, Type>): Map<string, Type> =
      // List.iter (fun (t1, t2) -> printfn "(%A, %A)" t1 t2) (Map.toList subst)
      // printfn "====\n"
      match eqns with
      | [] -> subst
      | (t1, t2) :: tail -> solveEqns tail (unify t1 t2 subst)
    
    let checkIorBVal (subst: Map<string, Type>) =
      for n in 0 .. !numIorBTyVar do
        // printfn $"{n}"
        let tyvar = "iorb" + string n
        let typ = tryFind tyvar subst
        match typ with
        | Func(_, _) | TyVar _ -> raise TypeError
        | _ -> ()

    let tyvar = genTyVar()
    let eqns = gen Map.empty<string, Type> prog tyvar
    // List.iter (fun (x, y) -> printfn "%A %A" x y) eqns
    let subst = solveEqns eqns Map.empty<string, Type>
    // printfn "checkIorBVal"
    checkIorBVal subst
    app subst tyvar

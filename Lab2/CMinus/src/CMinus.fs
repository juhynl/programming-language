module CMinus

open AST
open Types

// Evaluate expression into a value, under the given memory.
let rec evalExp (exp: Exp) (mem: Mem) : Val =
  match exp with
  | Num i -> Int i
  | True -> Bool true
  | False -> Bool false
  | Var x -> 
    if Map.containsKey (VarName x) mem then 
      match (Map.tryFind (VarName x) mem).Value with
      | Int i -> Int i
      | Bool b -> Bool b
    else raise UndefinedSemantics
  | Add (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> Int (i1 + i2)
    | _ -> raise UndefinedSemantics
  | Sub (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> Int (i1 - i2)
    | _ -> raise UndefinedSemantics
  | LessThan (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> if (i1 < i2) then Bool true else Bool false
    | _ -> raise UndefinedSemantics
  | GreaterThan (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> if (i1 > i2) then Bool true else Bool false
    | _ -> raise UndefinedSemantics
  | Equal (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> if (i1 = i2) then Bool true else Bool false 
    | (Bool b1, Bool b2) -> if (b1 = b2) then Bool true else Bool false 
    | _ -> raise UndefinedSemantics
  | NotEq (e1, e2) -> 
    match (evalExp e1 mem, evalExp e2 mem) with
    | (Int i1, Int i2) -> if (i1 <> i2) then Bool true else Bool false 
    | (Bool b1, Bool b2) -> if (b1 <> b2) then Bool true else Bool false  
    | _ -> raise UndefinedSemantics

// Note: You may define more functions.

// Execute a statement and return the updated memory.
let rec exec (stmt: Stmt) (mem: Mem) : Mem =
  match stmt with
  | NOP -> mem // NOP does not change the memory.
  | Assign (x, e) -> Map.add (x) (evalExp e mem) mem
  | Seq (s1, s2) -> exec s2 (exec s1 mem)
  | If (e, s1, s2) ->
    match evalExp e mem with
    | Bool true -> exec s1 mem
    | Bool false -> exec s2 mem
    | _ -> raise UndefinedSemantics
  | While (e, s) -> 
    match evalExp e mem with
    | Bool true -> exec stmt (exec s mem)
    | Bool false -> mem
    | _ -> raise UndefinedSemantics

// The program starts execution with an empty memory. Do NOT fix this function.
let run (prog: Program) : Mem =
  exec prog Map.empty

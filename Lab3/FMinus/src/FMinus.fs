module FMinus

open AST
open Types

// Evaluate expression into a value, under the given environment.
let rec evalExp (exp: Exp) (env: Env) : Val =
  match exp with
  | Num i -> Int i
  | True -> Bool true
  | False -> Bool false
  | Add(e1, e2) -> 
    match (evalExp e1 env, evalExp e2 env) with
    | (Int i1, Int i2) -> Int (i1 + i2)
    | _ -> raise UndefinedSemantics
  | Sub(e1, e2) -> 
    match (evalExp e1 env, evalExp e2 env) with
    | (Int i1, Int i2) -> Int (i1 - i2)
    | _ -> raise UndefinedSemantics
  | _ -> raise // TODO: fill in the remaining cases.

// Note: You may define more functions.

// The program starts execution with an empty environment. Do not fix this code.
let run (prog: Program) : Val =
  evalExp prog Map.empty

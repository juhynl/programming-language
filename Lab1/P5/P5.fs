module P5

/// For int list 'l' that contains decimal digits (0~9), return the integer that
/// is represented by this list. For example, "digitsToInt [1; 3; 2]" must
/// return 132 as a result. When the input list is empty, just return 0.
let rec dtoiHelper (n: int) (l: int list): int=
  match l with
  | [] -> n
  | head :: tail -> dtoiHelper (n * 10 + head) tail

let rec digitsToInt (l: int list) : int =
  dtoiHelper 0 l

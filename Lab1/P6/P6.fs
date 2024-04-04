module P6

/// From list 'l', find the element that appears most frequently in the list,
/// and return how many times it appears. If the input list is empty, return 0.
let rec countMostFrequentHelper (max_count: int) (l: List<'a>) : int =
  match l with 
  | [] -> max_count
  | head :: tail ->
      let count_current_element = List.filter (fun n -> n = head) l |> List.length
      if count_current_element > max_count then countMostFrequentHelper count_current_element tail
      else countMostFrequentHelper max_count tail

let rec countMostFrequent (l: List<'a>) : int =
  countMostFrequentHelper 0 l
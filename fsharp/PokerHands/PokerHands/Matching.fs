module Matching

open Foundations

type MatchDescriptor = 
    {
        IsMatch : bool;
        ValuesInRank : int [];
        ValuesNotInRank : int [] 
    }

let desc n = -n
let self n = n
let descLength (n,l) = -l
let areSameSuit hand = (hand |> Seq.map suitOf |> Seq.distinct |> Seq.length) = 1
let noMatch = { IsMatch = false; ValuesInRank = [||]; ValuesNotInRank = [||] }

let matchStraightFlush hand =
    let values =
        hand
        |> Seq.map valueOf
        |> Seq.sort
        |> Seq.distinct
        |> Array.ofSeq
    // Expect a sorted array of 5 elements where diff between 1st and last is 4
    if values.Length <> 5 || (values.[4] - values.[0]) <> 4 || not (areSameSuit hand) then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = [| values.[4] |];
        ValuesNotInRank = [||]
    }

let matchFourOfAKind hand =
    let matches =
        hand
        |> Seq.map valueOf
        |> Seq.groupBy self
        |> Seq.map (fun (n,s) -> n,(Seq.length s))
        |> Seq.sortBy descLength
        |> Array.ofSeq
    // Expect groups of 4 + 1 values, meaning four of a kind
    if matches.Length <> 2 || snd(matches.[0]) <> 4 then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = [| fst(matches.[0]) |];
        ValuesNotInRank = [| fst(matches.[1]) |]
    }

let matchFullHouse hand =
    let matches = 
        hand
        |> Seq.map valueOf
        |> Seq.groupBy self
        |> Seq.map (fun (n,s) -> n,(Seq.length s))
        |> Seq.sortBy descLength
        |> Array.ofSeq
    // Expect groups of 3 + 2 values, meaning full house
    if matches.Length <> 2 || snd(matches.[0]) <> 3 then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = [| fst(matches.[0]); fst(matches.[1]) |];
        ValuesNotInRank = [||]
    }

let matchFlush hand =
    if not(areSameSuit hand) then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = hand |> Seq.map valueOf |> Seq.sortBy desc |> Array.ofSeq;
        ValuesNotInRank = [||]
    }

let matchStraight hand =
    let values =
        hand
        |> Seq.map valueOf
        |> Seq.sort
        |> Seq.distinct
        |> Array.ofSeq
    // Expect a sorted array of 5 elements where diff between 1st and last is 4
    if values.Length <> 5 || (values.[4] - values.[0]) <> 4 then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = [| values.[4] |];
        ValuesNotInRank = [||]
    }
    
let matchThreeOfAKind hand =
    let matches =
        hand
        |> Seq.map valueOf
        |> Seq.groupBy self
        |> Seq.map (fun (n,s) -> n,(Seq.length s))
        |> Seq.sortBy descLength
        |> Array.ofSeq
    // Expect groups of 3 + 1 + 1 values, meaning three of a kind
    if matches.Length <> 3 || snd(matches.[0]) <> 3 then noMatch
    else
    {
        IsMatch = true;
        ValuesInRank = [| fst(matches.[0]) |];
        ValuesNotInRank = [| fst(matches.[1]); fst(matches.[2]) |] |> Array.sortBy desc
    }

let matchTwoPair hand =
    let matches =
        hand 
        |> Seq.map valueOf 
        |> Seq.groupBy self 
        |> Seq.map (fun (n,s) -> n,(Seq.length s)) 
        |> Seq.sortBy descLength
        |> Array.ofSeq
    // Expect groups of 2 + 2 + 1 values, meaning two pairs
    if matches.Length <> 3 || snd(matches.[0]) <> 2 || snd(matches.[1]) <> 2 then noMatch
    else 
    { 
        IsMatch = true; 
        ValuesInRank = [| fst(matches.[0]); fst(matches.[1]) |] |> Array.sortBy desc; 
        ValuesNotInRank = [| fst(matches.[2]) |] 
    }

let matchOnePair hand =
    let matches =
        hand 
        |> Seq.map valueOf 
        |> Seq.groupBy self 
        |> Seq.map (fun (n,s) -> n,(Seq.length s)) 
        |> Seq.sortBy descLength
        |> Array.ofSeq
    // Expect groups of 2 + 1 + 1 + 1 values, meaning two pairs
    if matches.Length <> 4 || snd(matches.[0]) <> 2 then noMatch
    else 
    { 
        IsMatch = true; 
        ValuesInRank = [| fst(matches.[0]) |];
        ValuesNotInRank = [| fst(matches.[1]); fst(matches.[2]); fst(matches.[3]) |] |> Array.sortBy desc
    }

let matchHighCard hand = 
    {
        IsMatch = true; 
        ValuesInRank = [||]
        ValuesNotInRank = hand |> Seq.map valueOf |> Seq.sortBy desc |> Array.ofSeq;
    }
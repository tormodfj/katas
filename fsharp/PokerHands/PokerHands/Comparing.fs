module Comparing

open Foundations
open Matching

let compareHands hand1 hand2 =

    let matchingFunctions =
        [
            matchStraightFlush
            matchFourOfAKind 
            matchFullHouse
            matchFlush
            matchStraight
            matchThreeOfAKind   
            matchTwoPair
            matchOnePair
            matchHighCard
        ]

    let rec doCompareHands hand1 hand2 functions =
        match functions with
        | [] -> 0 // Tie
        | currentFunction::remainingFunctions ->
            let { IsMatch = isMatch1; ValuesInRank = in1; ValuesNotInRank = notIn1 } = currentFunction(hand1)
            let { IsMatch = isMatch2; ValuesInRank = in2; ValuesNotInRank = notIn2 } = currentFunction(hand2)
            match isMatch1, isMatch2 with
            | true, false -> 1
            | false, true -> -1
            | true, true ->
                let c = compare in1 in2
                if c <> 0 then c
                else compare notIn1 notIn2
            | false, false -> doCompareHands hand1 hand2 remainingFunctions // tail recursive

    doCompareHands hand1 hand2 matchingFunctions
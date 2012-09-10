module PokerLib

open Foundations
open Comparing

let determineWinner (player1Hand:string) (player2Hand:string) =
    let parseCard (cardString:string) =
        let parseSuit suitChar =
            match suitChar with
            | 'H' -> Hearts
            | 'D' -> Diamonds
            | 'C' -> Clubs
            | 'S' -> Spades
            | _ -> failwith "Unknown suit"
        
        if cardString.Length <> 2 then failwith "cardString must be 2 characthers long"
        let suit = parseSuit cardString.[1]
        let charVal (c:char) = int(c) - 48 // get int value of ascii char

        match cardString.[0] with
        | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' -> Value(charVal cardString.[0], suit)
        | 'T' -> Value(10, suit)
        | 'J' -> Jack(suit)
        | 'Q' -> Queen(suit)
        | 'K' -> King(suit)
        | 'A' -> Ace(suit)
        | _ -> failwith "Unknown rank"

    let parseHand (handString:string) =
        let parts = handString.Split(' ')
        if parts.Length <> 5 then failwith "Hands must contain five cards"
        else
            parts |> Array.map parseCard

    let hand1 = parseHand player1Hand
    let hand2 = parseHand player2Hand

    compareHands hand1 hand2
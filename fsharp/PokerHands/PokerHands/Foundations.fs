module Foundations

type Suit = 
    | Hearts 
    | Diamonds 
    | Clubs 
    | Spades

type Card = 
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | Value of int * Suit

let valueOf card =
    match card with
    | Ace(_) -> 14
    | King(_) -> 13
    | Queen(_) -> 12
    | Jack(_) -> 11
    | Value(value, _) -> value

let suitOf card =
    match card with
    | Ace(suit) | King(suit) | Queen(suit) | Jack(suit) | Value(_, suit) -> suit
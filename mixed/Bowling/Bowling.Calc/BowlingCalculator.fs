﻿module BowlingCalculator

let CalculateScore pins =

    let rec CalculateScore pins frame =

        match pins with

        // Strike with determined bonus
        | 10 :: y :: z :: rest -> 10 + y + z + CalculateScore (y :: z :: rest) (frame + 1)

        // Strike -without- determined bonus
        | 10 :: y :: [] -> 0

        // Spare with determined bonus
        | x :: y :: z :: rest when x + y = 10 -> 10 + z + CalculateScore (z :: rest) (frame + 1)

        // Spare -without- determined bonus
        | x :: y :: [] when x + y = 10 -> 0

        // Open frame
        | x :: y :: rest -> x + y + CalculateScore (rest) (frame + 1)

        // Special last frame
        | x :: y :: z :: [] when frame = 10 -> x + y + z

        // Otherwise
        | _ -> 0

    CalculateScore pins 1
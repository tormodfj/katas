module Solver

open Sudoku

let solve board = 

    let getCandidates cellIndex board =
        let candidates = [| 1; 2; 3; 4; 5; 6; 7; 8; 9 |]
        let values cells =
            cells
            |> Array.map Cell.value
            |> Array.choose id
        let isNotContained values candidate =
            values 
            |> Array.exists (fun value -> value = candidate)
            |> not
        let rowValues = board |> Board.getRow cellIndex |> values
        let columnValues = board |> Board.getColumn cellIndex |> values
        let regionValues = board |> Board.getRegion cellIndex |> values
        let validCandidates =
            candidates
            |> Array.filter (isNotContained rowValues)
            |> Array.filter (isNotContained columnValues)
            |> Array.filter (isNotContained regionValues)
        validCandidates

    let hasValue board cellIndex = board |> Board.getCell cellIndex |> Cell.value |> Option.isSome

    let updateBoard cellIndex board value =
        Board.setCellValue cellIndex board value
        board

    let rec solveRec board i =
        if i = (Board.Size * Board.Size) then
            true
        else
            if hasValue board i then
                solveRec board (i+1)
            else
                let candidates = getCandidates i board
                if Array.isEmpty candidates then
                    false
                else
                    let isSuccess = 
                        candidates
                        |> Seq.skipWhile (fun c -> (solveRec (updateBoard i board (Some(c))) (i+1)) = false)
                        |> Seq.isEmpty
                        |> not
                    if not (isSuccess) then updateBoard i board None |> ignore
                    isSuccess

    solveRec board 0 |> ignore
    board
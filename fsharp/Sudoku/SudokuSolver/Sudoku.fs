module Sudoku

open System

module Cell =
    type T = Cell of int option

    let create char =
        let value =
            match char with
            | '.' -> None
            | c -> Some(int c - int '0')
        Cell(value)

    let value (Cell value) = value

    let toChar cell =
        match cell with
        | Cell(Some i) -> char (i + int '0')
        | _ -> '.'

module Board =
    type T = Board of Cell.T array array

    let Size = 9

    let create (input:string) =
        let createRow chars =
            chars
            |> Array.ofSeq
            |> Array.map Cell.create
        let board = 
            input.Split([|'\n';'\r';' '|], StringSplitOptions.RemoveEmptyEntries)
            |> Array.map createRow
        Board(board)

    let value (Board cells) = cells

    let toString board =
        let formatRow row = row |> Array.map Cell.toChar
        let formatBoard board =
            let rows = 
                board
                |> Array.map formatRow
                |> Array.map (fun row -> new String(row))
            String.Join("\n", rows)
        let cells = value board
        formatBoard cells

    let getCell cellIndex board =
        let cells = value board
        let rowIndex = cellIndex / Size
        let columnIndex = cellIndex % Size
        cells.[rowIndex].[columnIndex]

    let setCellValue cellIndex board newValue =
        let cells = value board
        let rowIndex = cellIndex / Size
        let columnIndex = cellIndex % Size
        cells.[rowIndex].[columnIndex] <- Cell.Cell(newValue)

    let getRow cellIndex board = 
        let cells = value board
        let rowIndex = cellIndex / Size
        cells.[rowIndex]

    let getColumn cellIndex board =
        let cells = value board
        let columnIndex = cellIndex % Size
        cells |> Array.map (fun row -> row.[columnIndex])

    let getRegion cellIndex board =
        let cells = value board
        let regionRowIndex = cellIndex / Size / (Size / 3)
        let regionColumnIndex = cellIndex % Size / (Size / 3)
        let skipAndTake thirdIndex array =
            array
            |> Seq.skip (thirdIndex * (Size / 3))
            |> Seq.take (Size / 3)
            |> Array.ofSeq
        cells
        |> skipAndTake regionRowIndex
        |> Array.map (skipAndTake regionColumnIndex)
        |> Array.concat

module Solver =
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

    let solve board = 
        solveRec board 0 |> ignore
        board
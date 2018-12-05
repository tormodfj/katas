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

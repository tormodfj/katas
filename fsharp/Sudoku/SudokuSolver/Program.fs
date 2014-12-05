open Sudoku

[<EntryPoint>]
let main argv = 
    if argv |> Array.isEmpty then failwith "Pass puzzle file as argument"

    let file = argv.[0]

    System.IO.File.ReadAllText(file)
    |> Board.create
    |> Solver.solve
    |> Board.toString
    |> printfn "%s"

    0 // return an integer exit code
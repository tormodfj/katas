module Handlers

open System.IO
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc

open Sudoku

let solve (req: HttpRequest) =
    async {
        use stream = new StreamReader(req.Body)
        let! body = stream.ReadToEndAsync() |> Async.AwaitTask
        
        let solution = 
            body
            |> Board.create
            |> Solver.solve
            |> Board.toString

        return OkObjectResult(solution) :> IActionResult
    }
    |> Async.RunSynchronously
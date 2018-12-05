module Handlers

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc

open LcdDisplay

let display (req: HttpRequest) =
    async {

        let number = req.Query.["number"].ToString()
        let lcd = render number

        return OkObjectResult(lcd) :> IActionResult
    }
    |> Async.RunSynchronously
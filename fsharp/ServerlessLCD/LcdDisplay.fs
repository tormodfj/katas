module LcdDisplay

open LcdFont

let render number =

    let getLine i = 
        number 
        |> Seq.map (fun digit -> (digits.Item digit).[i])

    let result = 
        [0..2]
        |> List.map getLine
        |> List.map (String.concat "")
        |> String.concat "\n"

    result
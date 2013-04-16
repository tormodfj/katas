open System

let getLegalMoves (input:String) =
    
    let board = 
        input.Split([|'\n';'\r';' '|], StringSplitOptions.RemoveEmptyEntries)
        |> Array.map Array.ofSeq

    let me = board.[8].[0]        

    let opponent =
        match me with
        | 'B' -> 'W'
        | 'W' -> 'B'
        | _ -> failwith "invalid player marker"

    let directions =
        [|(-1,-1); (0,-1); (1,-1);
          (-1, 0);         (1, 0);
          (-1, 1); (0, 1); (1, 1)|]

    let isInsideBoard (x, y) = (0 <= x && x < 8) && (0 <= y && y < 8)

    let itemAt (x, y) = board.[y].[x]

    let traverseUntilEdge (x,y) (dx,dy) =
        Seq.initInfinite (fun i -> (x+dx*i, y+dy*i))
        |> Seq.takeWhile isInsideBoard
        |> Array.ofSeq

    let isLegal input =
        let isFreeSpot = Array.length input > 0 && input.[0] = '.'
        let isOpponentAdjacent = Array.length input > 1 && input.[1] = opponent
        let isMeBeforeDotOrEnd = 
            input
            |> Seq.skip 1
            |> Seq.takeWhile (fun c -> c <> '.')
            |> Seq.exists (fun c -> c = me)
        isFreeSpot && isOpponentAdjacent && isMeBeforeDotOrEnd

    let isLegalMove (x,y) =
        let getChars coords =
            coords
            |> Seq.map itemAt
            |> Array.ofSeq
        directions
        |> Seq.ofArray
        |> Seq.map (fun (dx, dy) -> traverseUntilEdge (x,y) (dx,dy))
        |> Seq.map getChars
        |> Seq.exists isLegal

    let getMarker (x,y,legal) =
        match legal with
        | true -> '0'
        | false -> itemAt (x,y)
        
    let legalMovesOnRow row =
        [|0..7|]
        |> Array.map (fun col -> (row, col, isLegalMove(row, col)))
        |> Array.map getMarker

    let legalMovesOnBoard =
        [|0..7|]
        |> Array.map legalMovesOnRow

    legalMovesOnBoard
    |> Seq.map (fun line -> new String(line))
    |> String.concat "\n"

// Let's try it
let board = "
    ........
    ........
    ....WW..
    ..BBWW..
    ...BB...
    ...WBB..
    ........
    ........
    B"

board
|> getLegalMoves 
|> printf "%s"
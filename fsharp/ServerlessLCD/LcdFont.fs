module LcdFont

let digits =

    let one = 
        [|
        "   ";
        "  |";
        "  |"
        |]
    let two =
        [|
        " _ ";
        " _|";
        "|_ "
        |]
    let three =
        [|
        " _ ";
        " _|";
        " _|"
        |]    
    let four =
        [|
        "   ";
        "|_|";
        "  |"
        |]
    let five =
        [|
        " _ ";
        "|_ ";
        " _|"
        |]   
    let six =
        [|
        " _ ";
        "|_ ";
        "|_|"
        |]   
    let seven =
        [|
        " _ ";
        "  |";
        "  |"
        |]          
    let eight =
        [|
        " _ ";
        "|_|";
        "|_|"
        |]
    let nine =
        [|
        " _ ";
        "|_|";
        " _|"
        |]
    let zero =
        [|
        " _ ";
        "| |";
        "|_|"
        |]

    [
        '1', one;
        '2', two;
        '3', three;
        '4', four;
        '5', five;
        '6', six;
        '7', seven;
        '8', eight;
        '9', nine;
        '0', zero;
    ]    
    |> Map.ofList

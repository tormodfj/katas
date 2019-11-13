use std::env;
use std::fs;

fn main() {
    let args: Vec<String> = env::args().skip(1).collect();
    let contents = fs::read_to_string(&args[0]).expect("Shit blew up");
    let iter_of_row_strings = contents.split("\n");
    let board: Vec<Vec<Option<u32>>> = iter_of_row_strings.map(get_row_values).collect();
    println!("{:?}", board);
}

fn get_row_values(r:&str) -> Vec<Option<u32>> {
    return r.chars().map(get_char_value).collect();
}

fn get_char_value(c:char) -> Option<u32> {
    return c.to_digit(10);
}

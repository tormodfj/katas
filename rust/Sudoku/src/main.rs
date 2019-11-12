/*--------------------------------------------------------------------------------------------------------------
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License. See https://go.microsoft.com/fwlink/?linkid=2090316 for license information.
 *-------------------------------------------------------------------------------------------------------------*/
use std::env;

fn main() {
    let args: Vec<String> = env::args().skip(1).collect();
    let name = if args.len() > 0 { &args[0] } else { "VS Code Remote - Containers" };
    println!("Hello, {}!", name);
}

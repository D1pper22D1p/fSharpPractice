open System

[<EntryPoint>]
let main argv =
    printf "Input ur name: "
    let name = Console.ReadLine()
    printfn "Ur name is: %s" name
    0

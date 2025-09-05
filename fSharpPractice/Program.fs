open System

[<EntryPoint>]
let main argv =
    let arr = [| 1..100 |]
    let Sum = Array.sum (arr)
    printfn "%d" Sum
    0

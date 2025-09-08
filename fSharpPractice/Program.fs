open System
open System.Threading

type Direction = Up | Down | Left | Right
type Position = int * int
type GameState = {
    Snake: Position list
    Food: Position
    Direction: Direction
    Width: int
    Height: int
    Score: int
    GameOver: bool
}

let width = 20
let height = 15
let initialSnake = [(10, 7); (9, 7); (8, 7)]
let initialFood = (5, 5)
let gameSpeed = 150

let random = Random()
let generateFood (snake: Position list) (width: int) (height: int) : Position =
    let rec generate() =
        let food = (random.Next(1, width - 1), random.Next(1, height - 1))
        if List.contains food snake then generate() else food
    generate()

let checkCollision (snake: Position list) (width: int) (height: int) : bool =
    let head = List.head snake
    let (x, y) = head
    
    if x <= 0 || x >= width - 1 || y <= 0 || y >= height - 1 then
        true
    elif List.tail snake |> List.contains head then
        true
    else
        false

let moveSnake (state: GameState) : GameState =
    let (headX, headY) = List.head state.Snake
    
    let newHead = 
        match state.Direction with
        | Up -> (headX, headY - 1)
        | Down -> (headX, headY + 1)
        | Left -> (headX - 1, headY)
        | Right -> (headX + 1, headY)
    
    let ateFood = newHead = state.Food
    
    let newSnake = 
        if ateFood then
            newHead :: state.Snake
        else
            newHead :: (List.take (List.length state.Snake - 1) state.Snake)
    
    let newFood = if ateFood then generateFood newSnake state.Width state.Height else state.Food
    let newScore = if ateFood then state.Score + 1 else state.Score
    let gameOver = checkCollision newSnake state.Width state.Height
    
    { state with 
        Snake = newSnake
        Food = newFood
        Score = newScore
        GameOver = gameOver }

let draw (state: GameState) =
    Console.Clear()
    Console.SetCursorPosition(0, 0)
    printfn "Змейка на F# | Счет: %d" state.Score
    
    printfn "%s" (String.replicate state.Width "#")
    
    for y = 0 to state.Height - 1 do
        for x = 0 to state.Width - 1 do
            let pos = (x, y)
            if x = 0 || x = state.Width - 1 || y = 0 || y = state.Height - 1 then
                printf "#"
            elif List.contains pos state.Snake then
                if pos = List.head state.Snake then
                    printf "O"
                else
                    printf "o"
            elif pos = state.Food then
                printf "*"
            else
                printf " "
        printfn ""
    
    printfn "%s" (String.replicate state.Width "#")
    
    if state.GameOver then
        printfn "\nИГРА ОКОНЧЕНА! Финальный счет: %d" state.Score
        printfn "Нажмите любую клавишу для выхода..."

let handleInput (state: GameState) : Direction option =
    if Console.KeyAvailable then
        let key = Console.ReadKey(true).Key
        match key with
        | ConsoleKey.UpArrow when state.Direction <> Down -> Some Up
        | ConsoleKey.DownArrow when state.Direction <> Up -> Some Down
        | ConsoleKey.LeftArrow when state.Direction <> Right -> Some Left
        | ConsoleKey.RightArrow when state.Direction <> Left -> Some Right
        | ConsoleKey.Escape -> 
            printfn "Выход из игры..."
            Environment.Exit(0)
            None
        | _ -> None
    else
        None

let rec gameLoop (state: GameState) =
    if not state.GameOver then
        draw state
        Thread.Sleep(gameSpeed)
        
        let newDirection = 
            match handleInput state with
            | Some dir -> dir
            | None -> state.Direction
        
        let updatedState = { state with Direction = newDirection }
        let newState = moveSnake updatedState
        
        gameLoop newState
    else
        draw state
        Console.ReadKey() |> ignore

let initGameState = {
    Snake = initialSnake
    Food = initialFood
    Direction = Right
    Width = width
    Height = height
    Score = 0
    GameOver = false
}

Console.Title <- "Змейка на F#"
Console.CursorVisible <- false

printfn "Добро пожаловать в Змейку на F#!"
printfn "Управление: стрелки для движения, ESC для выхода"
printfn "Нажмите любую клавишу для начала..."
Console.ReadKey() |> ignore

gameLoop initGameState
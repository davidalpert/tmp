namespace BowlingCalculator

type BowlingGame(numberOfPlayers:int) =
    let players:string [] = Array.zeroCreate numberOfPlayers
    let scores:int [] = Array.zeroCreate numberOfPlayers
    let mutable currentPlayerIndex = 0
    let mutable currentBall = 1
    let mutable currentFrame = 1

    member this.ListPlayerNames() =
        players

    member this.CurrentPlayer =
        players.GetValue(currentPlayerIndex)

    member this.CurrentFrame =
        currentFrame

    member this.CurrentBall =
        currentBall

    member this.KnockDown(numberOfPins:int) =
        let oldScore = scores.[currentPlayerIndex]
        scores.SetValue(oldScore + numberOfPins, currentPlayerIndex)
        currentFrame <- match currentBall with
                        | 2 -> currentFrame + 1
                        | 1 -> currentFrame
        currentBall <- match currentBall with
                        | 2 -> 1 
                        | 1 -> currentBall + 1
        ()

    member this.SetPlayerName(index:int,name:string) =
        players.SetValue(name, index-1)

    member this.StartGame() =
        ()

    member this.GetScores() =
        players
        |> Array.mapi (fun i n -> (n,scores.[i]))
        |> dict

    member this.GetScores1() =
        let d = new System.Collections.Generic.Dictionary<_,_>()
        players
        |> Array.mapi (fun i n -> d.Add(n,scores.[i]))
        |> ignore
        d

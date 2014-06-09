namespace BowlingCalculator

type BowlingGame(numberOfPlayers:int) =
    let players:string [] = Array.zeroCreate numberOfPlayers
    let mutable currentPlayerIndex = 0

    member this.ListPlayers() =
        players

    member this.CurrentPlayer =
        players.GetValue(currentPlayerIndex)

    member this.CurrentFrame =
        1

    member this.CurrentBall =
        1

    member this.SetPlayerName(index:int,name:string) =
        players.SetValue(name, index)

    member this.StartGame() =
        ()

    member this.GetScores() =
        players
        |> Array.map (fun n -> (n,0))
        |> dict

    member this.GetScores1() =
        let d = new System.Collections.Generic.Dictionary<_,_>()
        players
        |> Array.map (fun n -> d.Add(n,0))
        |> ignore
        d

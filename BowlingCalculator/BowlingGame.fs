namespace BowlingCalculator

type BowlingGame(numberOfPlayers:int) =
    let players:string [] = Array.zeroCreate numberOfPlayers
    member this.ListPlayers =
        players
    member this.SetPlayerName(index:int,name:string) =
        players.SetValue(name, index)

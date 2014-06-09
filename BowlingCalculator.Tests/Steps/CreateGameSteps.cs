using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BowlingCalculator.Tests.Steps
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> actOn)
        {
            if (actOn == null) return;
            items.ForEach((index, item) => actOn(item));
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<int,T> actOn)
        {
            if (actOn == null) return;
            var index = 0;
            foreach (var item in items)
            {
                actOn(index, item);
                index++;
            }
        }
    }

    [Binding]
    public class CreateGameSteps
    {
        [Given(@"a game has been set up with the following players:")]
        public void GivenAGameHasBeenSetUpWithTheFollowingPlayers(Table table)
        {
            GivenICreateAGameWithPlayer(table.RowCount);
            GivenIAddTheFollowingPlayersToTheGame(table);
        }
        
        [When(@"I press start")]
        public void WhenIPressStart()
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            game.StartGame();
        }
        
        [Then(@"the players should have the following scores:")]
        public void ThenThePlayersShouldHaveTheFollowingScores(Table table)
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            var actualScores = game.GetScores();
            var expectedScores = table.Rows
                                      .ToDictionary(r => r.Values.ElementAt(0), 
                                                    r => Int32.Parse(r.Values.ElementAt(1)));
            CollectionAssert.AreEqual(expectedScores, actualScores);
        }

        [Then(@"it should be (.*?)'s turn")]
        public void ThenItShouldBeTurn(string playerName)
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            Assert.AreEqual(playerName, game.CurrentPlayer);
        }

        [Then(@"the game should be ready to receive frame (.*), ball (.*)")]
        public void ThenTheGameShouldBeReadyToReceiveBallOfFrame(int frameNumber, int ballNumber)
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            Assert.AreEqual(frameNumber, game.CurrentFrame);
            Assert.AreEqual(ballNumber, game.CurrentBall);
        }

        [Given(@"I create a game with (.*) players?")]
        public void GivenICreateAGameWithPlayer(int numberOfPlayers)
        {
            var game = new BowlingGame(numberOfPlayers);
            ScenarioContext.Current.Set(game);
        }
        
        [Given(@"I add the following players to the game:")]
        public void GivenIAddTheFollowingPlayersToTheGame(Table table)
        {
            var names = table.Rows.Select(r => r.Values.ElementAt(0).Trim());
            var game = ScenarioContext.Current.Get<BowlingGame>();
            names.ForEach(game.SetPlayerName);
        }
        
        [When(@"I list the players")]
        public void WhenIListThePlayers()
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            var list = game.ListPlayers();
            ScenarioContext.Current.Set(list, "player list");
        }
        
        [Then(@"the list shall include the following players:")]
        public void ThenTheListShallIncludeTheFollowingPlayers(Table table)
        {
            var names = table.CreateSet<string>();
            var playerNames = ScenarioContext.Current.Get<string[]>("player list");
            CollectionAssert.AreEqual(names, playerNames);
        }
    }
}

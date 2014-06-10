using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BowlingCalculator.Tests.Steps
{
    [Binding]
    public class GameSteps
    {
        #region Context Helpers

        public BowlingGame CurrentGame
        {
            get { return ScenarioContext.Current.Get<BowlingGame>(); }
            set { ScenarioContext.Current.Set(value); }
        }

        public string[] CurrentPlayerNames
        {
            get { return ScenarioContext.Current.Get<string[]>("Player List"); }
            set { ScenarioContext.Current.Set(value, "Player List"); }
        }

        #endregion

        [Given(@"a game has been set up with the following players:")]
        public void GivenAGameHasBeenSetUpWithTheFollowingPlayers(Table table)
        {
            GivenICreateAGameWithPlayer(table.RowCount);
            GivenIAddTheFollowingPlayersToTheGame(table);
        }

        [Given(@"I create a game with (.*) players?")]
        public void GivenICreateAGameWithPlayer(int numberOfPlayers)
        {
            var game = new BowlingGame(numberOfPlayers);
            CurrentGame = game;
        }

        [Given(@"I add the following players to the game:")]
        public void GivenIAddTheFollowingPlayersToTheGame(Table table)
        {
            var names = table.Rows.Select(r => r.Values.ElementAt(0).Trim());
            var game = CurrentGame;
            names.ForEach(game.SetPlayerName,1);
        }

        [When(@"I list the players")]
        public void WhenIListThePlayers()
        {
            var game = CurrentGame;

            CurrentPlayerNames = game.ListPlayers();
        }

        [When(@"I press start")]
        public void WhenIPressStart()
        {
            var game = CurrentGame;
            game.StartGame();
        }

        [StepArgumentTransformation]
        public int[][] TransformThrows(Table table)
        {
            return table.Rows.Select(r => r.ElementAt(0).Value
                                                        .Split(',')
                                                        .Select(Int32.Parse)
                                                        .ToArray())
                             .ToArray();
        }

        [When(@"I record the following results:")]
        public void WhenIRecordTheFollowingThrows(int[][] throws)
        {
            var game = CurrentGame;
            throws.SelectMany(x => x).ForEach(game.KnockDown);
        }

        [Then(@"the players should have the following scores:")]
        public void ThenThePlayersShouldHaveTheFollowingScores(Table table)
        {
            var game = CurrentGame;
            var actualScores = game.GetScores();
            var expectedScores = table.Rows
                                      .ToDictionary(r => r.Values.ElementAt(0), 
                                                    r => Int32.Parse(r.Values.ElementAt(1)));
            CollectionAssert.AreEqual(expectedScores, actualScores);
        }

        [Then(@"it should be (.*?)'s turn")]
        public void ThenItShouldBeTurn(string playerName)
        {
            var game = CurrentGame;
            Assert.AreEqual(playerName, game.CurrentPlayer);
        }

        [Then(@"the game should be ready to receive frame (.*), ball (.*)")]
        public void ThenTheGameShouldBeReadyToReceiveBallOfFrame(int frameNumber, int ballNumber)
        {
            var game = CurrentGame;
            Assert.AreEqual(frameNumber, game.CurrentFrame);
            Assert.AreEqual(ballNumber, game.CurrentBall);
        }

        [Then(@"the list shall include the following players:")]
        public void ThenTheListShallIncludeTheFollowingPlayers(Table table)
        {
            var expectedNames = table.Rows.Select(r => r.GetString("name"));
            CollectionAssert.AreEqual(expectedNames, CurrentPlayerNames);
        }
    }
}

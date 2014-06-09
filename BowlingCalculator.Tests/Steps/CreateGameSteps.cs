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
        [Given(@"I create a game with (.*) players?")]
        public void GivenICreateAGameWithPlayer(int numberOfPlayers)
        {
            var game = new BowlingGame(numberOfPlayers);
            ScenarioContext.Current.Set(game);
        }
        
        [Given(@"I add the following players to the game:")]
        public void GivenIAddTheFollowingPlayersToTheGame(Table table)
        {
            var names = table.CreateSet<string>();
            var game = ScenarioContext.Current.Get<BowlingGame>();
            names.ForEach(game.SetPlayerName);
        }
        
        [When(@"I list the players")]
        public void WhenIListThePlayers()
        {
            var game = ScenarioContext.Current.Get<BowlingGame>();
            var list = game.ListPlayers;
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

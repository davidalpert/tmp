using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ObjectApproval;

namespace BowlingCalculator.Tests.UnitTests
{
    [TestFixture]
    public class BowlingGameTests
    {
        [Test]
        public void Can_add_players()
        {
            var game = new BowlingGame(1);

            game.SetPlayerName(1, "Mal");

            ObjectApprover.VerifyWithJson(game.ListPlayerNames());
        }

        [Test]
        public void Can_add_several_players()
        {
            var game = new BowlingGame(4);

            game.SetPlayerName(1, "Mal");
            game.SetPlayerName(2, "Inara");
            game.SetPlayerName(3, "Wash");
            game.SetPlayerName(4, "Zoe");

            ObjectApprover.VerifyWithJson(game.ListPlayerNames());
        }
    }
}

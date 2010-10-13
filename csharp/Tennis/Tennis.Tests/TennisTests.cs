using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tennis.Tests
{
    [TestClass]
    public class When_player1_scores_a_point : Shared
    {
        public When_player1_scores_a_point()
        {
            _eventRaised = false;
            _tennis.ScorePointToPlayerOne();
        }

        [TestMethod]
        public void Should_raise_ScoreChanged_event()
        {
            Assert.IsTrue(_eventRaised);
        }
    }

    [TestClass]
    public class When_player2_scores_a_point : Shared
    {
        public When_player2_scores_a_point()
        {
            _eventRaised = false;
            _tennis.ScorePointToPlayerTwo();
        }

        [TestMethod]
        public void Should_raise_ScoreChanged_event()
        {
            Assert.IsTrue(_eventRaised);
        }
    }

    [TestClass]
    public class When_no_player_has_scored : Shared
    {
        [TestMethod]
        public void Should_report_score_Love_all()
        {
            Assert.AreEqual("Love all", _tennis.Score);
        }
    }

    [TestClass]
    public class When_game_progresses : Shared
    {
        [TestMethod]
        public void Should_report_appropriate_scores()
        {
            var testCases = new[]
            {
                new { Game = "1", ExpectedScore = "15, Love" },
                new { Game = "11", ExpectedScore = "30, Love" },
                new { Game = "111", ExpectedScore = "40, Love. Set ball " + Player1 },
                new { Game = "1111", ExpectedScore = Player1 + " wins!" },

                new { Game = "2", ExpectedScore = "Love, 15" },
                new { Game = "22", ExpectedScore = "Love, 30" },
                new { Game = "222", ExpectedScore = "Love, 40. Set ball " + Player2 },
                new { Game = "2222", ExpectedScore = Player2 + " wins!" },

                new { Game = "12", ExpectedScore = "15 all" },
                new { Game = "21", ExpectedScore = "15 all" },
                new { Game = "1122", ExpectedScore = "30 all" },
                new { Game = "1221", ExpectedScore = "30 all" },

                new { Game = "111222", ExpectedScore = "Deuce" },
                new { Game = "11112222", ExpectedScore = "Deuce" },
                new { Game = "1111122222", ExpectedScore = "Deuce" },
                new { Game = "111111222222", ExpectedScore = "Deuce" },

                new { Game = "112", ExpectedScore = "30, 15" },
                new { Game = "1121", ExpectedScore = "40, 15. Set ball " + Player1 },
                new { Game = "11212", ExpectedScore = "40, 30. Set ball " + Player1 },
                new { Game = "112121", ExpectedScore = Player1 + " wins!" },

                new { Game = "1121221", ExpectedScore = "Advantage " + Player1 },
                new { Game = "11212212", ExpectedScore = "Deuce" },
                new { Game = "112122122", ExpectedScore = "Advantage " + Player2 },
                new { Game = "1121221222", ExpectedScore = Player2 + " wins!" }
            };

            foreach (var game in testCases)
            {
                NewGame();
                Play(game.Game, game.ExpectedScore);
            }
        }

        private void Play(string gameProgress, string expectedScore)
        {
            Play(gameProgress);
            Assert.AreEqual(expectedScore, _tennis.Score);
        }

        private void Play(string gameProgress)
        {
            foreach (char point in gameProgress)
            {
                switch (point)
                {
                    case '1':
                        _tennis.ScorePointToPlayerOne();
                        break;
                    case '2':
                        _tennis.ScorePointToPlayerTwo();
                        break;
                    default:
                        Assert.Fail("Only characters '1' and '2' allowed");
                        break;
                }
            }
        }
    }

    public class Shared
    {
        protected const string Player1 = "John Doe";
        protected const string Player2 = "Jane Doe";

        protected Tennis _tennis;
        protected bool _eventRaised;

        public Shared()
        {
            NewGame();
        }

        protected void NewGame()
        {
            _tennis = new Tennis(Player1, Player2);
            _tennis.ScoreChanged += (o, e) => _eventRaised = true;
        }
    }
}

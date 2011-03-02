using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Tests
{
	[TestClass]
	public class When_a_new_game_is_created : Shared
	{
		[TestMethod]
		public void Should_give_all_players_zero_score()
		{
			Assert.AreEqual(0, game.Score[player1]);
			Assert.AreEqual(0, game.Score[player2]);
		}

		[TestMethod]
		public void Should_start_with_first_player()
		{
			Assert.AreEqual(player1, game.CurrentPlayer);
		}
	}

	[TestClass]
	public class When_first_player_knocks_three_pins : Shared
	{
		protected override void AfterInit()
		{
			base.AfterInit();
			game.RegisterPinsKnockedOver(3);
		}

		[TestMethod]
		public void Should_not_score_any_points_yet()
		{
			Assert.AreEqual(0, game.Score[player1]);
		}

		[TestMethod]
		public void Should_give_first_player_second_attempt()
		{
			Assert.AreEqual(player1, game.CurrentPlayer);
		}
	}
	
	[TestClass]
	public class When_first_player_knocks_three_pins_twice : Shared
	{
		protected override void AfterInit()
		{
			base.AfterInit();
			game.RegisterPinsKnockedOver(3);
			game.RegisterPinsKnockedOver(3);
		}

		[TestMethod]
		public void Should_score_six_points()
		{
			Assert.AreEqual(6, game.Score[player1]);
		}

		[TestMethod]
		public void Should_advance_to_next_player()
		{
			Assert.AreEqual(player2, game.CurrentPlayer);
		}
	}

	[TestClass]
	public class When_game_progresses : Shared
	{
		[TestMethod]
		public void Should_report_appropriate_players_and_scores()
		{
			var testCases = new[]
			{
				new { KnockedPins = "6", ExpectedScores = "0-0", ExpectedPlayer = player1 },
				new { KnockedPins = "6-2", ExpectedScores = "8-0", ExpectedPlayer = player2 },
				new { KnockedPins = "6-2 - 3", ExpectedScores = "8-0", ExpectedPlayer = player2 },
				new { KnockedPins = "6-2 - 3-4", ExpectedScores = "8-7", ExpectedPlayer = player1 },
				new { KnockedPins = "6-2 - 3-4 - 10", ExpectedScores = "8-7", ExpectedPlayer = player2 },

				new { KnockedPins = "6-4", ExpectedScores = "0-0", ExpectedPlayer = player2 },
				new { KnockedPins = "6-4 - 3-3", ExpectedScores = "0-6", ExpectedPlayer = player1 },
				new { KnockedPins = "6-4 - 3-3 - 5", ExpectedScores = "15-6", ExpectedPlayer = player1 },
				new { KnockedPins = "6-4 - 3-3 - 5-5", ExpectedScores = "15-6", ExpectedPlayer = player2 },
				new { KnockedPins = "6-4 - 3-3 - 5-5 - 10", ExpectedScores = "15-6", ExpectedPlayer = player1 },
				new { KnockedPins = "6-4 - 3-3 - 5-5 - 10 - 10", ExpectedScores = "35-6", ExpectedPlayer = player2 },
				new { KnockedPins = "6-4 - 3-3 - 5-5 - 10 - 10 - 10", ExpectedScores = "35-6", ExpectedPlayer = player1 },
				new { KnockedPins = "6-4 - 3-3 - 5-5 - 10 - 10 - 10 - 3-5", ExpectedScores = "61-6", ExpectedPlayer = player2 },
				new { KnockedPins = "6-4 - 3-3 - 5-5 - 10 - 10 - 10 - 3-5 - 10", ExpectedScores = "61-36", ExpectedPlayer = player2 },
			};

			foreach (var game in testCases)
			{
				NewGame();
				Play(game.KnockedPins, game.ExpectedScores, game.ExpectedPlayer);
			}
		}

		private void Play(string knockedPins, string expectedScores, string expectedPlayer)
		{
			Play(knockedPins
				.Split('-')
				.Select(s => Convert.ToInt32(s)));

			AssertExpectedScores(expectedScores
				.Split('-')
				.Select(s => Convert.ToInt32(s))
				.ToArray());

			AssertExpectedPlayer(expectedPlayer);
		}

		private void Play(IEnumerable<int> attempts)
		{
			foreach (var knockedPins in attempts)
			{
				game.RegisterPinsKnockedOver(knockedPins);
			}
		}

		private void AssertExpectedScores(int[] scores)
		{
			Assert.AreEqual(scores[0], game.Score[player1]);
			Assert.AreEqual(scores[1], game.Score[player2]);
		}

		private void AssertExpectedPlayer(string expectedPlayer)
		{
			Assert.AreEqual(expectedPlayer, game.CurrentPlayer);
		}
	}

	public abstract class Shared
	{
		protected Game game;

		protected string player1;
		protected string player2;

		[TestInitialize]
		public void TestInit()
		{
			InitFields();
			BeforeInit();

			NewGame();

			AfterInit();
		}

		protected void NewGame()
		{
			game = new Game(player1, player2);
		}

		private void InitFields()
		{
			player1 = "Alice";
			player2 = "Bob";
		}

		protected virtual void BeforeInit()
		{
		}

		protected virtual void AfterInit()
		{
		}
	}
}

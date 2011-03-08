using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Tests.GameSpecs
{
	[TestClass]
	public class When_a_new_game_is_created : Shared
	{
		[TestMethod]
		public void Should_give_all_players_zero_score()
		{
			Assert.AreEqual(0, Game.Score[Player1]);
			Assert.AreEqual(0, Game.Score[Player2]);
		}

		[TestMethod]
		public void Should_start_with_first_player()
		{
			AssertCurrentPlayer(Player1);
		}
	}

	[TestClass]
	public class When_a_player_doesnt_score_strike_on_first_attempt : Shared
	{
		protected override void AfterInit()
		{
			Game.KnockOver(3);
		}

		[TestMethod]
		public void Should_give_player_another_attempt()
		{
			AssertCurrentPlayer(Player1);
		}
	}
	
	[TestClass]
	public class When_a_player_has_spent_both_attempts : Shared
	{
		protected override void AfterInit()
		{
			Game.KnockOver(3);
			Game.KnockOver(3);
		}

		[TestMethod]
		public void Should_advance_to_next_player()
		{
			AssertCurrentPlayer(Player2);
		}
	}

	[TestClass]
	public class When_a_player_scores_a_strike : Shared
	{
		protected override void AfterInit()
		{
			Game.KnockOver(10);
		}

		[TestMethod]
		public void Should_advance_to_next_player()
		{
			AssertCurrentPlayer(Player2);
		}
	}

	public abstract class Shared
	{
		protected const string Player1 = "Alice";

		protected const string Player2 = "Bob";

		protected Game Game;

		[TestInitialize]
		public void TestInit()
		{
			NewGame();
			AfterInit();
		}

		protected void NewGame()
		{
			Game = new Game(Player1, Player2);
		}

		protected virtual void AfterInit()
		{
		}

		protected void AssertCurrentPlayer(string player)
		{
			Assert.AreEqual(player, Game.CurrentPlayer);
		}
	}
}

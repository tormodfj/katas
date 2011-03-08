using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bowling.Tests.PlayerSpecs
{
	[TestClass]
	public class When_player_doesnt_strike_on_first_attempt : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(3);
		}

		[TestMethod]
		public void Should_not_count_score_immediately()
		{
			AssertScore(0);
		}
	}

	[TestClass]
	public class When_player_doesnt_strike_or_spare_in_frame : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(3, 6);
		}

		[TestMethod]
		public void Should_count_score_as_sum_of_pins()
		{
			AssertScore(9);
		}
	}

	[TestClass]
	public class When_player_gets_a_spare : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(4, 6);
		}

		[TestMethod]
		public void Should_not_count_score_immediately()
		{
			AssertScore(0);
		}

		[TestMethod]
		public void Should_count_score_for_strike_after_next_attempt()
		{
			KnockOver(3);
			AssertScore(13);
		}
	}

	[TestClass]
	public class When_player_gets_a_strike : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(10);
		}

		[TestMethod]
		public void Should_not_count_score_immediately()
		{
			AssertScore(0);
		}

		[TestMethod]
		public void Should_not_count_score_after_following_attempt()
		{
			KnockOver(8);
			AssertScore(0);
		}

		[TestMethod]
		public void Should_count_score_after_following_two_attempts()
		{
			KnockOver(8, 1);
			AssertScore(28);
		}
	}

	[TestClass]
	public class When_player_get_two_consecutive_strikes : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(10, 10);
		}

		[TestMethod]
		public void Should_not_count_score_immediately()
		{
			AssertScore(0);
		}

		[TestMethod]
		public void Should_count_score_for_first_strike_after_next_attempt()
		{
			KnockOver(3);
			AssertScore(23);
		}

		[TestMethod]
		public void Should_count_score_for_second_strike_after_second_next_attempt()
		{
			KnockOver(3, 3);
			AssertScore(45);
		}
	}

	[TestClass]
	public class When_player_scores_a_perfect_game : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10);
		}

		[TestMethod]
		public void Should_score_300()
		{
			AssertScore(300);
		}
	}

	[TestClass]
	public class When_player_gets_complex_score_card : Shared
	{
		protected override void AfterInit()
		{
			KnockOver(10, 10, 2, 8, 10, 4);
		}

		[TestMethod]
		public void Should_keep_track_of_score()
		{
			AssertScore(62);
		}
	}

	public abstract class Shared
	{
		private Player player;

		[TestInitialize]
		public void TestInit()
		{
			player = new Player("Foo");
			AfterInit();
		}

		protected virtual void AfterInit()
		{
		}

		protected void KnockOver(params int[] attempts)
		{
			foreach(var pins in attempts)
			{
				player.KnockOver(pins);
			}
		}

		protected void AssertScore(int expectedScore)
		{
			Assert.AreEqual(expectedScore, player.CalculateScore());
		}
	}
}

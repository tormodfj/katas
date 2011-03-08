using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
	public class Game
	{
		private readonly List<Player> players;

		private int currentPlayer;

		private int remainingAttempts;

		public Game(params string[] players)
		{
			this.players = new List<Player>(players.Select(p => new Player(p)));
			this.currentPlayer = 0;
			this.remainingAttempts = 2;
		}

		public IDictionary<string, int> Score
		{
			get { return players.ToDictionary(x => x.Name, x => x.CalculateScore()); }
		}

		public string CurrentPlayer
		{
			get { return players[currentPlayer].Name; }
		}

		private bool PlayerHasNoRemainingAttempts
		{
			get { return remainingAttempts == 0; }
		}

		public void KnockOver(int numberOfPins)
		{
			RegisterPinsOnCurrentPlayer(numberOfPins);
			UpdateRemainingAttempts(numberOfPins);

			if (PlayerHasNoRemainingAttempts)
			{
				AdvanceToNextPlayer();
			}
		}

		private void RegisterPinsOnCurrentPlayer(int numberOfPins)
		{
			players[currentPlayer].KnockOver(numberOfPins);
		}

		private void UpdateRemainingAttempts(int numberOfPins)
		{
			if (numberOfPins == 10)
			{
				remainingAttempts = 0;
			}
			else
			{
				remainingAttempts--;
			}
		}

		private void AdvanceToNextPlayer()
		{
			currentPlayer = (currentPlayer + 1) % players.Count;
			remainingAttempts = 2;
		}
	}
}

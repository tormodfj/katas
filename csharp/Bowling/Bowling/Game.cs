using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bowling
{
	public class Game
	{
		private Dictionary<string, int> playerScores;

		private Dictionary<string, int> pendingBonuses;

		private Dictionary<string, int> pendingBonusPoints;

		private int currentPlayer;

		private int currentFrame;

		private int currentAttempt;

		private int pinsRemaining;

		public Game(params string[] players)
		{
			playerScores = players.ToDictionary(p => p, p => 0);
			pendingBonuses = players.ToDictionary(p => p, p => 0);
			pendingBonusPoints = players.ToDictionary(p => p, p => 0);
			currentPlayer = 0;
			currentAttempt = 0;
			pinsRemaining = 10;
		}

		public IDictionary<string, int> Score
		{
			get { return playerScores.ToDictionary(x => x.Key, x => x.Value); }
		}

		public string CurrentPlayer
		{
			get { return playerScores.Keys.ElementAt(currentPlayer); }
		}

		public bool FirstAttempt
		{
			get { return currentAttempt == 0; }
		}

		private bool CurrentPlayerHasNoMoreAttempts
		{
			get { return currentAttempt == 1; }
		}

		public bool AllPinsAreKnockedDown
		{
			get { return pinsRemaining == 0; }
		}

		public bool CurrentFrameHasNoMorePlayers 
		{
			get { return currentPlayer == playerScores.Count - 1; }
		}

		public bool CurrentPlayerHasPendingBonuses 
		{
			get { return pendingBonuses[CurrentPlayer] > 0; }
		}

		public void RegisterPinsKnockedOver(int pinsKnockedOver)
		{
			pinsRemaining = Math.Max(0, pinsRemaining - pinsKnockedOver);

			if (CurrentPlayerHasPendingBonuses)
			{
				pendingBonusPoints[CurrentPlayer] += pinsKnockedOver;

				if (--pendingBonuses[CurrentPlayer] == 0)
				{
					playerScores[CurrentPlayer] += pendingBonusPoints[CurrentPlayer];
				}
			}

			if (CurrentPlayerHasNoMoreAttempts)
			{
				AdvanceToNextPlayer();
			}
			else
			{
				if (AllPinsAreKnockedDown)
				{
					AdvanceToNextPlayer();
				}
				else
				{
					AdvanceToNextAttempt();
				}
			}
		}

		private void UpdateCurrentPlayerScore()
		{
			if (AllPinsAreKnockedDown)
			{
				if (FirstAttempt)
				{
					RegisterStrikeForCurrentPlayer();
				}
				else
				{
					RegisterSpareForCurrentPlayer();
				}
			}
			else
			{
				playerScores[CurrentPlayer] += 10 - pinsRemaining;
			}
		}

		private void AdvanceToNextAttempt()
		{
			currentAttempt = (currentAttempt + 1) % 2;
		}

		private void AdvanceToNextPlayer()
		{
			UpdateCurrentPlayerScore();

			currentPlayer = (currentPlayer + 1) % playerScores.Count;
			currentAttempt = 0;
			pinsRemaining = 10;

			if (CurrentFrameHasNoMorePlayers)
			{
				currentFrame++;
			}
		}

		private void RegisterSpareForCurrentPlayer()
		{
			pendingBonuses[CurrentPlayer] = 1;
			pendingBonusPoints[CurrentPlayer] = 10;
		}

		private void RegisterStrikeForCurrentPlayer()
		{
			pendingBonuses[CurrentPlayer] = 2;
			pendingBonusPoints[CurrentPlayer] = 10;
		}
	}
}

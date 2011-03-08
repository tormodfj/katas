using System.Collections.Generic;

namespace Bowling
{
	public class Player
	{
		private readonly string name;

		private readonly List<int> pinsKnockedOver;

		public Player(string name)
		{
			this.pinsKnockedOver = new List<int>();
			this.name = name;
		}

		public string Name
		{
			get { return name; }
		}

		public void KnockOver(int numberOfPins)
		{
			pinsKnockedOver.Add(numberOfPins);
		}

		public int CalculateScore()
		{
			return BowlingCalculator.CalculateScore(pinsKnockedOver.ToFSharpList());
		}
	}
}

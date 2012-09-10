using System;

namespace PokerHands.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter player 1's hand:");
            var player1Hand = Console.ReadLine();

            Console.WriteLine("Enter player 2's hand:");
            var player2Hand = Console.ReadLine();

            var result = PokerLib.determineWinner(player1Hand, player2Hand);

			if(result == 0)
				Console.WriteLine("Tie");
			if(result > 0)
				Console.WriteLine("Player 1 wins!");
			if(result < 0)
				Console.WriteLine("Player 2 wins!");

            Console.ReadKey();
        }
    }
}

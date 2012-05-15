using System;
using System.Linq;

namespace Reversi
{
    public static class Reversi
    {
        public static string Run(string input)
        {
            char[][] parsedInput = Parse(input);
            char[][] legalMoves = FillLegalMoves(parsedInput, '0');

            return FormatBoard(legalMoves);
        }

        private static char[][] Parse(string input)
        {
            return input
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.ToArray())
                .ToArray();
        }

        private static char[][] FillLegalMoves(char[][] input, char legalMoveIndicator)
        {
			char currentPlayer = input[8][0];
            char[][] relevantInput = input.Take(8).ToArray();

            return 
                Enumerable.Range(0, 8).Select(row =>
                    Enumerable.Range(0, 8).Select(col =>
                        IsLegalMove(relevantInput, currentPlayer, row, col) 
                            ? legalMoveIndicator 
                            : input[row][col])
                        .ToArray())
                    .Concat(new[] { new[] { currentPlayer } })
                    .ToArray();
        }

        private static string FormatBoard(char[][] legalMoves)
        {
			var lines = legalMoves.Select(line => new string(line));
			return string.Join(Environment.NewLine, lines);
        }

        private static bool IsLegalMove(char[][] input, char player, int row, int col)
        {
            return IsLegal(GoingN(input, row, col), player) 
                || IsLegal(GoingNE(input, row, col), player)
                || IsLegal(GoingE(input, row, col), player)
                || IsLegal(GoingSE(input, row, col), player) 
                || IsLegal(GoingS(input, row, col), player) 
                || IsLegal(GoingSW(input, row, col), player) 
                || IsLegal(GoingW(input, row, col), player) 
                || IsLegal(GoingNW(input, row, col), player);
        }

        private static char[] GoingN(char[][] input, int row, int col)
        {
            return input.Take(row + 1).Reverse().Select(cs => cs[col]).ToArray();
        }

        private static char[] GoingNE(char[][] input, int row, int col)
        {
            return input.Take(row + 1).Reverse().Take(8 - col).Select(cs => cs[col++]).ToArray();
        }

        private static char[] GoingE(char[][] input, int row, int col)
        {
            return input[row].Skip(col).ToArray();
        }

        private static char[] GoingSE(char[][] input, int row, int col)
        {
            return input.Skip(row).Take(8 - col).Select(cs => cs[col++]).ToArray();
        }

        private static char[] GoingS(char[][] input, int row, int col)
        {
            return input.Skip(row).Select(cs => cs[col]).ToArray();
        }

        private static char[] GoingSW(char[][] input, int row, int col)
        {
            return input.Skip(row).Take(col + 1).Select(cs => cs[col--]).ToArray();
        }

        private static char[] GoingW(char[][] input, int row, int col)
        {
            return input[row].Take(col + 1).Reverse().ToArray();
        }

        private static char[] GoingNW(char[][] input, int row, int col)
        {
            return input.Take(row + 1).Reverse().Take(col).Select(cs => cs[col--]).ToArray();
        }

        private static bool IsLegal(char[] input, char player)
        {
            return IsFreeSpot(input)
                && IsOtherPlayerAdjacent(input, player)
                && IsPlayerBeforeDotOrEnd(input, player);
        }

        private static bool IsFreeSpot(char[] input)
        {
            return input.Any() && input[0] == '.';
        }

        private static bool IsOtherPlayerAdjacent(char[] input, char player)
        {
            return input.Skip(1).Any() && input[1] == OtherPlayer(player);
        }

        private static bool IsPlayerBeforeDotOrEnd(char[] input, char player)
        {
            return input.Skip(1).TakeWhile(c => c != '.').Any(c => c == player);
        }

        private static char OtherPlayer(char player)
        {
			return player == 'B' ? 'W' : 'B';
        }
    }
}

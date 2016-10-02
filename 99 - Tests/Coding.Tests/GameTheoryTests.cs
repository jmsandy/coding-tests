using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coding.Tests
{
    internal sealed class GameStones
    {
        /// <summary>
        /// Maximum stones.
        /// </summary>
        private const byte MaxStones = 100;

        /// <summary>
        /// Possible moves.
        /// </summary>
        private readonly byte[] PossibleMoves = new byte[] { 5, 3, 2 };

        /// <summary>
        /// It's possible removes 2, 3 o 5 stones.
        /// </summary>
        /// <param name="stones">stone numbers.</param>
        /// <returns>is valid move.</returns>
        private bool IsValidMove(byte stones)
        {
            return stones == 5 || stones == 3 || stones == 2;
        }

        /// <summary>
        /// Verifies the game winner.
        /// </summary>
        /// <param name="stones">stones on the game.</param>
        /// <param name="players">players on the game.</param>
        /// <param name="currentPlayer">current player.</param>
        /// <returns>index to player's winner.</returns>
        public byte WhoWin(byte stones, byte[] players, byte currentPlayer = 0)
        {
            if (stones > MaxStones)
            {
                throw new ArgumentException("Invalid stones");
            }
            if (players == null || players.Length < 2)
            {
                throw new ArgumentException("Invalid players");
            }

            // Winner player.
            byte winnerPlayer = (byte)((currentPlayer + 1) < players.Length ? (currentPlayer + 1) : 0);

            // Rest of stones
            var restStones = -1;

            // Calculate the next move that benefits the current player.
            foreach (var move in PossibleMoves)
            {
                restStones = stones - move;
                if (restStones >= 0 && !IsValidMove((byte)restStones)) break;
            }

            // Verifies the winner.
            if (restStones >= 0)
            {
                winnerPlayer = WhoWin((byte)restStones, players, winnerPlayer);
            }

            return winnerPlayer;
        }
    }

    /// <summary>
    /// Tests to game theory.
    /// </summary>
    /// <Author>Jose Mauro da Silva Sandy - Rerum</Author>
    /// <Date>10/1/2016 8:52:38 PM</Date>
    [TestClass]
    public class GameTheoryTests
    {
        /// <summary>
        /// Game Theory - game stones.
        /// </summary>
        [TestMethod]
        public void GameStonesTest()
        {
            // Number of stones in a test case.
            var stones = new byte[] { 1, 2, 3, 4, 5, 6, 7, 10 };

            // The players
            var players = new byte[] { 1, 2 };

            // First player
            byte firstPlayer = 0;

            // Game stons algorithm.
            var gameStones = new GameStones();

            // Prints the winners for possible moves.
            for (int testCase = 0; testCase < stones.Length; testCase++)
            {
                var winnerPlayer = players[gameStones.WhoWin(stones[testCase], players, firstPlayer)];
                System.Diagnostics.Debug.WriteLine(string.Format("TestCase [{0}] - For {1} stones the winner is: {2}",
                    (testCase + 1), stones[testCase], winnerPlayer));
            }
        }
    }
}

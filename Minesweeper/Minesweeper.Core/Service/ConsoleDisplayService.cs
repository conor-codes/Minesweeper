using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Models;

namespace Minesweeper.Core.Service
{
    public class ConsoleDisplayService : IConsoleDisplayService
    {
        public ConsoleDisplayService()
        {
        }

        public void DisplayMessage(string input)
        {
            Console.WriteLine(input);
        }

        public string ConvertPosToAlphabet(int number)
        {
            string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            return alphabetArray.GetValue(number) as string;
        }

        public void DisplayLandedOnMineMessage(Player player)
        {
            string lifeFormat = player.Lives == 1 ? "life" : "lives";
            DisplayMessage($"Oops, you landed on a mine! You have {player.Lives} {lifeFormat} remaining");
        }

        public void DisplayPlayerMovedMessage(Player player)
        {
            DisplayMessage($"Player moved to: {ConvertPosToAlphabet(player.CurrentPosition.xPosition)}" +
               $", { player.CurrentPosition.yPosition + 1} Lives: {player.Lives} Moves: {player.Moves}");
        }
    }
}

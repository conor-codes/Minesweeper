using System;
using Minesweeper.Models;
namespace Minesweeper.Core.Interfaces
{
    public interface IConsoleDisplayService
    {
        void DisplayMessage(string input);
        void DisplayLandedOnMineMessage(Player player);
        void DisplayPlayerMovedMessage(Player player);
        string ConvertPosToAlphabet(int number);
    }
}

using System;
namespace Minesweeper.Core.Interfaces
{
    public interface IConsoleDisplayService
    {
        void DisplayMessage(string input);
        string ConvertPosToAlphabet(int number);
    }
}

using System;
using Minesweeper.Core.Interfaces;

namespace Minesweeper.Core.Service
{
    public class ConsoleDisplayService : IConsoleDisplayService
    {
        public ConsoleDisplayService()
        {
        }

        public string ConvertPosToAlphabet(int number)
        {
            string[] alphabetArray = { "A", "B", "C", "D", "E", "F", "G", "H" };
            return alphabetArray.GetValue(number) as string;
        }

        public void DisplayMessage(string input)
        {
            Console.WriteLine(input);
        }
    }
}

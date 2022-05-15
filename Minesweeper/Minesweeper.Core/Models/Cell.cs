using System;
namespace Minesweeper.Core.Models
{
    public class Cell
    {
        public int xPosition { get; set; }
        public int yPosition { get; set; }

        public bool HasMine { get; set; }

        public Cell(int x, int y)
        {
            xPosition = x;
            yPosition = y;
        }
    }
}

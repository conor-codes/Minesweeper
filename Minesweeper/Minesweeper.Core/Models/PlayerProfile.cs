using System;
using Minesweeper.Core.Models.Enums;

namespace Minesweeper.Core.Models
{
    public class Player
    {
        public GameState GameProgress { get; set; }
        public string Name { get; set; }
        public int Lives { get; set; }
        public int Moves { get; set; }
        public Cell CurrentPosition { get; set; }
    }
}

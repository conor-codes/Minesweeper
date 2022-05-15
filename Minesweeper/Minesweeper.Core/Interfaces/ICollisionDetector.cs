using System;
using Minesweeper.Core.Models;

namespace Minesweeper.Core.Interfaces
{
    public interface ICollisionDetectorService
    {
        bool DetectIfPlayerLandsOnMine(Player player, Cell[,] grid);
        bool DetectIfPlayerIsCloseToAMine(Player player, Cell[,] grid);
    }
}

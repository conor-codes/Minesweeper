using System;
using Minesweeper.Models;

namespace Minesweeper.Core.Interfaces
{
    public interface IGameStateService
    {
        void UpdateGameState(ref Player player, bool landedOnMine, ref Cell[,] grid);
    }
}

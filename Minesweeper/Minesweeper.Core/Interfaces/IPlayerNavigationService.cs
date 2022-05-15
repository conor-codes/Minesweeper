using System;
using Minesweeper.Core.Models;
using Minesweeper.Core.Models.Enums;

namespace Minesweeper.Core.Interfaces
{
    public interface IPlayerNavigationService
    {
        PlayerDirection PlayerInput();
        bool MovePlayer(ref Player player, Cell[,] grid, PlayerDirection move);
    }
}

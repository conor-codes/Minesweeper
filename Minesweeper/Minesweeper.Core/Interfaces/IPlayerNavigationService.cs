using System;
using Minesweeper.Models;
using Minesweeper.Models.Enums;

namespace Minesweeper.Core.Interfaces
{
    public interface IPlayerNavigationService
    {
        PlayerDirection PlayerInput();
        bool MovePlayer(ref Player player, Cell[,] grid, PlayerDirection move);
    }
}

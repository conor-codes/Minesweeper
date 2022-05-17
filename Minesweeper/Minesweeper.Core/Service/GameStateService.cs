using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Models;
using Minesweeper.Models.Enums;

namespace Minesweeper.Core.Service
{
    public class GameStateService : IGameStateService
    {
        private IConsoleDisplayService consoleDisplayService;
        public GameStateService(IConsoleDisplayService consoleDisplayService)
        {
            this.consoleDisplayService = consoleDisplayService;
        }

        public void UpdateGameState(ref Player player, bool landedOnMine, ref Cell[,] grid)
        {
            player.Moves++;
            consoleDisplayService.DisplayPlayerMovedMessage(player);

            //If a player lands on a mine, remove the mine from the cell and update the players lives.
            if (landedOnMine)
            {
                var refToCell = grid.GetValue(player.CurrentPosition.xPosition, player.CurrentPosition.yPosition) as Cell;
                refToCell.HasMine = false;

                player.Lives--;
                consoleDisplayService.DisplayLandedOnMineMessage(player);
            }

            // Update the game state
            if (player.Lives == 0)
            {
                player.GameProgress = GameState.Lose;
            }
            else if (player.CurrentPosition.yPosition == 7)
            {
                player.GameProgress = GameState.Won;
            }
        }
    }
}

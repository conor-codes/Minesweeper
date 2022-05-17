using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Models;

namespace Minesweeper.Core.Service
{
    public class CollisionDetectionService : ICollisionDetectorService
    {
        public CollisionDetectionService()
        {
        }

        public bool DetectIfPlayerLandsOnMine(Player player, Cell[,] grid)
        {
            var cell = grid.GetValue(player.CurrentPosition.xPosition, player.CurrentPosition.yPosition) as Cell;
            return cell.HasMine;
        }

        //Todo: This isn't working as expected (Not implemented).
        public bool DetectIfPlayerIsCloseToAMine(Player player, Cell[,] grid)
        {
            var cell = grid.GetValue(player.CurrentPosition.xPosition, player.CurrentPosition.yPosition) as Cell;
            var tempCell = cell;
            var mineClose = false;

            //loop through 4 times checking the cells around the current cell, if a neighbor cell has a mine then return true; 
            for (int i = 1; i < 4; i++)
            {
                tempCell = cell;
                switch (i)
                {
                    case 1:
                        tempCell.xPosition--;
                        break;

                    case 2:
                        tempCell.xPosition++;
                        break;

                    case 3:
                        tempCell.yPosition++;
                        break;

                    case 4:
                        tempCell.yPosition--;
                        break;
                }

                //Off the board
                if (tempCell.xPosition < 0 || tempCell.yPosition > 7)
                    continue;

                mineClose = tempCell.HasMine;

                if (mineClose)
                    return mineClose;
            }
            return mineClose;
        }
    }
}

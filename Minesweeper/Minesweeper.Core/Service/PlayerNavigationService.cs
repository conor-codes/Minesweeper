using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Core.Interfaces;
using Minesweeper.Models;
using Minesweeper.Models.Enums;

namespace Minesweeper.Core.Service
{
    public class PlayerNavigationService : IPlayerNavigationService
    {
        public bool MovePlayer(ref Player player, Cell[,] grid, PlayerDirection move)
        {
            // Create a new instance so we don't directly edit the ref unless needed.
            var nextMove = new Cell(player.CurrentPosition.xPosition, player.CurrentPosition.yPosition);
            var yPos = 0;
            var xPos = 0;
           
            switch (move)
            {
                case PlayerDirection.Up:
                    yPos--;
                    break;
                case PlayerDirection.Down:
                    yPos++;
                    break;
                case PlayerDirection.Left:
                    xPos--;
                    break;
                case PlayerDirection.Right:
                    xPos++;
                    break;
            }

            nextMove.xPosition = nextMove.xPosition + xPos;
            nextMove.yPosition = nextMove.yPosition + yPos;

            //Check if it's a legal move, still on the board
            if (Enumerable.Range(0, 8).Contains(nextMove.xPosition) &&
               (Enumerable.Range(0, 8).Contains(nextMove.yPosition)))
            {
                player.CurrentPosition = nextMove;
                return true;
            }

            return false;
        }

        //Handle user input and output direction
        public PlayerDirection PlayerInput()
        {
            ConsoleKey choice;
            choice = Console.ReadKey(true).Key;
            switch (choice)
            {
                case ConsoleKey.UpArrow:
                    return PlayerDirection.Up;

                case ConsoleKey.DownArrow:
                    return PlayerDirection.Down;
                   
                case ConsoleKey.LeftArrow:
                    return PlayerDirection.Left;

                case ConsoleKey.RightArrow:
                    return PlayerDirection.Right;

                default:
                    return PlayerDirection.Error;
            }
        }
    }
}

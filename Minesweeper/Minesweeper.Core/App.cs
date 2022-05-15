using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Core.Models;
using Minesweeper.Core.Models.Enums;

namespace Minesweeper.Core
{
    public class App
    {
        private IGridGeneratorService gridGeneratorService;
        private IConsoleDisplayService consoleDisplayService;
        private IPlayerNavigationService playerNavigationService;
        private ICollisionDetectorService collisionDetectorService;

        public App(IGridGeneratorService gridGeneratorService, IConsoleDisplayService consoleDisplayService,
            IPlayerNavigationService playerNavigationService, ICollisionDetectorService collisionDetectorService)
        {
            this.gridGeneratorService = gridGeneratorService;
            this.consoleDisplayService = consoleDisplayService;
            this.playerNavigationService = playerNavigationService;
            this.collisionDetectorService = collisionDetectorService;
        }

        private Player player;
        private Cell[,] grid;

        public void OnStart()
        {
            SetUp();
            Running();
            Finished();
        }

        private void SetUp()
        {
            consoleDisplayService.DisplayMessage("Welcome to Minesweeper!");
            consoleDisplayService.DisplayMessage("Please enter your name: ");

            gridGeneratorService.GenerateGrid();
            gridGeneratorService.GenerateMines();

            var name = Console.ReadLine();
            player = new Player() { GameProgress = Models.Enums.GameState.InProgress, Lives = 3, Name = name, Moves = 0, CurrentPosition = new Cell(0,0)};
            grid = gridGeneratorService.GetGrid();

            consoleDisplayService.DisplayMessage("The game has started, you are currently at A,1");
        }


        private void Running()
        {
            try
            {
                do
                {
                    // Gets the users keyboard input
                    var move = playerNavigationService.PlayerInput();
                    if (move == PlayerDirection.Error)
                    {
                        consoleDisplayService.DisplayMessage("Invalid key press, please use the arrow buttons");
                        continue;
                    }

                    //Moves the plater in the direction of the keypress
                    var canMove = playerNavigationService.MovePlayer(ref player, grid, move);
                    if (!canMove)
                        continue;

                    player.Moves++;

                    //Added plus 1, to display in chess terms A,1
                    consoleDisplayService.DisplayMessage($"Player moved to: {consoleDisplayService.ConvertPosToAlphabet(player.CurrentPosition.xPosition)}" +
                        $", { player.CurrentPosition.yPosition + 1} Lives: {player.Lives} Moves: {player.Moves}");
                   
                    //Checks if the player has landed on a mine
                    var landedOnMine = collisionDetectorService.DetectIfPlayerLandsOnMine(player, grid);
                    if (landedOnMine)
                    {
                        player.Lives--;
                        consoleDisplayService.DisplayMessage($"Oops, you landed on a mine! You have {player.Lives} lives remaining");

                        //Remove the mine once collided
                        var refToCell = grid.GetValue(player.CurrentPosition.xPosition, player.CurrentPosition.yPosition) as Cell;
                        refToCell.HasMine = false;
                    }

                    //Detect if a mine is close
                    var mineClose = collisionDetectorService.DetectIfPlayerIsCloseToAMine(player, grid);
                    if (mineClose)
                    {
                        consoleDisplayService.DisplayMessage($"Psst, watch your step there is a mine close by!");
                    }

                    //Check the game state.
                    player.GameProgress = player.Lives == 0 ? GameState.Lose : GameState.InProgress;
                    if (player.GameProgress != GameState.InProgress)
                        continue;                    

                    player.GameProgress = player.CurrentPosition.yPosition == 8 ? GameState.Won : GameState.InProgress;

                } while (player.GameProgress == Models.Enums.GameState.InProgress);
            }
            catch(Exception ex)
            {
                consoleDisplayService.DisplayMessage($"An error has occured: {ex.Message} ");
            }
        }

        private void Finished()
        {
            consoleDisplayService.DisplayMessage($"Game over {player.Name}! Result: {player.GetType().GetProperty("GameProgress").GetValue(player, null)}, " +
                $"Lives: {player.Lives}, Move Count: {player.Moves}");
            Console.ReadLine();
        }
    }
}

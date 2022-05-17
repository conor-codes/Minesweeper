using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Models;
using Minesweeper.Models.Enums;

namespace Minesweeper.Core
{
    public class App
    {
        private IGridGeneratorService gridGeneratorService;
        private IConsoleDisplayService consoleDisplayService;
        private IPlayerNavigationService playerNavigationService;
        private ICollisionDetectorService collisionDetectorService;
        private IGameStateService gameStateService;

        public App(IGridGeneratorService gridGeneratorService, IConsoleDisplayService consoleDisplayService,
            IPlayerNavigationService playerNavigationService, ICollisionDetectorService collisionDetectorService,
            IGameStateService gameStateService)
        {
            this.gridGeneratorService = gridGeneratorService;
            this.consoleDisplayService = consoleDisplayService;
            this.playerNavigationService = playerNavigationService;
            this.collisionDetectorService = collisionDetectorService;
            this.gameStateService = gameStateService;
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
            //Sets up the initial values, and displays user prompts
            consoleDisplayService.DisplayMessage("Welcome to Minesweeper!");
            consoleDisplayService.DisplayMessage("Please enter your name: ");

            gridGeneratorService.GenerateGrid();
            gridGeneratorService.GenerateMines();

            var name = Console.ReadLine();
            player = new Player() { GameProgress = Models.Enums.GameState.InProgress, Lives = 3, Name = name, Moves = 0, CurrentPosition = new Cell(0,0)};
            grid = gridGeneratorService.GetGrid();

            consoleDisplayService.DisplayMessage("The game has started, you are currently at A,1! Please use the arrow buttons to move the player");
        }


        private void Running()
        {
            try
            {
                do
                {
                    // Gets the players keyboard input
                    var move = playerNavigationService.PlayerInput();
                    if (move == PlayerDirection.Error)
                    {
                        consoleDisplayService.DisplayMessage("Invalid keypress, please use the arrow buttons");
                        continue;
                    }

                    //Moves the player in the direction of the keypress
                    var canMove = playerNavigationService.MovePlayer(ref player, grid, move);
                    if (!canMove)
                        continue;
   
                    //Checks if the player has landed on a mine
                    var landedOnMine = collisionDetectorService.DetectIfPlayerLandsOnMine(player, grid);

                    //Check the current gamestate, and update values
                    gameStateService.UpdateGameState(ref player, landedOnMine, ref grid);

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

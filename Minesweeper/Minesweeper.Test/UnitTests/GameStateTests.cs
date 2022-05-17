using System;
using System.Collections.Generic;
using Minesweeper.Core.Service;
using Minesweeper.Models;
using Xunit;

namespace Minesweeper.Test.UnitTests
{
    public class GameStateTests
    {
        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void GameState_MoveIncremented(Player playerInfo)
        {
            //Arrange
            var consoleDisplayService = new ConsoleDisplayService();
            var gameStateService = new GameStateService(consoleDisplayService);
            var gridGeneratorService = new GridGeneratorService();

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            gameStateService.UpdateGameState(ref playerInfo, false, ref grid);

            //Assert
            Assert.Equal(1,playerInfo.Moves);
        }

        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void GameState_TakeAwayLife(Player playerInfo)
        {
            //Arrange
            var consoleDisplayService = new ConsoleDisplayService();
            var gameStateService = new GameStateService(consoleDisplayService);
            var gridGeneratorService = new GridGeneratorService();

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            gameStateService.UpdateGameState(ref playerInfo, true, ref grid);

            //Assert
            Assert.Equal(2, playerInfo.Lives);
        }

        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void GameState_Lost(Player playerInfo)
        {
            //Arrange
            var consoleDisplayService = new ConsoleDisplayService();
            var gameStateService = new GameStateService(consoleDisplayService);
            var gridGeneratorService = new GridGeneratorService();
            playerInfo.Lives = 1;

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            gameStateService.UpdateGameState(ref playerInfo, true, ref grid);

            //Assert
            Assert.Equal(Models.Enums.GameState.Lose, playerInfo.GameProgress);
        }

        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void GameState_Won(Player playerInfo)
        {
            //Arrange
            var consoleDisplayService = new ConsoleDisplayService();
            var gameStateService = new GameStateService(consoleDisplayService);
            var gridGeneratorService = new GridGeneratorService();
            playerInfo.CurrentPosition.yPosition = 7;

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            gameStateService.UpdateGameState(ref playerInfo, false, ref grid);

            //Assert
            Assert.Equal(Models.Enums.GameState.Won, playerInfo.GameProgress);
        }

        public static IEnumerable<object[]> GetPlayerData()
        {
            yield return new object[] {
                new Player()
                {
                    CurrentPosition = new Cell(1, 1),
                    GameProgress = Models.Enums.GameState.InProgress,
                    Lives = 3,
                    Moves = 0,
                    Name = "Conor"
                }
            };
        }
    }
}

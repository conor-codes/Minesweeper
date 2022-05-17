using System;
using System.Collections.Generic;
using Minesweeper.Core.Service;
using Minesweeper.Models;
using Xunit;
using Xunit.Extensions;

namespace Minesweeper.Test.UnitTests
{
    public class CollisionDetectionTest
    {

        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void CollisionDetection_PlayerLandedOnMine(Player playerInfo)
        {
            //Arrange
            var collisionDetectionService = new CollisionDetectionService();
            var gridGeneratorService = new GridGeneratorService();
           
            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            (grid.GetValue(1,1) as Cell).HasMine = true;

            var result = collisionDetectionService.DetectIfPlayerLandsOnMine(playerInfo, grid);

            //Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetPlayerData))]
        public void CollisionDetection_PlayerDoesNotLandOnMine(Player playerInfo)
        {
            //Arrange
            var collisionDetectionService = new CollisionDetectionService();
            var gridGeneratorService = new GridGeneratorService();

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;
            (grid.GetValue(1, 1) as Cell).HasMine = false;

            var result = collisionDetectionService.DetectIfPlayerLandsOnMine(playerInfo, grid);

            //Assert
            Assert.False(result);
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

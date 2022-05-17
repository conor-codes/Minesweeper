using Minesweeper.Core.Service;
using Xunit;

namespace Minesweeper.Test.UnitTests
{
    public class GridGeneratorTests
    {
        [Fact]
        public void GenerateGrid_CheckGridHasValues()
        {
            //Arrange
            var gridGeneratorService = new GridGeneratorService();

            //Act
            gridGeneratorService.GenerateGrid();
            var grid = gridGeneratorService.Grid;

            //Assert
            Assert.Equal(64, grid.Length);
        }

        [Fact]
        public void GenerateMines_CheckMinesHaveBeenGenerated()
        {
            //Arrange
            var gridGeneratorService = new GridGeneratorService();
            var mines = 0;

            //Act
            gridGeneratorService.GenerateGrid();
            gridGeneratorService.GenerateMines();
            var grid = gridGeneratorService.Grid;

            foreach (var cell in grid)
            {
                if (cell.HasMine)
                    mines++;
            }

            //Assert
            Assert.Equal(12, mines);
        }
    }
}

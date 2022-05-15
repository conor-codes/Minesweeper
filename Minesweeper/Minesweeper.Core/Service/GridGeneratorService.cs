using System;
using Minesweeper.Core.Interfaces;
using Minesweeper.Core.Models;

namespace Minesweeper.Core.Service
{
    public class GridGeneratorService : IGridGeneratorService
    {
        // 8x8 chess board dimenson
        public const int DIMENSION = 8;

        // Number of mines places
        public const int MINECOUNT = 12;

        // delcare 2D array of cell //TODO: Sort this accesability out 
        public Cell[,] Grid { get; private set; }

        public void GenerateGrid()
        {
            Grid = new Cell[DIMENSION, DIMENSION];

            // Generate board
            for (int x = 0; x < DIMENSION; x++)
            {
                for (int y = 0; y < DIMENSION; y++)
                {
                    Grid[x, y] = new Cell(x, y);
                }
            }
        }

        public void GenerateMines()
        {
            var minesOnGrid = 0;
            var rand = new Random();

            while(minesOnGrid < MINECOUNT)
            {
                // Generate a random cell location
                var x = rand.Next(0, DIMENSION);
                var y = rand.Next(0, DIMENSION);

                //Don't place a mine on starting cell
                if (x == 0 && y == 0)
                    continue;

                //Get cell
                var cell = Grid[x, y];

                //if it doesn't have a mine add one to the current cell
                if (!cell.HasMine)
                {
                    cell.HasMine = true;
                    minesOnGrid++;
                }
            }
        }

        public Cell[,] GetGrid()
        {
            return Grid;
        }
    }
}

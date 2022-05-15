﻿using Minesweeper.Core.Models;

namespace Minesweeper.Core.Interfaces
{
    public interface IGridGeneratorService
    {
        void GenerateGrid();
        void GenerateMines();
   
        Cell[,] GetGrid();
    }
}

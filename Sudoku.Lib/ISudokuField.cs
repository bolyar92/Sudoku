using Sudoku.Lib.Entities;
using System.Collections.Generic;

namespace Sudoku.Lib
{
    public interface ISudokuField
    {
        bool IsSolved { get; }

        int[,] GetSudokuValue();

        SudokuCell GetNextCell();

        IEnumerable<SudokuCell> SetCellValue(int x, int y, int possibleValue);

        void ClearCellValue(int x, int y, int possibleValue, IEnumerable<SudokuCell> affectedCells);
    }
}

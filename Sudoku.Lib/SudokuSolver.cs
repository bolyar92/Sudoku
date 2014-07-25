using Sudoku.Lib.Entities;
using System.Linq;

namespace Sudoku.Lib
{
    public class SudokuSolver : ISudokuSolver
    {
        private const int FieldSize = 9;
        private ISudokuField sudoku;
        private bool isSolved;
        private int[,] solution;

        public SudokuSolver(ISudokuField sudokuField)
        {
            this.sudoku = sudokuField;
        }

        public void LoadSudoku(string[] sudokuConfiguration)
        {
            for (int x = 0; x < FieldSize; x++)
            {
                int y = 0;
                foreach (char value in sudokuConfiguration[x])
                {
                    if (value < '1' || value > '9')
                    {
                        if (value == '0')
                        {
                            y++;
                        }

                        continue;
                    }
                    this.sudoku.SetCellValue(x, y, int.Parse(value.ToString()));
                    y++;
                }
            }

            this.isSolved = false;
        }

        public int[,] SolveSudoku()
        {
            this.Solve();

            return this.solution;
        }

        private void Solve()
        {
            if (this.isSolved)
            {
                return;
            }
            if (this.sudoku.IsSolved && this.isSolved == false)
            {
                this.isSolved = true;
                this.solution = this.sudoku.GetSudokuValue();
            }

            SudokuCell cell = this.sudoku.GetNextCell();
            if (cell == null)
            {
                return;
            }

            var possibleValues = cell.PossibleValues.ToList();
            foreach (int possibleValue in possibleValues)
            {
                var affectedCells = this.sudoku.SetCellValue(cell.PositionX, cell.PositionY, possibleValue);

                Solve();

                this.sudoku.ClearCellValue(cell.PositionX, cell.PositionY, possibleValue, affectedCells);
            }
        }
    }
}

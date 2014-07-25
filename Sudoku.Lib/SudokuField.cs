using Sudoku.Lib.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Lib
{
    public class SudokuField : ISudokuField
    {
        public ICollection<SudokuCell> Cells { get; set; }

        public bool IsSolved
        {
            get
            {
                return this.Cells.Where(c => c.Value == 0).Count() == 0;
            }
        }

        public SudokuField()
        {
            this.Cells = new List<SudokuCell>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Cells.Add(new SudokuCell(x, y));
                }
            }
        }

        public int[,] GetSudokuValue()
        {
            int[,] values = new int[9, 9];

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    values[x, y] = Cells.First(c => c.PositionX == x && c.PositionY == y).Value;
                }
            }

            return values;
        }

        public SudokuCell GetNextCell()
        {
            return this.GetNextCell(1);
        }

        public IEnumerable<SudokuCell> SetCellValue(int x, int y, int possibleValue)
        {
            var cell = this.Cells.Single(c => c.PositionX == x && c.PositionY == y);
            cell.Value = possibleValue;

            var affectedCells = this.Cells.Where(c => c.PositionX == cell.PositionX ||
                                                      c.PositionY == cell.PositionY ||
                                                      c.SquareId == cell.SquareId).
                                           Where(c => c.PossibleValues.Contains(possibleValue)).ToList();

            foreach (var affectedCell in affectedCells)
            {
                affectedCell.PossibleValues.Remove(possibleValue);
            }
            

            return affectedCells;
        }

        public void ClearCellValue(int x, int y, int possibleValue, IEnumerable<SudokuCell> affectedCells)
        {
            var cell = this.Cells.Single(c => c.PositionX == x && c.PositionY == y);

            cell.Value = 0;
            foreach (var affectedCell in affectedCells)
            {
                affectedCell.PossibleValues.Add(possibleValue);
            }
        }

        private SudokuCell GetNextCell(int possibleValuesCount)
        {
            if (possibleValuesCount == 10)
            {
                return null;
            }

            return this.Cells.FirstOrDefault(c => c.PossibleValues.Count() == possibleValuesCount && c.Value == 0) ?? this.GetNextCell(possibleValuesCount + 1);
        }
    }
}

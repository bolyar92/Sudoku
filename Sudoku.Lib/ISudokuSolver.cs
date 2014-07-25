namespace Sudoku.Lib
{
    public interface ISudokuSolver
    {
        void LoadSudoku(string[] sudokuConfiguration);

        int[,] SolveSudoku();
    }
}

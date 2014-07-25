using Sudoku.Lib;
using Sudoku.Lib.Configuration;
using System;
using System.Diagnostics;

namespace Sudoku.App
{
    class Program
    {

        private static void PrintSudoku(int[,] sudoku)
        {
            Console.WriteLine("----------------------");

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(sudoku[i, j] + " ");
                    if (j % 3 == 2)
                    {
                        Console.Write(" ");
                    }
                }
                if (i % 3 == 2)
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            ISudokuConfigurationProvider configProvider = new SudokuConfigurationProvider();

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

            ISudokuSolver sudokuSolver = new SudokuSolver(new SudokuField());
            sudokuSolver.LoadSudoku(configProvider.GetSudokuConfiguration());

            int[,] solution = sudokuSolver.SolveSudoku();

			stopwatch.Stop();
			PrintSudoku(solution);

			Console.WriteLine(stopwatch.ElapsedMilliseconds);
			Console.ReadKey();
        }
    }
}

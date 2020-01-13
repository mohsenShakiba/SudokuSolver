using System;
using System.Linq;
using Sudoku.Models;
using Sudoku.Presentation;
using Sudoku.Solver;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var chart = new Chart(3);

            var generator = new ChartGenerator();
            
            generator.Generate(chart, 40, 1);

            var presentation = new StringPresentation();
            presentation.Present(chart);

            var solver = new SudokuSolver();
            solver.Solve(chart, null);
            
            presentation.Present(chart);

            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    continue;
                var values = input.Split(",").Select(s => int.Parse(s)).ToArray();
                if (values.Count() != 3)
                {
                    Console.WriteLine("bad input");
                    continue;
                }

                var success = generator.AddInputManually(chart, values[0], values[1], values[2]);
                
                Console.WriteLine($"manual input was {success}");

                presentation.Present(chart);
  
            }
        }
    }
}
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using Sudoku.Loader;
using Sudoku.Models;
using Sudoku.Presentation;
using Sudoku.Solver;

namespace Sudoku
{
    class Program
    {

        static void Main(string[] args)
        {

            var loader = new FileLoader(@"..\..\..\Examples\easy_1.txt", 3);

            var chart = loader.Load();
            
            var presentation = new ConsolePresentation();
            
            var solver = new BackTrackSolver();
            var solvedChart = solver.Solve(chart);
            
            presentation.Present(solvedChart);
            
        }
    }

}
using System;
using Sudoku.Models;
using Sudoku.Presentation;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var chart = new Chart(3);

            var generator = new ChartGenerator();
            
            generator.Generate(chart, 25, 1);

            var presentation = new StringPresentation();
            presentation.Present(chart);
        }
    }
}
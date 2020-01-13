using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var chart = new Chart(3);

            var generator = new SudokuGenerator();
            
            generator.Generate(chart, 25, 1);

            Console.WriteLine(chart.RepresentInString());
        }
    }
}
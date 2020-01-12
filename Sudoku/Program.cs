using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var chart = new Chart(3);

            foreach (var square in chart.Squares)
            {
                RandomInputGenerator.Permutation(chart, square, 5);
            }

            Console.WriteLine(chart.RepresentInString());
            
            RandomInputGenerator.RemoveViolatingRows(chart);
            
            Console.WriteLine(chart.RepresentInString());

            
        }
    }
}
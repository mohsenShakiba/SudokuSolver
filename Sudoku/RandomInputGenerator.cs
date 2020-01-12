using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class RandomInputGenerator
    {

        public static void Permutation(Chart chart, Square square, int minCount)
        {
            var rnd = new Random(1000);
            while (square.Count() < minCount)
            {
                var row  = rnd.Next(1, square.Size + 1);
                var column  = rnd.Next(1, square.Size + 1);
                var input = square.GetInput(row, column);
                if (input.Value != null)
                    continue;
                var value  = rnd.Next(1, square.Size * square.Size);
                input.Value = value;
                if (!chart.IsInputValidForSquare(square, input))
                {
                    input.Value = null;
                }
            }
        }

        public static void RemoveViolatingRows(Chart chart)
        {
            foreach (var square in chart.Squares)
            {
                var neighboringRows = chart.NeighbourRowSquares(square);
                var neighboringColumns = chart.NeighbourColumnSquares(square);
                var distinctValues = new List<int>();
                var distinctRowValues = neighboringRows.SelectMany(s => s.Inputs).Where(i => i.Value.HasValue).Select(i => i.Value.Value).Distinct();
                var distinctColumnValues = neighboringColumns.SelectMany(s => s.Inputs).Where(i => i.Value.HasValue).Select(i => i.Value.Value).Distinct();
                distinctValues.AddRange(distinctRowValues);
                distinctValues.AddRange(distinctColumnValues);
                distinctValues = distinctValues.Distinct().ToList(); 
                
                foreach (var distinctValue in distinctValues)
                {
                    var matchingRows = neighboringRows.SelectMany(s => s.Inputs).Where(i => i.Value == distinctValue).Select(i => i.Row);
                    var matchingColumns = neighboringColumns.SelectMany(s => s.Inputs).Where(i => i.Value == distinctValue).Select(i => i.Column);
                    var results = square.Inputs
                        .Where(i => !matchingRows.Contains(i.Row))
                        .Where(i => !matchingColumns.Contains(i.Column));
                    if (!results.Any(i => i.Value == null))
                        results.First().Value = null;
                }
            }
        }
        
    }
}
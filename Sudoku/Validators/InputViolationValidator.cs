using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class InputViolationValidator: IValidator
    {
        public bool IsValid(Chart chart, Box box, Input input)
        {
            foreach (var square in chart.Boxes)
            {
                var neighboringRows = chart.NeighbourBoxesInRow(square);
                var neighboringColumns = chart.NeighbourBoxesInColumn(square);
                var distinctValues = new List<int>();
                var distinctRowValues = neighboringRows.SelectMany(s => s.Inputs).Where(i => i.HasValue).Select(i => i.GetValue).Distinct();
                var distinctColumnValues = neighboringColumns.SelectMany(s => s.Inputs).Where(i => i.HasValue).Select(i => i.GetValue).Distinct();
                distinctValues.AddRange(distinctRowValues);
                distinctValues.AddRange(distinctColumnValues);
                distinctValues = distinctValues.Distinct().ToList(); 
                
                foreach (var distinctValue in distinctValues)
                {
                    var matchingRows = neighboringRows
                        .SelectMany(s => s.Inputs)
                        .Where(i => i != input)
                        .Where(i => i.Row == input.Row)
                        .Where(i => i.HasValue && i.Value == distinctValue)
                        .Select(i => i.Row);
                    var matchingColumns = neighboringColumns
                        .SelectMany(s => s.Inputs)
                        .Where(i => i != input)
                        .Where(i => i.Column == input.Column)
                        .Where(i => i.HasValue && i.Value == distinctValue)
                        .Select(i => i.Column);
                    var results = square.Inputs
                        .Where(i => !matchingRows.Contains(i.Row))
                        .Where(i => !matchingColumns.Contains(i.Column));
                    if (results.All(i => i.HasValue && i.Value != input.Value))
                        return false;
                }
            }
            return true;
        }
    }
}
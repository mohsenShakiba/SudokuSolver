using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Permutation.Validators
{
    public class InputViolationValidator: IGenerationValidator
    {
        public bool Validate(Chart chart, Box box, Input input)
        {
            foreach (var square in chart.Boxes)
            {
                var neighboringRows = chart.NeighbourRowBoxes(square);
                var neighboringColumns = chart.NeighbourColumnBoxes(square);
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
                        return false;
                }
            }
            return true;
        }
    }
}
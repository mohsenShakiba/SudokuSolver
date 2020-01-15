using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class ColumnValidator: IValidator
    {
        public bool IsValid(Chart chart, Input input)
        {
            var columnBoxes = chart.NeighbourBoxesInColumn(input);
            foreach (var columnBox in columnBoxes)
                if (columnBox.Inputs.Any(i => i.Column == input.Column && i.HasValue && i.Value == input.Value))
                    return false;
            return true;
        }
    }
}
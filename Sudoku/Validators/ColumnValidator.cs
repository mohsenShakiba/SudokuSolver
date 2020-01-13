using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class ColumnValidator: IValidator
    {
        public bool IsValid(Chart chart, Box box, Input input)
        {
            var columnBoxes = chart.NeighbourColumnBoxes(box);
            foreach (var columnBox in columnBoxes)
                if (columnBox.Inputs.Any(i => i.Column == input.Column && i.Value == input.Value))
                    return false;
            return true;
        }
    }
}
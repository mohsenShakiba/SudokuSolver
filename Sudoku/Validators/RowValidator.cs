using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class RowValidator: IValidator
    {
        public bool IsValid(Chart chart, Box box, Input input)
        {
            var rowBoxes = chart.NeighbourBoxesInRow(box);
            foreach (var rowBox in rowBoxes)
                if (rowBox.Inputs.Any(i => i.Row == input.Row && i.Value == input.Value && i.HasValue))
                    return false;
            return true;
        }
    }
}
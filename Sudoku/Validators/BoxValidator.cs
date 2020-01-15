using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class BoxValidator: IValidator
    {
        public bool IsValid(Chart chart, Input input)
        {
            var box = chart.BoxForInput(input);
            return box.Inputs.Where(i => i != input && i.HasValue).All(i => i.Value != input.Value);
        }
    }
}
using System.Linq;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class BoxValidator: IValidator
    {
        public bool IsValid(Chart chart, Box box, Input input)
        {
            return box.Inputs.Where(i => i != input && i.HasValue).All(i => i.Value != input.Value);
        }
    }
}
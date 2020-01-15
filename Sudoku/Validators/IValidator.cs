using Sudoku.Models;

namespace Sudoku.Validators
{
    public interface IValidator
    {
        bool IsValid(Chart chart, Input input);
    }
}
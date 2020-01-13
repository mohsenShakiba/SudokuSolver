using Sudoku.Models;

namespace Sudoku.Validators
{
    public interface IValidator
    {
        bool IsValid(Chart chart, Box box, Input input);
    }
}
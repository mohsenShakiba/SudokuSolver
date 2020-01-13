namespace Sudoku.Permutation.Validators
{
    public interface IGenerationValidator
    {
        bool Validate(Chart chart, Box box, Input input);
    }
}
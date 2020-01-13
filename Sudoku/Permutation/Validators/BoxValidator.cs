namespace Sudoku.Permutation.Validators
{
    public class BoxValidator: IGenerationValidator
    {
        public bool Validate(Chart chart, Box box, Input input)
        {
            return box.IsInputValidInBox(input);
        }
    }
}
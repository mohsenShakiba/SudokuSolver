namespace Sudoku.Permutation.Validators
{
    public class ColumnValidator: IGenerationValidator
    {
        public bool Validate(Chart chart, Box box, Input input)
        {
            return chart.IsInputValidForBox(box, input);
        }
    }
}
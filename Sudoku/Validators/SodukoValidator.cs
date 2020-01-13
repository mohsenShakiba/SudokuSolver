using System.Collections.Generic;
using Sudoku.Models;

namespace Sudoku.Validators
{
    public class SodukoValidator: IValidator
    {
        private readonly List<IValidator> _validators = new List<IValidator>();

        public SodukoValidator()
        {
            // adding validators
            _validators.Add(new BoxValidator());
            _validators.Add(new ColumnValidator());
            _validators.Add(new RowValidator());
            _validators.Add(new InputViolationValidator());
        }

        public bool IsValid(Chart chart, Box box, Input input)
        {
            foreach (var validator in _validators)
                if (!validator.IsValid(chart, box, input))
                    return false;
            return true;
        }
    }
}
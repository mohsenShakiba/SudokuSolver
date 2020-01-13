using System.Collections.Generic;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku.Generation.Validation
{
    public class GenerationValidatorPipeline: IValidator
    {
        private readonly List<IValidator> _validators;

        public GenerationValidatorPipeline()
        {
            _validators = new List<IValidator>();
        }

        public void AddValidator(IValidator validator)
        {
            _validators.Add(validator);
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
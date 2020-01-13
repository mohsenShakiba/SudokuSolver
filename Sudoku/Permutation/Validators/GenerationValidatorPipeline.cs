using System.Collections.Generic;

namespace Sudoku.Permutation.Validators
{
    public class GenerationValidatorPipeline: IGenerationValidator
    {
        private readonly List<IGenerationValidator> _validators;

        public GenerationValidatorPipeline()
        {
            _validators = new List<IGenerationValidator>();
        }

        public void AddValidator(IGenerationValidator validator)
        {
            _validators.Add(validator);
        }

        public bool Validate(Chart chart, Box box, Input input)
        {
            foreach (var validator in _validators)
                if (!validator.Validate(chart, box, input))
                    return false;
            return true;
        }
    }
}
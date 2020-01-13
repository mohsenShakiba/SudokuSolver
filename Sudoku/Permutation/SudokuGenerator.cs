using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Permutation.Validators;

namespace Sudoku
{
    public class SudokuGenerator
    {

        private GenerationValidatorPipeline _generationValidatorPipeline;
        
        public SudokuGenerator()
        {
            _generationValidatorPipeline = new GenerationValidatorPipeline();
            _generationValidatorPipeline.AddValidator(new BoxValidator());
            _generationValidatorPipeline.AddValidator(new RowValidator());
            _generationValidatorPipeline.AddValidator(new ColumnValidator());
            _generationValidatorPipeline.AddValidator(new InputViolationValidator());
        }

        public void Generate(Chart chart, int minimumRequiredGeneratedNumbers, int? seedValue)
        {
            var random = seedValue.HasValue ? new Random(seedValue.Value) : new Random();
            
            if (minimumRequiredGeneratedNumbers <= 0)
                throw new InvalidOperationException($"Invalid valud for {nameof(minimumRequiredGeneratedNumbers)}");

            while (chart.Count() < minimumRequiredGeneratedNumbers)
            {
                // select a random box
                var randomlySelectedBoxIndex = random.Next(1, (int)Math.Pow(chart.Size, 2));
                var randomlySelectedBox = chart.Boxes[randomlySelectedBoxIndex];
                
                // randomly select an input
                var randomlySelectedInputIndex = random.Next(1, (int)Math.Pow(chart.Size, 2));
                var randomlySelectedInput = randomlySelectedBox.Inputs[randomlySelectedInputIndex];
                
                // check if input is empty
                if (randomlySelectedInput.Value.HasValue)
                    continue;

                var randomlySelectedValue = random.Next(1, (int) Math.Pow(chart.Size, 2));
                randomlySelectedInput.Value = randomlySelectedValue;

                if (!_generationValidatorPipeline.Validate(chart, randomlySelectedBox, randomlySelectedInput))
                {
                    randomlySelectedInput.Value = null;
                }

            }
        }
        
//        public static void Permutation(Chart chart, Box box, int minCount)
//        {
//            while (box.Count() < minCount)
//            {
//                var row  = rnd.Next(1, box.Size + 1);
//                var column  = rnd.Next(1, box.Size + 1);
//                var input = box.GetInput(row, column);
//                if (input.Value != null)
//                    continue;
//                var value  = rnd.Next(1, box.Size * box.Size);
//                input.Value = value;
//                if (!chart.IsInputValidForBox(box, input))
//                {
//                    input.Value = null;
//                }
//            }
//        }

    }
}
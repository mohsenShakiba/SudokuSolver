using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Generation.Validation;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku
{
    public class ChartGenerator
    {

        private GenerationValidatorPipeline _generationValidatorPipeline;
        
        public ChartGenerator()
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

                // create a random value
                var randomlySelectedValue = random.Next(1, (int) Math.Pow(chart.Size, 2));
                randomlySelectedInput.Value = randomlySelectedValue;
                
                // validate the new input
                // if the input is invalid, remove it
                if (!_generationValidatorPipeline.IsValid(chart, randomlySelectedBox, randomlySelectedInput))
                {
                    randomlySelectedInput.Value = null;
                }

            }
        }
        
    }
}
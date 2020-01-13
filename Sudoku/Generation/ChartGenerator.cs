using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku
{
    public class ChartGenerator
    {

        private IValidator _validator = new SodukoValidator();
        

        public bool AddInputManually(Chart chart, int i, int j, int value)
        {
            var selectedRow = chart.Rows.ElementAt(i - 1);
            var selectedInput = selectedRow.Inputs.ElementAt(j - 1);

            selectedInput.Value = value;
            
            if (!_validator.IsValid(chart, selectedInput.Box, selectedInput))
            {
                selectedInput.Value = null;
                return false;
            } 
            return true;
        }

        public void Generate(Chart chart, int minimumRequiredGeneratedNumbers, int? seedValue)
        {
            var random = seedValue.HasValue ? new Random(seedValue.Value) : new Random();
            
            if (minimumRequiredGeneratedNumbers <= 0)
                throw new InvalidOperationException($"Invalid valud for {nameof(minimumRequiredGeneratedNumbers)}");

            var maxCount = 1000;
            var currentCount = 0;
            
            
            while (chart.Count() < minimumRequiredGeneratedNumbers)
            {

                currentCount += 1;
                
                if (currentCount >= maxCount)
                    return;
                
                // select a random box
                var randomlySelectedBoxIndex = random.Next(1, chart.Size + 1);
                var randomlySelectedBox = chart.Boxes[randomlySelectedBoxIndex - 1];
                
                // randomly select an input
                var randomlySelectedInputIndex = random.Next(1, chart.Size + 1);
                var randomlySelectedInput = randomlySelectedBox.Inputs[randomlySelectedInputIndex - 1];
                
                // check if input is empty
                if (randomlySelectedInput.HasValue)
                    continue;

                // create a random value
                var randomlySelectedValue = random.Next(1, chart.Size + 1);
                randomlySelectedInput.Value = randomlySelectedValue;
                
                // validate the new input
                // if the input is invalid, remove it
                if (!_validator.IsValid(chart, randomlySelectedBox, randomlySelectedInput))
                {
                    randomlySelectedInput.Value = null;
                }
                else
                {
                    Console.WriteLine($"size is {chart.Count()}");
                }
                
            }
        }
        
    }
}
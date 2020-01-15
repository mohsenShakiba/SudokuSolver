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
            
            if (!_validator.IsValid(chart, selectedInput))
            {
                selectedInput.Value = null;
                return false;
            } 
            return true;
        }

        public void Generate(Chart chart, int? seedValue)
        {
            var random = seedValue.HasValue ? new Random(seedValue.Value) : new Random();
            
            var maxCount = 10000;
            var currentCount = 0;

            var currentNumber = 1;
            
            while (chart.Count < (int)Math.Pow(chart.Size, 2))
            {

                currentCount += 1;
                
                if (currentCount >= maxCount)
                    return;
                
                // select a random box
                var randomlySelectedBoxIndex = random.Next(1, chart.Size + 1);
                var randomlySelectedBox = chart.Boxes.ElementAt(randomlySelectedBoxIndex - 1);
                
                // randomly select an input
                var randomlySelectedInputIndex = random.Next(1, chart.Size + 1);
                var randomlySelectedInput = randomlySelectedBox.Inputs.ElementAt(randomlySelectedInputIndex - 1);
                
                // check if input is empty
                if (randomlySelectedInput.HasValue)
                    continue;

                // set value for selected input
                randomlySelectedInput.Value = currentNumber;
                
                // validate the new input
                // if the input is invalid, remove it
                if (!_validator.IsValid(chart, randomlySelectedInput))
                {
                    randomlySelectedInput.Value = null;
                }
                else
                {
                    Console.WriteLine($"size is {chart.Count}");
                }

                Console.WriteLine(chart.CountFor(currentNumber));

                if (chart.CountFor(currentNumber) == chart.Size)
                    currentNumber += 1;
                
                if (currentNumber > chart.Size)
                    break;
            }
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku.Solver.Strategies
{
    public class SimpleBoxStrategy: ISolverStrategy
    {
        
        public int Solve(Chart chart, IValidator validator)
        {
            var numberOfInputsSolved = 0;
            foreach (var box in chart.Boxes)
            {
                if (box.Count == box.Size - 1)
                {
                    var allValues = new List<int>();

                    for (int i = 0; i < box.Size; i++)
                    {
                        allValues.Add(i + 1);
                    }

                    var selectedValues = box.Inputs.Where(i => i.HasValue).Select(i => i.GetValue);

                    var remainingValue = allValues.Except(selectedValues).First();

                    var remainingInput = box.Inputs.First(i => !i.HasValue);

                    remainingInput.Value = remainingValue;
                    
                    if (!validator.IsValid(chart, remainingInput))
                    {
                        remainingInput.Value = null;
                        continue;
                    }

                    Console.WriteLine("1 was solved using simple box strategy");

                    numberOfInputsSolved += 1;
                }
            }

            return numberOfInputsSolved;
        }
        
    }
}
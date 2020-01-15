using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku.Solver.Strategies
{
    public class SimpleColumnStrategy: ISolverStrategy
    {
        public int Solve(Chart chart, IValidator validator)
        {
            var numberOfInputsSolved = 0;
            foreach (var column in chart.Columns)
            {
                if (column.Count() == column.Size - 1)
                {
                    var allValues = new List<int>();

                    for (int i = 0; i < column.Size; i++)
                    {
                        allValues.Add(i + 1);
                    }

                    var selectedValues = column.Inputs.Where(i => i.HasValue).Select(i => i.GetValue);

                    var remainingValue = allValues.Except(selectedValues).First();

                    var remainingInput = column.Inputs.First(i => !i.HasValue);

                    remainingInput.Value = remainingValue;
                    
                    if (!validator.IsValid(chart, remainingInput))
                    {
                        remainingInput.Value = null;
                        continue;
                    }

                    Console.WriteLine("1 was solved using simple column strategy");

                    numberOfInputsSolved += 1;
                } 
            }
            return numberOfInputsSolved;
        }
    }
}
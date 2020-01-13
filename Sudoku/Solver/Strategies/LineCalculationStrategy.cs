using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku.Solver.Strategies
{
    public class LineCalculationStrategy: ISolverStrategy
    {
        public int Solve(Chart chart, IValidator validator)
        {
            var numberOfInputsSolved = 0;
            
            // get all values 
            var allValues = new List<int>();
            for (int i = 0; i < chart.Size; i++)
            {
                allValues.Add(i + 1);
            }
            
            foreach (var box in chart.Boxes)
            {
                // find values that haven't been set in the box
                var selectedValues = box.Inputs.Where(i => i.HasValue && !allValues.Contains(i.GetValue)).Select(i => i.GetValue);

                var remainingValues = allValues.Except(selectedValues);

                foreach (var remainingValue in remainingValues)
                {
                    // get neighbouring boxes in row
                    var neighbouringBoxesInRow = chart.NeighbourBoxesInRow(box);
                    
                    // get neighbouring boxes in column
                    var neighbouringBoxesInColumn = chart.NeighbourBoxesInColumn(box);
                    
                    // get row of inputs with the same value in neighbouring rows
                    var inputRowsWithSameValueInNeighbouringBoxesInRow = neighbouringBoxesInRow
                        .SelectMany(b => b.Inputs)
                        .Where(i => i.HasValue)
                        .Where(i => i.GetValue == remainingValue)
                        .Select(i => i.Row);
                    
                    // get column of inputs with the same value in neighbouring column
                    var inputColumnsWithSameValueInNeighbouringBoxesInColumn = neighbouringBoxesInColumn
                        .SelectMany(b => b.Inputs)
                        .Where(i => i.HasValue)
                        .Where(i => i.GetValue == remainingValue)
                        .Select(i => i.Column);

                    var inputsFromBoxExcludedRowsAndColumnsFromNeighbouringInputs = box.Inputs
                        .Where(i => !i.HasValue)
                        .Where(i => !inputRowsWithSameValueInNeighbouringBoxesInRow.Contains(i.Row))
                        .Where(i => !inputColumnsWithSameValueInNeighbouringBoxesInColumn.Contains(i.Column));

                    if (inputsFromBoxExcludedRowsAndColumnsFromNeighbouringInputs.Count() == 1)
                    {
                        var input = inputsFromBoxExcludedRowsAndColumnsFromNeighbouringInputs.First();
                        input.Value = remainingValue;
                        
                        if (!validator.IsValid(chart, input.Box, input))
                        {
                            input.Value = null;
                            continue;
                        }

                        numberOfInputsSolved += 1;

                        Console.WriteLine("1 was solved using caclulate box strategy");
                    }
                }
            }
            return numberOfInputsSolved;
        }
    }
}
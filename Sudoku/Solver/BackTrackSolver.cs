using System.Collections.Generic;
using System.Linq;
using Sudoku.Models;
using Sudoku.Presentation;

namespace Sudoku.Solver
{
    
    /// <summary>
    /// this class will provide back tracking strategy to solve the sudoku
    /// </summary>
    public class BackTrackSolver: ISolver
    {
        public Chart Solve(Chart chart)
        {
            // if chart is complete return 
            if (chart.IsComplete)
                return chart;
            
            // get first empty input
            var firstEmptyInput = chart.Inputs.First(i => !i.HasValue);
            
            // provide a number for the input and check if the sudoku is solveable with the number
            foreach (var number in ValidNumbersForInput(chart, firstEmptyInput))
            {
                // set value of input
                firstEmptyInput.Value = number;
                
                // clone the chart
                var clonedChart = chart.Clone();
                
                // solve the sudoku with the given input in cloned chart
                var solvedChart = Solve(clonedChart);
                
                // if the chart could not be solved
                // clear the input
                // otherwise return
                if (solvedChart.IsFaulted)
                    firstEmptyInput.Clear();
                else
                    return solvedChart;
            }
            
            // the sudoku is invalid, return
            if (!firstEmptyInput.HasValue)
                chart.IsFaulted = true;
            
            // return the chart
            return chart;
        }

        /// <summary>
        /// return numbers that are valid for the given input
        /// </summary>
        /// <param name="chart"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        private IEnumerable<int> ValidNumbersForInput(Chart chart, Input input)
        {
            var numbers = Enumerable.Range(1, chart.Size);
        
            // remove numbers that exist in the box
            var box = chart.BoxForInput(input);
            numbers = numbers.Except(box.Inputs.Where(i => i.HasValue).Select(i => i.GetValue));
        
            // remove numbers that exist in the row
            var row = chart.RowForInput(input);
            numbers = numbers.Except(row.Inputs.Where(i => i.HasValue).Select(i => i.GetValue));
        
            // remove numbers that exist in the column
            var column = chart.ColumnForInput(input);
            numbers = numbers.Except(column.Inputs.Where(i => i.HasValue).Select(i => i.GetValue));

            return numbers;
        }

    }
}
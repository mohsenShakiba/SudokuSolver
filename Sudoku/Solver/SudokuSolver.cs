using System.Collections.Generic;
using Sudoku.Models;
using Sudoku.Solver.Strategies;
using Sudoku.Validators;

namespace Sudoku.Solver
{
    public class SudokuSolver: ISolverStrategy
    {

        private readonly List<ISolverStrategy> _strategies = new List<ISolverStrategy>();
        private readonly IValidator _validator = new SodukoValidator();
        
        public SudokuSolver()
        {
            // adding strategies
            _strategies.Add(new SimpleBoxStrategy());
            _strategies.Add(new SimpleRowStrategy());
            _strategies.Add(new SimpleColumnStrategy());
            _strategies.Add(new LineCalculationStrategy());
        }


        public int Solve(Chart chart, IValidator validator)
        {
            var totalSolved = 0;
            var currentIterationSolved = -1;
            while (currentIterationSolved != 0)
            {
                currentIterationSolved = 0;
                foreach (var strategy in _strategies)
                {
                    currentIterationSolved += strategy.Solve(chart, _validator);
                }

                totalSolved += currentIterationSolved;
            }
            return 0;
        }
    }
}
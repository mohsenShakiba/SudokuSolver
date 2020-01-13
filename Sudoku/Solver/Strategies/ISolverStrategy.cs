using Sudoku.Models;
using Sudoku.Validators;

namespace Sudoku.Solver.Strategies
{
    public interface ISolverStrategy
    {
        int Solve(Chart chart, IValidator validator);
    }
}
using Sudoku.Models;

namespace Sudoku.Solver
{
    /// <summary>
    /// interface for solving strategies
    /// </summary>
    public interface ISolver
    {
        Chart Solve(Chart chart);
    }
}
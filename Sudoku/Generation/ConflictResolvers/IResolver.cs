using Sudoku.Models;

namespace Sudoku.ConflictResolvers
{
    public interface IResolver
    {
        void ResolveConflict(Chart chart, Input input);
    }
}
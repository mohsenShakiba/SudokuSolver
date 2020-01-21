using Sudoku.Models;

namespace Sudoku.Presentation
{
    /// <summary>
    /// an interface for presenting the sudoku chart
    /// </summary>
    public interface IPresentation
    {
        void Present(Chart chart);
        void Clear();
    }
}
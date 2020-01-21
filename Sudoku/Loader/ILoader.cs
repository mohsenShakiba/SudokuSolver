using Sudoku.Models;

namespace Sudoku.Loader
{
    /// <summary>
    /// an interface for loading sudoku from sources such as file 
    /// </summary>
    public interface ILoader
    {
        Chart Load();
    }
}
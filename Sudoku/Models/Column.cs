using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Column
    {
        public IEnumerable<Input> Inputs { get; }
        public int Index { get; }
        
        public Column(int columnIndex, Chart chart)
        {
            Index = columnIndex;
            Inputs = chart.Inputs.Where(i => i.Column == columnIndex);
        }
        
        public override string ToString() => $"Row({Index})";
        
        public bool Contains(Input input) => Inputs.Contains(input);
        
    }
}
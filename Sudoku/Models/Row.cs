using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Row
    {
        public IEnumerable<Input> Inputs { get; }
        public int Index { get; }
        
        public Row(int rowIndex, Chart chart)
        {
            Index = rowIndex;
            Inputs = chart.Inputs.Where(i => i.Row == rowIndex);
        }

        public override string ToString() => $"Row({Index})";
        
        public bool Contains(Input input) => Inputs.Contains(input);

    }
}
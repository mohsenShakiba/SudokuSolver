using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Row
    {
        public IEnumerable<Input> Inputs { get; }
        public int Class { get; }
        public int Index { get; }
        public int Size => (int)Math.Pow(Class, 2);
        
        public int Count() => Inputs.Count(i => i.HasValue);
        
        public Row(int rowIndex, Chart chart)
        {
            Index = rowIndex;
            Class = chart.Class;
            Inputs = chart.Inputs.Where(i => i.Row == rowIndex);
        }

        public override string ToString() => $"Row({Index})";
        
        public bool Contains(Input input) => Inputs.Contains(input);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Column
    {
        public IEnumerable<Input> Inputs { get; }
        public int Class { get; }
        public int Index { get; }
        public int Size => (int)Math.Pow(Class, 2);
        
        public Column(int columnIndex, Chart chart)
        {
            Index = columnIndex;
            Class = chart.Class;
            Inputs = chart.Inputs.Where(i => i.Column == columnIndex);
        }
        
        public int Count() => Inputs.Count(i => i.HasValue);

        public override string ToString() => $"Row({Index})";
        
        public bool Contains(Input input) => Inputs.Contains(input);
        
    }
}
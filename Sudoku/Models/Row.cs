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
        public Row(int rowIndex, Chart chart)
        {
            Index = rowIndex;
            Class = chart.Class;
            var boxRow = (rowIndex / 3) + 1;
            var inputRow = (rowIndex % 3) + 1;
            var columnRange = 0..chart.Class;
            var boxes = chart.Boxes.Where(s =>
                s.Row == boxRow);
            var inputs = boxes.SelectMany(i => i.Inputs).Where(i =>
                i.Row == inputRow &&
                i.Column >= columnRange.Start.Value && 
                i.Column <= columnRange.End.Value);

            Inputs = inputs;
        }
        
        public int Count()
        {
            return Inputs.Count(i => i.HasValue);
        }

        public override string ToString()
        {
            return $"Row({Index})";
        }
        
    }
}
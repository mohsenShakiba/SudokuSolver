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
        public Column(int rowIndex, Chart chart)
        {
            Index = rowIndex;
            Class = chart.Class;
            var boxColumn = (rowIndex / 3) + 1;
            var inputColumn = (rowIndex % 3) + 1;
            var rowRange = 0..chart.Class;
            var boxes = chart.Boxes.Where(s =>
                s.Column == boxColumn);
            var inputs = boxes.SelectMany(i => i.Inputs).Where(i =>
                i.Column == inputColumn &&
                i.Row >= rowRange.Start.Value && 
                i.Row <= rowRange.End.Value);

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
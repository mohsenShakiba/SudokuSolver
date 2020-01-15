using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Box
    {
        public int Class { get; }
        public int Size => (int)Math.Pow(Class, 2);
        public int Row { get; }
        public int Column { get; }
        public IEnumerable<Input> Inputs { get; }
        public int Count => Inputs.Count(i => i.HasValue);

        public Box(Chart chart, int row, int column)
        {
            Class = chart.Class;
            Row = row;
            Column = column;

            Inputs = chart.Inputs.Where(i => (i.Row / 3) + 1 == row).Where(i => (i.Column / 3) + 1 == column);
        }

        public int CountFor(int value) => Inputs.Count(i => i.HasValue && i.GetValue == value);

        public override string ToString() => $"Box({Row}:{Column})";

        public bool Contains(Input input) => Inputs.Contains(input);

    }
}
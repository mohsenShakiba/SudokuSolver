using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Box
    {
        public int Row { get; }
        public int Column { get; }
        public IEnumerable<Input> Inputs { get; }

        public Box(Chart chart, int row, int column)
        {
            Row = row;
            Column = column;
            Inputs = chart.Inputs.Where(i => (i.Row - 1)  / 3 == row - 1).Where(i => (i.Column - 1) / 3 == column - 1);
        }

        public override string ToString() => $"Box({Row}:{Column})";

        public bool Contains(Input input) => Inputs.Contains(input);

    }
}
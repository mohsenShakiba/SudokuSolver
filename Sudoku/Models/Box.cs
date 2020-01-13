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
        public List<Input> Inputs { get; }

        public Box(int @class, int row, int column)
        {
            Class = @class;
            Row = row;
            Column = column;
            Inputs = new List<Input>();
            for (int i = 0; i < @class; i++)
            {
                for (int j = 0; j < @class; j++)
                {
                    Inputs.Add(new Input(this, i + 1, j + 1));
                }
            }
        }

        public int Count()
        {
            return Inputs.Count(i => i.HasValue);
        }

        public override string ToString()
        {
            return $"Box({Row}:{Column})";
        }
        
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Box
    {
        public int Size { get; }
        public int Row { get; }
        public int Column { get; }
        public List<Input> Inputs { get; }

        public Box(int size, int row, int column)
        {
            Size = size;
            Row = row;
            Column = column;
            Inputs = new List<Input>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Inputs.Add(new Input(i + 1, j + 1));
                }
            }
        }

        public Input GetInput(int row, int column)
        {
            return Inputs.FirstOrDefault(i => i.Row == row && i.Column == column);
        }

        public int Count()
        {
            return Inputs.Count(i => i.Value != null);
        }

        public override string ToString()
        {
            return $"Box({Row}:{Column})";
        }
        
    }
}
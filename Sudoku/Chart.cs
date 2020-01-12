using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    public class Chart
    {
        public int Size { get; }
        public HashSet<Square> Squares { get; }
        
        public Chart(int size, int row, int column)
        {
            Size = size;
            Squares = new HashSet<Square>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Squares.Add(new Square(Size, i + 1, j + 1));
                }
            }
        }

        private IEnumerable<Square> NeighbourRowSquares(Square square)
        {
            return Squares.Where(s => s.Row == square.Row && s != square);
        }
        
        private IEnumerable<Square> NeighbourColumnSquares(Square square)
        {
            return Squares.Where(s => s.Column == square.Column && s != square);
        }

        public int Count()
        {
            return Squares.Sum(s => s.Count());
        }
    }

    public class Square
    {
        public int Size { get; }
        public int Row { get; }
        public int Column { get; }
        public HashSet<Input> Inputs { get; }

        public Square(int size, int row, int column)
        {
            Size = size;
            Row = row;
            Column = column;
            Inputs = new HashSet<Input>();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Inputs.Add(new Input(i + 1, j + 1));
                }
            }
        }

        public void SetInput(Input input)
        {
            var existingInput = Inputs.FirstOrDefault(i => i.Row == input.Row && i.Column == input.Column);
            if (existingInput.Value != null)
                throw new InvalidOperationException($"Cannot reset the value of {input} in {this}");
        }

        public bool IsInputValidInRow(Input input)
        {
            return !Inputs.Any(i => i.Row == input.Row && i.Value == input.Value);
        }
        
        public bool IsInputValidInColumn(Input input)
        {
            return !Inputs.Any(i => i.Column == input.Column && i.Value == input.Value);
        }

        public int Count()
        {
            return Inputs.Count(i => i.Value != null);
        }

        public override string ToString()
        {
            return $"Squate({Row}:{Column})";
        }
    }

    public class Row
    {
        
    }

    public class Column
    {
        
    }

    public struct Input
    {
        public int Row { get; }
        public int Column { get; }
        public int? Value { get; }

        public Input(int row, int column)
        {
            Row = row;
            Column = column;
            Value = null;
        }

        public override string ToString()
        {
            return $"{Row}:{Column} -> {Value}";
        }
    }
}
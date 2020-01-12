using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public bool IsInputValidForSquare(Square square, Input input)
        {
            var rowSquares = NeighbourRowSquares(square);
            var columnSquares = NeighbourColumnSquares(square);
            foreach (var rowSquare in rowSquares)
                if (!rowSquare.IsInputValidInRow(input))
                    return false;
            foreach (var columnSquare in columnSquares)
                if (!columnSquare.IsInputValidInColumn(input))
                    return false;
            return square.IsInputValidInRow(input);
        }

        public void AddInput(Square square, Input input)
        {
            square.SetInput(input);
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

        public string RepresentInString()
        {
            var sb = new StringBuilder();
            foreach (var square in Squares)
            {
                sb.Append(square);
                if (square.Column == 3)
                    sb.Append("\n");
            }
            return sb.ToString();
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
            return $"Square({Row}:{Column})";
        }

        public string RepresentInString()
        {
            var sb = new StringBuilder();
            foreach (var input in Inputs)
            {
                sb.Append($" {input.Value} ");
                if (input.Column == 3)
                    sb.Append("\n");
            }
            return sb.ToString();
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Chart
    {
        public int Size { get; }
        public List<Square> Squares { get; }
        
        public Chart(int size)
        {
            Size = size;
            Squares = new List<Square>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
            return square.IsInputValidInSquare(input);
        }

        public IEnumerable<Square> NeighbourRowSquares(Square square)
        {
            return Squares.Where(s => s.Row == square.Row && s != square);
        }
        
        public IEnumerable<Square> NeighbourColumnSquares(Square square)
        {
            return Squares.Where(s => s.Column == square.Column && s != square);
        }

        public int Count()
        {
            return Squares.Sum(s => s.Count());
        }

        public string RepresentInString()
        {

            string PrintRowSeperator()
            {
                var sb = new StringBuilder();
                var totalColumnsInRepresentationStr = (Size * Size) + (Size - 1) + 2;
                
                for (int j = 0; j < totalColumnsInRepresentationStr; j++)
                {
                    if (j % (Size + 1) == 0)
                        sb.Append("   ");
                    else
                        sb.Append("---");
                }
                
                sb.AppendLine();

                return sb.ToString();
            }
            
            var sb = new StringBuilder();

            sb.Append(PrintRowSeperator());
            
            for (int i = 0; i < Size * Size; i++)
            {
                var squareRow = (i / 3) + 1;
                var inputRow = (i % 3) + 1;
                var columnRange = 1..Size;
                var squares = Squares.Where(s =>
                    s.Row == squareRow);
                var inputs = squares.SelectMany(i => i.Inputs).Where(i =>
                    i.Row == inputRow &&
                    i.Column >= columnRange.Start.Value && 
                    i.Column <= columnRange.End.Value);
                
                sb.Append(" | ");
                
                foreach (var input in inputs)
                {
                    sb.Append($" {input.Value?.ToString() ?? " "} ");
                    if (input.Column == Size)
                        sb.Append(" | ");
                }

                sb.AppendLine();

                if (inputRow == 3)
                    sb.Append(PrintRowSeperator());
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

        public bool IsInputValidInRow(Input input)
        {
            return !Inputs.Any(i => i.Row == input.Row && i.Value == input.Value);
        }
        
        public bool IsInputValidInColumn(Input input)
        {
            return !Inputs.Any(i => i.Column == input.Column && i.Value == input.Value);
        }

        public bool IsInputValidInSquare(Input input)
        {
            return !Inputs.Where(i => i != input).Any(i => i.Value == input.Value);
        }

        public int Count()
        {
            return Inputs.Count(i => i.Value != null);
        }

        public override string ToString()
        {
            return $"Square({Row}:{Column})";
        }
        
    }

    public class Row
    {
        
    }

    public class Column
    {
        
    }

    public class Input
    {
        public int Row { get; }
        public int Column { get; }
        public int? Value { get; set; }

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
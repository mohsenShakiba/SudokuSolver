using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Chart
    {
        public int Size { get; }
        public List<Box> Boxes { get; }
        
        public Chart(int size)
        {
            Size = size;
            Boxes = new List<Box>();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Boxes.Add(new Box(Size, i + 1, j + 1));
                }
            }
        }

        public bool IsInputValidForBox(Box box, Input input)
        {
            var rowBoxes = NeighbourRowBoxes(box);
            var columnBoxes = NeighbourColumnBoxes(box);
            foreach (var rowBox in rowBoxes)
                if (!rowBox.IsInputValidInRow(input))
                    return false;
            foreach (var columnBox in columnBoxes)
                if (!columnBox.IsInputValidInColumn(input))
                    return false;
            return box.IsInputValidInBox(input);
        }

        public IEnumerable<Box> NeighbourRowBoxes(Box box)
        {
            return Boxes.Where(s => s.Row == box.Row && s != box);
        }
        
        public IEnumerable<Box> NeighbourColumnBoxes(Box box)
        {
            return Boxes.Where(s => s.Column == box.Column && s != box);
        }

        public int Count()
        {
            return Boxes.Sum(s => s.Count());
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
                var boxRow = (i / 3) + 1;
                var inputRow = (i % 3) + 1;
                var columnRange = 1..Size;
                var boxes = Boxes.Where(s =>
                    s.Row == boxRow);
                var inputs = boxes.SelectMany(i => i.Inputs).Where(i =>
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

        public bool IsInputValidInRow(Input input)
        {
            return !Inputs.Any(i => i.Row == input.Row && i.Value == input.Value);
        }
        
        public bool IsInputValidInColumn(Input input)
        {
            return !Inputs.Any(i => i.Column == input.Column && i.Value == input.Value);
        }

        public bool IsInputValidInBox(Input input)
        {
            return !Inputs.Where(i => i != input).Any(i => i.Value == input.Value);
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
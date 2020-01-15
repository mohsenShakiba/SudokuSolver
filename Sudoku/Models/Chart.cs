using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Sudoku.Models
{
    public class Chart
    {
        public int Class { get; }
        public int Size => (int)Math.Pow(Class, 2);
        public bool IsComplete => Count == (int) Math.Pow(Size, 2);
        private readonly IList<Input> _inputs = new List<Input>();
        public IReadOnlyList<Input> Inputs => _inputs.ToImmutableList();
        
        public int Count => _inputs.Count();

        public Chart(int @class)
        {
            Class = @class;
            GenerateColumns();
        }

        private void GenerateColumns()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    _inputs.Add(new Input(row, column));
                }
            }
        }

        public Box BoxForInput(Input input) => Boxes.First(b => b.Contains(input));
        
        public Row RowForInput(Input input) => Rows.First(b => b.Contains(input));
        
        public Column ColumnForInput(Input input) => Columns.First(b => b.Contains(input));
        
        public IEnumerable<Box> NeighbourBoxesInRow(Input input)
        {
            var containingBox = Boxes.First(b => b.Contains(input));
            return NeighbourBoxesInRow(containingBox);
        }
        
        public IEnumerable<Box> NeighbourBoxesInColumn(Input input)
        {
            var containingBox = Boxes.First(b => b.Contains(input));
            return NeighbourBoxesInColumn(containingBox);
        }

        public IEnumerable<Box> NeighbourBoxesInRow(Box box)
        {
            return Boxes.Where(s => s.Row == box.Row && s != box);
        }
        
        public IEnumerable<Box> NeighbourBoxesInColumn(Box box)
        {
            return Boxes.Where(s => s.Column == box.Column && s != box);
        }
        
        
        public int CountFor(int value) => Boxes.Sum(s => s.CountFor(value));

        public IEnumerable<Box> Boxes
        {
            get
            {
                for (int row = 0; row < Class; row++)
                    for (int column = 0; column < Class; column++)
                        yield return new Box(this, row, column);
            }
        }

        public IEnumerable<Row> Rows
        {
            get
            {
                for (int i = 0; i < Size; i++)
                    yield return new Row(i, this);
            }
        }
        
        public IEnumerable<Column> Columns
        {
            get
            {
                for (int i = 0; i < Size; i++)
                    yield return new Column(i, this);
            }
        }

    }
}
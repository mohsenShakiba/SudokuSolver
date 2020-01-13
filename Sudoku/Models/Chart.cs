using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Models
{
    public class Chart
    {
        public int Class { get; }
        public int Size => (int)Math.Pow(Class, 2);
        public List<Box> Boxes { get; }
        
        public Chart(int @class)
        {
            Class = @class;
            Boxes = new List<Box>();
            for (int i = 0; i < @class; i++)
            {
                for (int j = 0; j < @class; j++)
                {
                    Boxes.Add(new Box(Class, i + 1, j + 1));
                }
            }
        }

        public IEnumerable<Box> NeighbourBoxesInRow(Box box)
        {
            return Boxes.Where(s => s.Row == box.Row && s != box);
        }
        
        public IEnumerable<Box> NeighbourBoxesInColumn(Box box)
        {
            return Boxes.Where(s => s.Column == box.Column && s != box);
        }

        public int Count()
        {
            return Boxes.Sum(s => s.Count());
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
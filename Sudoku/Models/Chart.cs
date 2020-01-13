using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku.Models
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

    }
}
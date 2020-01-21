using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Sudoku.Models
{
    public class Chart
    {
        
        #region PublicProperties
        /// <summary>
        /// represents the class of chart
        /// for example a 9x9 sudoku chart has a class of 3
        /// </summary>
        public int Class { get; }
        
        /// <summary>
        /// represents the size of chart
        /// for example a typical 9x9 sudoku chart has a size of 9
        /// </summary>
        public int Size => (int)Math.Pow(Class, 2);
        
        /// <summary>
        /// returns true if all the inputs have value
        /// </summary>
        public bool IsComplete => Count == (int) Math.Pow(Size, 2);
        
        /// <summary>
        /// readonly representation of inputs
        /// </summary>
        public IReadOnlyList<Input> Inputs => _inputs.ToImmutableList();
        
        /// <summary>
        /// count of inputs that have value
        /// </summary>
        public int Count => _inputs.Count(i => i.HasValue);
        
        /// <summary>
        /// if the chart cannot be solved, is faulted will be set to true
        /// </summary>
        public bool IsFaulted { get; set; }

        #endregion

        #region PrivateFields
        
        private readonly IEnumerable<Input> _inputs;
        
        #endregion

        public Chart(int @class)
        {
            Class = @class;
            _inputs = GenerateColumns();
        }

        private Chart(int @class, IList<Input> inputs)
        {
            Class = @class;
            _inputs = inputs;
        }

        private IEnumerable<Input> GenerateColumns()
        {
            var inputs = new List<Input>(Size);
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    inputs.Add(new Input(row + 1, column + 1));
                }
            }
            return inputs;
        }

        /// <summary>
        /// returns the box for the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Box BoxForInput(Input input) => Boxes.First(b => b.Contains(input));
        
        /// <summary>
        /// returns the row for the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Row RowForInput(Input input) => Rows.First(b => b.Contains(input));
        
        /// <summary>
        /// returns the column for the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Column ColumnForInput(Input input) => Columns.First(b => b.Contains(input));
        
        
        /// <summary>
        /// return a box view of the chart
        /// </summary>
        public IEnumerable<Box> Boxes
        {
            get
            {
                for (int row = 0; row < Class; row++)
                    for (int column = 0; column < Class; column++)
                        yield return new Box(this, row + 1, column + 1);
            }
        }

        /// <summary>
        /// returns a row view of the chart
        /// </summary>
        public IEnumerable<Row> Rows
        {
            get
            {
                for (int i = 0; i < Size; i++)
                    yield return new Row(i + 1, this);
            }
        }
        
        /// <summary>
        /// return a column view of the chart
        /// </summary>
        public IEnumerable<Column> Columns
        {
            get
            {
                for (int i = 0; i < Size; i++)
                    yield return new Column(i + 1, this);
            }
        }

        /// <summary>
        /// creates a new chart with all properties cloned from the current chart
        /// </summary>
        /// <returns></returns>
        public Chart Clone()
        {
            return new Chart(Class, Inputs.Select(i => i.Clone()).ToList());
        }

    }
}
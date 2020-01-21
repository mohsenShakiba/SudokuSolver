using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Input
    {
        public int Row { get; }
        public int Column { get; }
        
        private int? _value;

        public int? Value
        {
            get => _value;
            set
            {
                if (_value != null && value != null)
                    throw new InvalidOperationException($"value for {this} has already been set");
                _value = value;
            }
        }

        public bool HasValue => Value != null;

        public Input(int row, int column, int? value = null)
        {
            Row = row;
            Column = column;
            _value = value;
        }

        /// <summary>
        /// clears the value
        /// </summary>
        public void Clear() => _value = null;
        
        public override string ToString()
        {
            return $"{Row}:{Column} -> {Value})";
        }

        public Input Clone()
        {
            return new Input(Row, Column, _value);
        }
        
        
    }

}
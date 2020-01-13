using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Input
    {
        public Box Box { get; }
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
        public int GetValue => _value ?? throw new InvalidOperationException($"{Value} is null");

        private readonly HashSet<int> _predictions;

        public Input(Box box, int row, int column)
        {
            Box = box;
            Row = row;
            Column = column;
            _predictions = new HashSet<int>();
        }
        
        public override string ToString()
        {
            return $"{Row}:{Column} -> {Value} ({string.Join(',', _predictions.AsEnumerable())})";
        }

        public void AddPrediction(int prediction)
        {
            _predictions.Add(prediction);
        }

        public void RemovePrediction(int prediction)
        {
            _predictions.Remove(prediction);
        }

        public IEnumerable<int> Predictions => _predictions.AsEnumerable();

        public int? MatchingPrediction(Input input)
        {
            return Predictions.Intersect(input.Predictions).FirstOrDefault();
        }

    }

}
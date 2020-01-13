using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models
{
    public class Input
    {
        public int Row { get; }
        public int Column { get; }
        public int? Value { get; set; }
        
        private readonly HashSet<int> _predictions;

        public Input(int row, int column)
        {
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
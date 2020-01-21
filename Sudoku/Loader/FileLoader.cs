using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sudoku.Models;

namespace Sudoku.Loader
{
    public class FileLoader: ILoader
    {

        private readonly string _path;
        private readonly Chart _chart;
        
        public FileLoader(string path, int @class)
        {
            _path = path;
            _chart = new Chart(@class);
        }
        
        public Chart Load()
        {
            // make sure the file exists
            if (!File.Exists(_path))
                throw new InvalidOperationException("InvalidInput");

            // get all lines
            var allLines = File.ReadAllLines(_path);

            // except separator lines
            var numberLines = allLines.Where(l => l.Any(c => c != '-')).ToArray();
                
            // make sure the file has enough lines
            if (numberLines.Length != _chart.Size)
                throw new InvalidOperationException("InvalidNumberOfLines");
            
            // sip rows with lines
            var rowLineZip = _chart.Rows.Zip(numberLines, (row, line) => new {row, line});
            
            foreach (var rowLine in rowLineZip)
            {
                var rowNumbers = ConvertStringToRowNumbers(rowLine.line);
                var inputNumberZip = rowLine.row.Inputs.Zip(rowNumbers, (input, number) => new {input, number});
                foreach (var inputNumber in inputNumberZip)
                {
                    inputNumber.input.Value = inputNumber.number;
                }
            }

            return _chart;
        }

        private IEnumerable<int?> ConvertStringToRowNumbers(string rowStr)
        {
            return string.Join(',', rowStr.Split("|")).Split(',').Select(s => s == "-" ? (int?)null : int.Parse(s));
        }
    }
}
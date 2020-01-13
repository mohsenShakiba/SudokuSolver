using System;
using System.Drawing;
using System.Linq;
using System.Text;
using Sudoku.Models;

namespace Sudoku.Presentation
{
    public class StringPresentation: IPresentation
    {
        public void Present(Chart chart)
        {
            string PrintRowSeperator()
            {
                var sb = new StringBuilder();
                var totalColumnsInRepresentationStr = (chart.Class * chart.Class) + (chart.Class - 1) + 2;
                
                for (int j = 0; j < totalColumnsInRepresentationStr; j++)
                {
                    if (j % (chart.Class + 1) == 0)
                        sb.Append("   ");
                    else
                        sb.Append("---");
                }
                
                sb.AppendLine();

                return sb.ToString();
            }
            
            var sb = new StringBuilder();

            sb.Append(PrintRowSeperator());
            
            for (int i = 0; i < chart.Class * chart.Class; i++)
            {
                var boxRow = (i / 3) + 1;
                var inputRow = (i % 3) + 1;
                var columnRange = 0..chart.Class;
                var boxes = chart.Boxes.Where(s =>
                    s.Row == boxRow);
                var inputs = boxes.SelectMany(i => i.Inputs).Where(i =>
                    i.Row == inputRow &&
                    i.Column >= columnRange.Start.Value && 
                    i.Column <= columnRange.End.Value);
                
                sb.Append(" | ");
                
                foreach (var input in inputs)
                {
                    sb.Append($" {input.Value?.ToString() ?? " "} ");
                    if (input.Column == chart.Class)
                        sb.Append(" | ");
                }

                sb.AppendLine();

                if (inputRow == 3)
                    sb.Append(PrintRowSeperator());
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
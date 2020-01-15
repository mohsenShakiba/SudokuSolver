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
            var sb = new StringBuilder();

            sb.Append(PrintSeparator(chart.Class));

            foreach (var input in chart.Inputs)
            {
                if (input.Column % chart.Class == 1)
                    sb.Append(" | ");
                
                sb.Append($" {input.Value?.ToString() ?? "0"} ");
                
                if (input.Column == chart.Size)
                {
                    sb.Append(" | ");
                    sb.AppendLine();
                    
                    if (input.Row % chart.Class == 0)
                        sb.Append(PrintSeparator(chart.Class));
                }

            }
            
            Console.WriteLine(sb.ToString());
        }

        private string PrintSeparator(int @class)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < (int) Math.Pow(@class, 2) + @class + 1; i++)
            {
                sb.Append("---");
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}
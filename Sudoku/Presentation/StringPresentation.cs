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

            foreach (var input in chart.Inputs)
            {
                if (input.Row == 1)
                    sb.Append(" | ");
                
//                if (input.Column == 1)
//                    sb.Append("---");
                
                sb.Append($" {input.Value?.ToString() ?? "0"} ");
                
                if (input.Row == 3)
                    sb.AppendLine(" | ");
                
//                if (input.Column == 1)
//                    sb.Append("---");
            }
            
            Console.WriteLine(sb.ToString());
        }
    }
}
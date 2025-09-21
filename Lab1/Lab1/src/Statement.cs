/*
* Course: PROG3330 - Fall 2025 - Section 2
* Programmed by : Juhwan Seo [8819123]
* Project: Lab 1
* Revision history:
*      21-Sep-2025: Project created
*      23-Sep-2025: Project complete
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab1.src
{
    /// <summary>
    /// Provides static methods to generate and format billing statements in both text and HTML formats.
    /// </summary>
    public static class Statement
    {
        public static string UsdFromCents(int cents) =>
            (cents / 100m).ToString("C", CultureInfo.GetCultureInfo("en-US"));

        // Generates a plain text billing statement
        public static string RunText(
            Dictionary<string, object> invoice,
            Dictionary<string, Dictionary<string, object>> plays)
            {
                var data = CreateStatementData.Build(invoice, plays);
                return RenderText(data);
            }

        // Generates an HTML billing statement
        public static string RunHtml(
            Dictionary<string, object> invoice,
            Dictionary<string, Dictionary<string, object>> plays)
            {
                var data = CreateStatementData.Build(invoice, plays);
                return RenderHtml(data);
            }

        // Formats the statement data as plain text
        private static string RenderText(StatementData _data)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Statement for {_data.Customer}");
            foreach (var perf in _data.Performances)
            {
                sb.AppendLine($"  {perf.PlayName}: {UsdFromCents(perf.Amount)} ({perf.Audience} seats)");
            }
            sb.AppendLine($"Amount owed is {UsdFromCents(_data.TotalAmount)}");
            sb.AppendLine($"You earned {_data.TotalVolumeCredits} credits");
            return sb.ToString().TrimEnd();
        }

        // Escapes special HTML characters
        private static string EscapeHtml(string input)
        {
            if (input == null) return "";
            return input.Replace("'", "&#39;")
                        .Replace("\"", "&quot;")
                        .Replace("&", "&amp;")
                        .Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("/", "&#47;")
                        .Replace("`", "&#96;")
                        .Replace("=", "&#61;");
        }

        // Formats the statement data as HTML.
        private static string RenderHtml(StatementData _data)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<h1>Statement for {EscapeHtml(_data.Customer)}</h1>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><th>Play</th><th>Seats</th><th>Cost</th></tr>");
            foreach (var perf in _data.Performances)
            {
                sb.AppendLine($"<tr><td>{EscapeHtml(perf.PlayName)}</td><td>{perf.Audience}</td><td>{UsdFromCents(perf.Amount)}</td></tr>");
            }
            sb.AppendLine("</table>");
            sb.AppendLine($"<p>Amount owed is <em>{UsdFromCents(_data.TotalAmount)}</em></p>");
            sb.AppendLine($"<p>You earned <em>{_data.TotalVolumeCredits}</em> credits</p>");
            return sb.ToString().TrimEnd();
        }
    }
}

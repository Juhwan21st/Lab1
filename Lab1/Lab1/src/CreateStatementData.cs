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
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab1.src.Calculators;

namespace Lab1.src
{
    /// <summary>
    /// Builds a StatementData DTO containing all calculated billing info for a customer.
    /// - Uses a registry of play type calculators for pricing and credits (Open/Closed Principle)
    /// - Returns only structured data; does not format currency or build output strings
    /// </summary>
    public static class CreateStatementData
    {
        /// <summary>
        /// Registry mapping play type strings to their calculator implementations.
        /// Add new play types here with a single line and adding a new calculator class in Calculators directory.
        /// </summary>
        private static readonly Dictionary<string, ICalculator> calculatorRegistry = new()
        {
            { "tragedy", new TragedyCalculator() },
            { "comedy", new ComedyCalculator() }
            // Add new play types here (e.g., { "history", new HistoryCalculator() })
        };

        public static StatementData Build(
            Dictionary<string, object> invoice,
            Dictionary<string, Dictionary<string, object>> plays)
        {
            string? customer = ((JsonElement)invoice["customer"]).GetString();
            var performances = ((JsonElement)invoice["performances"]).EnumerateArray();
            var rows = new List<Row>();
            int totalAmount = 0;
            int totalVolumeCredits = 0;

            foreach (var perf in performances)
            {
                string? playID = perf.GetProperty("playID").GetString();
                int audience = perf.GetProperty("audience").GetInt32();
                var play = plays[playID];
                string? type = ((JsonElement)play["type"]).GetString();
                string? playName = ((JsonElement)play["name"]).GetString();

                if (!calculatorRegistry.ContainsKey(type))
                {
                    throw new Exception($"Unknown play type: '{type}' for playID '{playID}'");
                }
                var calculator = calculatorRegistry[type];
                int amount = calculator.CalculateAmount(audience);
                int credits = calculator.CalculateCredits(audience);
                rows.Add(new Row(playName, audience, amount, credits));
                totalAmount += amount;
                totalVolumeCredits += credits;
            }

            return new StatementData(customer, rows, totalAmount, totalVolumeCredits);;
        }
    }

    public record Row(string PlayName, int Audience, int Amount, int VolumeCredits);

    public record StatementData(string Customer, IReadOnlyList<Row> Performances, int TotalAmount, int TotalVolumeCredits);
}
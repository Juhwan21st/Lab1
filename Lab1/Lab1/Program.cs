/*
* Course: PROG3330 - Fall 2025 - Section 2
* Programmed by : Juhwan Seo [8819123]
* Project: Lab 1
* Revision history:
*      21-Sep-2025: Project created
*      23-Sep-2025: Project complete
*/

using System.IO;
using System.Text.Json;
using Lab1.src;

/// <summary>
/// Entry point for the billing statement application.
/// </summary>
class Program
{
    static void Main()
    {
        // Load play and invoice data from JSON files 
        var plays = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(
            File.ReadAllText("data/plays.json"));
        var invoice = JsonSerializer.Deserialize<Dictionary<string, object>>(
            File.ReadAllText("data/invoice.json"));

        // Generate plain text statement using the Statement class
        var textResult = Statement.RunText(invoice, plays);
        Console.WriteLine("Plain Text:\n" + textResult);

        // Generate HTML statement using the Statement class
        var htmlResult = Statement.RunHtml(invoice, plays);
        Console.WriteLine("HTML:\n" + htmlResult);

        // Save HTML output to file for browser viewing
        File.WriteAllText("statement.html", htmlResult);

        // Automatically open the HTML file in the default web browser
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = Path.GetFullPath("statement.html"),
            UseShellExecute = true
        });
    }
}

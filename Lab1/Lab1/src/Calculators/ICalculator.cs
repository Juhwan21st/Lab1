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
using System.Threading.Tasks;

namespace Lab1.src.Calculators
{
    /// <summary>
    /// Interface for play type calculators (pricing and credits)
    /// - Supports Open/Closed Principle: new play types can be added by implementing this interface
    /// - Used in registry for polymorphic calculation
    /// </summary>
    public interface ICalculator
    {
        // Calculate how much to charge for this audience size.
        int CalculateAmount(int audience);

        /// Calculate how many credits to give for this audience size.
        int CalculateCredits(int audience);
    }
}

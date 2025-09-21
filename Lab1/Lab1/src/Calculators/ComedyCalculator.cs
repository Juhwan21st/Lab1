using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.src.Calculators
{
    internal class ComedyCalculator : ICalculator
    {
        public int CalculateAmount(int audience)
        {
            int amount = 30000;
            if (audience > 20)
            {
                amount += 10000 + 500 * (audience - 20);
            }
            amount += 300 * audience;
            return amount;
        }

        public int CalculateCredits(int audience)
        {
            return Math.Max(audience - 30, 0) + (int)Math.Floor(audience / 5.0);
        }
    }
}

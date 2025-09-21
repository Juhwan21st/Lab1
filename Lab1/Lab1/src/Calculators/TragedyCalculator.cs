using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.src.Calculators
{
    internal class TragedyCalculator : ICalculator
    {
        public int CalculateAmount(int audience)
        {
            int amount = 40000;
            if (audience > 30)
            {
                amount += 1000 * (audience - 30);
            }
            return amount;
        }

        public int CalculateCredits(int audience)
        {
            return Math.Max(audience - 30, 0);
        }
    }
}

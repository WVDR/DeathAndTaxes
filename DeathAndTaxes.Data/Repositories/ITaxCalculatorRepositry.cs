using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data.Repositories
{
    public interface ITaxCalculatorRepositry
    {
        public double calculateProgressive(double income);
        public double calculateFlatValue(double income);
        public double calculateFlatRate(double income);

        public double CalculateAndStoreTax(string postcode,double income);
    }
}


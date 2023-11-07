using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.API.Test
{
    internal class TaxCalculator
    {
        public bool IsPercentage(double value)
        {
            return value >= 0.0 && value <= 100.0;
        }
        public double CalculateFlatValueTax(double income, double flatTaxValue)
        {
            return income - flatTaxValue;
        }

        public double CalculateFlatRateTax(double income, double flatTaxRate)
        {
            if (IsPercentage(flatTaxRate))
            {
                return income - flatTaxRate;
            }
            else
            {
                throw new Exception(string.Format($"flatTaxRate is not a percentage: '{flatTaxRate}'"));
            }
        }

        public double CalculateProgressiveTax(double income)
        {
            if (income <= 8350)
            {
                return income * 0.10;
            }
            else if (income <= 33950)
            {
                return 8350 * 0.10 + (income - 8350) * 0.15;
            }
            else if (income <= 82250)
            {
                return 8350 * 0.10 + (33950 - 8350) * 0.15 + (income - 33950) * 0.25;
            }
            else if (income <= 171550)
            {
                return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (income - 82250) * 0.28;
            }
            else if (income <= 372950)
            {
                return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (171550 - 82250) * 0.28 + (income - 171550) * 0.33;
            }
            else
            {
                return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (171550 - 82250) * 0.28 + (372950 - 171550) * 0.33 + (income - 372950) * 0.35;
            }
        }
    }

    [TestFixture]
    public class TaxCalculatorTests
    {
        [Test]
        public void CalculateProgressiveTax_ShouldReturnCorrectTax()
        {
            // Arrange
            var taxCalculator = new TaxCalculator();

            // Test cases with different income values
            double income1 = 5000;  // 10% bracket
            double income2 = 20000; // 10% + 15% bracket
            double income3 = 40000; // 10% + 15% + 25% bracket
            double income4 = 100000; // 10% + 15% + 25% + 28% bracket
            double income5 = 200000; // 10% + 15% + 25% + 28% + 33% bracket
            double income6 = 400000; // 10% + 15% + 25% + 28% + 33% + 35% bracket

            // Act
            double tax1 = taxCalculator.CalculateProgressiveTax(income1);
            double tax2 = taxCalculator.CalculateProgressiveTax(income2);
            double tax3 = taxCalculator.CalculateProgressiveTax(income3);
            double tax4 = taxCalculator.CalculateProgressiveTax(income4);
            double tax5 = taxCalculator.CalculateProgressiveTax(income5);
            double tax6 = taxCalculator.CalculateProgressiveTax(income6);

            // Assert
            Assert.That(tax1, Is.EqualTo(500.0).Within(0.01));   // 5000 * 0.10
            Assert.That(tax2, Is.EqualTo(2582.5).Within(0.01));  // 8350 * 0.10 + 11650 * 0.15
            Assert.That(tax3, Is.EqualTo(6187.5).Within(0.01));  // 8350 * 0.10 + 25600 * 0.15 + 600 * 0.25
            Assert.That(tax4, Is.EqualTo(21720).Within(0.01)); // 8350 * 0.10 + 25600 * 0.15 + 48300 * 0.25 + 143750 * 0.28
            Assert.That(tax5, Is.EqualTo(51142.5).Within(0.01)); // 8350 * 0.10 + 25600 * 0.15 + 48300 * 0.25 + 89300 * 0.28 + 28250 * 0.33
            Assert.That(tax6, Is.EqualTo(117683.5).Within(0.01)); // 8350 * 0.10 + 25600 * 0.15 + 48300 * 0.25 + 89300 * 0.28 + 201400 * 0.33 + 272050 * 0.35
        }

        [Test]
        public void CalculateFlatValueTax_ShouldReturnCorrectTax()
        {
            // Arrange
            var taxCalculator = new TaxCalculator();
            double income = 50000;

            // Act
            double tax = taxCalculator.CalculateFlatValueTax(income, 1000);

            // Assert
            Assert.That(tax, Is.EqualTo(49000)); // Replace with the expected tax value
        }

        [Test]
        public void CalculateFlatRateTax_ShouldReturnCorrectTax()
        {
            // Arrange
            var taxCalculator = new TaxCalculator();
            double income = 50000;

            // Act
            double tax = taxCalculator.CalculateFlatRateTax(income, 0.15);

            // Assert
            Assert.That(tax, Is.EqualTo(49999.85)); // Replace with the expected tax value
        }
    }
}

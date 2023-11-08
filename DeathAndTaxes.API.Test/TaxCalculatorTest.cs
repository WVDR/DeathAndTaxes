using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.API.Test
{
    internal class TaxCalculatorTest
    {
        public static bool IsPercentage(double value)
        {
            return value >= 0.0 && value <= 100.0;
        }

        public static int GetIndex<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key)
        {
            int index = 0;

            foreach (var kvp in dictionary)
            {
                if (EqualityComparer<TKey>.Default.Equals(kvp.Key, key))
                {
                    return index;
                }
                index++;
            }

            return -1; // Key not found in the dictionary
        }

        public static double CalculateFlatValueTax(double incomePerAnum, double flatValueTaxRate, double incomePerAnumMax, double FlatValueTaxBasePerAnum)
        {

            if (!IsPercentage(flatValueTaxRate))
            {
                throw new Exception(string.Format($"flatValueTaxRate is not a percentage: '{flatValueTaxRate}'"));
            }

            if (incomePerAnum < incomePerAnumMax)
            {
                // If income is less than 200,000, apply a 5% tax rate.
                return incomePerAnum * flatValueTaxRate;
            }
            else
            {
                // If income is 200,000 or more, use a flat tax of 10,000 per year.
                return FlatValueTaxBasePerAnum;
            }
        }

        public static double CalculateFlatRateTax(double income, double flatTaxRate)
        {
            if (!IsPercentage(flatTaxRate))
            {
                throw new Exception(string.Format($"flatTaxRate is not a percentage: '{flatTaxRate}'"));
            }

            if (IsPercentage(flatTaxRate))
            {
                // Calculate tax at a flat rate of 17.5% of the income.
                return income * flatTaxRate;
            }
            else
            {
                throw new Exception(string.Format($"flatTaxRate is not a percentage: '{flatTaxRate}'"));
            }
        }

        /// <summary>
        /// This calculates progressive tax based on the income supplied and the tax brackets supplied.
        /// </summary>
        /// <param name="income">The income the individual earns.</param>
        /// <param name="taxBracketsAndPercentance">The tax bracket key and the percentage value.</param>
        /// <returns></returns>
        public double CalculateProgressiveTax(double income, Dictionary<double,double> taxBracketsAndPercentance)
        {
            // Order the keys alphabetically
            var orderedKeys = taxBracketsAndPercentance.Keys.OrderBy(key => key).ToArray();

            double progressiveTax = 0;
            for (int i = 0; i < orderedKeys.Count(); i++)
            {
                var taxbracketKey = orderedKeys[i];
                var percentageValue = taxBracketsAndPercentance[taxbracketKey];

                if (income <= taxbracketKey)
                {
                    //first level no need to process anything else
                    if (i == 0)
                    {
                        return income * percentageValue;
                    }


                    for (int j = 1; j <= i; j++)
                    {
                        var pastTaxbracketKey = orderedKeys[j];
                        var pastPercentageValue = taxBracketsAndPercentance[pastTaxbracketKey];
                        var pastPreviouseTaxbracketKey = orderedKeys[j-1];                       

                        if(j < i)
                        {
                            progressiveTax += (pastTaxbracketKey - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        else if (j >= i)
                        {
                            progressiveTax += (income - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        else
                        {
                            throw new Exception(string.Format($"taxbracket could not be processed: '{taxbracketKey}'"));
                        }
                    }
                    return progressiveTax;
                }
            }
            return progressiveTax;

            // SoduCode of the expected flow above.
            //if (income <= 8350)
            //{
            //    return income * 0.10;
            //}
            //else if (income <= 33950)
            //{
            //    return 8350 * 0.10 + (income - 8350) * 0.15;
            //}
            //else if (income <= 82250)
            //{
            //    return 8350 * 0.10 + (33950 - 8350) * 0.15 + (income - 33950) * 0.25;
            //}
            //else if (income <= 171550)
            //{
            //    return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (income - 82250) * 0.28;
            //}
            //else if (income <= 372950)
            //{
            //    return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (171550 - 82250) * 0.28 + (income - 171550) * 0.33;
            //}
            //else
            //{
            //    return 8350 * 0.10 + (33950 - 8350) * 0.15 + (82250 - 33950) * 0.25 + (171550 - 82250) * 0.28 + (372950 - 171550) * 0.33 + (income - 372950) * 0.35;
            //}
        }
    }

    [TestFixture]
    public class TaxCalculatorTests
    {
        [Test]
        public void CalculateProgressiveTax_ShouldReturnCorrectTax()
        {
            // Arrange
            var taxCalculator = new TaxCalculatorTest();

            // Test cases with different income values
            double income1 = 5000;  // 10% bracket
            double income2 = 20000; // 10% + 15% bracket
            double income3 = 40000; // 10% + 15% + 25% bracket
            double income4 = 100000; // 10% + 15% + 25% + 28% bracket
            double income5 = 200000; // 10% + 15% + 25% + 28% + 33% bracket
            double income6 = 400000; // 10% + 15% + 25% + 28% + 33% + 35% bracket
            Dictionary<double, double> taxBracketsAndPercentance = new Dictionary<double, double>();
            taxBracketsAndPercentance.Add(8350, 0.10);
            taxBracketsAndPercentance.Add(33950, 0.15);
            taxBracketsAndPercentance.Add(82250, 0.25);
            taxBracketsAndPercentance.Add(171550, 0.28);
            taxBracketsAndPercentance.Add(372950, 0.33);
            taxBracketsAndPercentance.Add(double.MaxValue, 0.35);

            // Act
            double tax1 = taxCalculator.CalculateProgressiveTax(income1, taxBracketsAndPercentance);
            double tax2 = taxCalculator.CalculateProgressiveTax(income2, taxBracketsAndPercentance);
            double tax3 = taxCalculator.CalculateProgressiveTax(income3, taxBracketsAndPercentance);
            double tax4 = taxCalculator.CalculateProgressiveTax(income4, taxBracketsAndPercentance);
            double tax5 = taxCalculator.CalculateProgressiveTax(income5, taxBracketsAndPercentance);
            double tax6 = taxCalculator.CalculateProgressiveTax(income6, taxBracketsAndPercentance);

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
            double incomePerAnum = 50000;
            double flatValueTaxRate = 0.05;
            double incomePerAnumMax = 200000;
            double FlatValueTaxBasePerAnum = 10000;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatValueTax(incomePerAnum, flatValueTaxRate, incomePerAnumMax, FlatValueTaxBasePerAnum);

            // Assert
            Assert.That(tax, Is.EqualTo(2500)); // Replace with the expected tax value
        }

        [Test]
        public void CalculateFlatRateTax_ShouldReturnCorrectTax()
        {
            // Arrange            
            double income = 50000;
            double flatTaxRate = 0.175;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatRateTax(income, flatTaxRate);

            // Assert
            Assert.That(tax, Is.EqualTo(8750)); // Replace with the expected tax value
        }

        [Test]
        public void CalculateTax_IncomeLessThan200000_TaxShouldBe5Percent()
        {
            // Arrange
            double incomePerAnum = 150000; // Example income less than 200,000
            double flatValueTaxRate = 0.05;
            double incomePerAnumMax = 200000;
            double FlatValueTaxBasePerAnum = 10000;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatValueTax(incomePerAnum, flatValueTaxRate, incomePerAnumMax, FlatValueTaxBasePerAnum);

            // Assert
            Assert.That(tax, Is.EqualTo(7500)); // 5% of 150,000
        }

        [Test]
        public void CalculateTax_IncomeGreaterOrEqualTo200000_TaxShouldBe10000()
        {
            // Arrange
            double incomePerAnum = 250000; // Example income greater than or equal to 200,000
            double flatValueTaxRate = 0.05;
            double incomePerAnumMax = 200000;
            double FlatValueTaxBasePerAnum = 10000;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatValueTax(incomePerAnum, flatValueTaxRate, incomePerAnumMax, FlatValueTaxBasePerAnum);

            // Assert
            Assert.That(tax, Is.EqualTo(10000)); // Flat tax of 10,000
        }

        [Test]
        public void CalculateTax_Income10000_TaxShouldBe1750()
        {
            // Arrange
            double income = 10000; // Example income
            double flatTaxRate = 0.175;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatRateTax(income, flatTaxRate);

            // Assert
            Assert.That(tax, Is.EqualTo(1750)); // 17.5% of 10,000
        }

        [Test]
        public void CalculateTax_Income50000_TaxShouldBe8750()
        {
            // Arrange
            double income = 50000; // Another example income
            double flatTaxRate = 0.175;

            // Act
            double tax = TaxCalculatorTest.CalculateFlatRateTax(income, flatTaxRate);

            // Assert
            Assert.That(tax, Is.EqualTo(8750)); // 17.5% of 50,000
        }
    }
}

using DeathAndTaxes.Core.Models;
using DeathAndTaxes.Data.Entities;
using DeathAndTaxes.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data.Repositories
{
    public class TaxCalculatorRepositry : ITaxCalculatorRepositry
    {
        private readonly DeathAndTaxesDbContext _dbcontext;
        private readonly IPostalCodeRepositry _postalCodeRepositry;

        public TaxCalculatorRepositry(DeathAndTaxesDbContext dbcontext, IPostalCodeRepositry postalCodeRepositry)
        {
            _dbcontext = dbcontext;
            _postalCodeRepositry = postalCodeRepositry;
        }

        public double calculateFlatRate(double income)
        {
            throw new NotImplementedException();
        }

        public double calculateFlatValue(double income)
        {
            throw new NotImplementedException();
        }

        public double calculateProgressive(double income)
        {
            var query = _dbcontext.ProgressiveTaxes
                .Join(
                    _dbcontext.TaxPercentageRates,
                    ProgressiveTaxes => ProgressiveTaxes.TaxPercentageRateId,
                    TaxPercentageRates => TaxPercentageRates.TaxPercentageRateId,
                    (ProgressiveTaxes, TaxPercentageRates) => new
                    {
                        ProgressiveTaxes,
                        TaxPercentageRates
                    }
                )
                .Join(
                    _dbcontext.TaxIncomeBrackets,
                    ProgressiveTaxesTaxCalculationTypes => ProgressiveTaxesTaxCalculationTypes.ProgressiveTaxes.TaxIncomeBracketId,
                    TaxIncomeBrackets => TaxIncomeBrackets.TaxIncomeBracketId,
                    (ProgressiveTaxesTaxCalculationTypes, TaxIncomeBrackets) => new
                    {
                        ProgressiveTaxesTaxCalculationTypes.TaxPercentageRates.PercentageRate,
                        TaxIncomeBrackets.ToIncomeBracket
                    }
                ).OrderBy(TaxPercentageRates => TaxPercentageRates.PercentageRate).ThenBy(TaxIncomeBrackets => TaxIncomeBrackets.ToIncomeBracket).ToList();

            Dictionary<double, double> taxBracketsAndPercentage = new Dictionary<double, double>();
            foreach (var item in query)
            {
                taxBracketsAndPercentage.Add(item.ToIncomeBracket, item.PercentageRate/100);
            }

            var result = CalculateProgressiveTax(income, taxBracketsAndPercentage);
            return result;

        }


        /// <summary>
        /// This calculates progressive tax based on the income supplied and the tax brackets supplied.
        /// </summary>
        /// <param name="income">The income the individual earns.</param>
        /// <param name="taxBracketsAndPercentance">The tax bracket key and the percentage value.</param>        
        /// <returns></returns>
        public double CalculateProgressiveTax(double income, Dictionary<double, double> taxBracketsAndPercentance)
        {
            // Order the keys alphabetically
            var orderedKeys = taxBracketsAndPercentance.Keys.OrderBy(key => key).ToArray();

            double progressiveTax = 0;
            var maxbracket = orderedKeys.Count() - 1;
            //all tax brackets is i
            for (int i = 0; i <= maxbracket; i++)
            {
                var taxbracketKey = orderedKeys[i];
                var percentageValue = taxBracketsAndPercentance[taxbracketKey];

                //Falls within tax bracket
                if (income <= taxbracketKey)
                {
                    //first level no need to process anything else
                    if (i == 0)
                    {
                        return income * percentageValue;
                    }
                    //Take the base level tax and calculate up
                    else
                    {
                        progressiveTax += orderedKeys[0] * taxBracketsAndPercentance[orderedKeys[0]];
                    }

                    //Tax brackets prior and upto the current one, but NOT 1st base one being 0.
                    for (int j = 1; j <= i; j++)
                    {
                        var pastTaxbracketKey = orderedKeys[j];
                        var pastPercentageValue = taxBracketsAndPercentance[pastTaxbracketKey];
                        var pastPreviouseTaxbracketKey = orderedKeys[j - 1];

                        //All prior brackets(besides base or 0), use the tax caluclation for that bracket only with its rate.
                        if (j < i)
                        {
                            progressiveTax += (pastTaxbracketKey - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        //current brackets, use the tax caluclation with the current income and that bracket rate.
                        else if (j == i)
                        {
                            progressiveTax += (income - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        //You either fall outside min, max range and there is something wrong.
                        else
                        {
                            throw new IndexOutOfRangeException(string.Format($"income: '{income}' does not match taxbracket could not be processed: '{taxbracketKey}'"));
                        }
                    }
                    return progressiveTax;
                }
                //this is above what we predicted so just process all brackets
                else if (income > orderedKeys[maxbracket])
                {
                    progressiveTax += orderedKeys[0] * taxBracketsAndPercentance[orderedKeys[0]];

                    //process all brackets besides base
                    for (int j = 1; j <= maxbracket; j++)
                    {
                        var pastTaxbracketKey = orderedKeys[j];
                        var pastPercentageValue = taxBracketsAndPercentance[pastTaxbracketKey];
                        var pastPreviouseTaxbracketKey = orderedKeys[j - 1];

                        //All prior brackets(besides base or 0), use the tax caluclation for that bracket only with its rate.
                        if (j < maxbracket)
                        {
                            progressiveTax += (pastTaxbracketKey - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        //current brackets, use the tax caluclation with the current income and that bracket rate.
                        else if (j == maxbracket)
                        {
                            progressiveTax += (income - pastPreviouseTaxbracketKey) * pastPercentageValue;
                        }
                        //You either fall outside min, max range and there is something wrong.
                        else
                        {
                            throw new ArgumentOutOfRangeException(string.Format($"income: '{income}' is outside of max taxbracket: '{orderedKeys[maxbracket]}'"));
                        }
                    }
                    return progressiveTax;
                }
                else if (i == maxbracket)
                {
                    throw new ArgumentException(string.Format($"income: '{income}' could not be process for taxbrackets: '{orderedKeys[0]}' - '{orderedKeys[maxbracket]}'"));
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

        public double CalculateAndStoreTax(string postcode, double income)
        {
            var result = _postalCodeRepositry.GetPostalCode(postcode);
            PostCodeCalculationType? PostCodeCalculationType =
                JsonSerializer.Deserialize<PostCodeCalculationType>(result);

            double tax = 0;
            switch (PostCodeCalculationType.Name)
            {
                case "Progressive":
                    tax = CalculateTax(income, CalculationTypeEnum.Progressive);
                    break;
                case "FlatValue":
                    tax = CalculateTax(income, CalculationTypeEnum.FlatValue);
                    break;
                case "FlatRate":
                    tax = CalculateTax(income, CalculationTypeEnum.FlatRate);
                    break;
                default:
                    break;
            }
            TaxScore taxScore = new TaxScore
            {
                User = WindowsIdentity.GetCurrent().Name, //TODO will only work in debug, change for production.
                Income = income,
                Tax = tax,
                DateCapotured = DateTime.UtcNow,//UTC for international cases.
                PostalCodeId = PostCodeCalculationType.PostalCodeId

            };
            _dbcontext.TaxScores.Add(taxScore);
            _dbcontext.SaveChanges();
            return tax;
        }

        private double CalculateTax(double income, CalculationTypeEnum calculationType)
        {
            double tax = 0;
            switch (calculationType)
            {
                case CalculationTypeEnum.None:                    
                    break;
                case CalculationTypeEnum.Progressive:                    
                     tax = calculateProgressive(income);
                    break;
                case CalculationTypeEnum.FlatValue:
                    tax = CalculateFlatValueTax(income);
                    break;
                case CalculationTypeEnum.FlatRate:
                    tax = CalculateFlatRateTax(income);
                    break;
                default:                    
                    break;
            }
            return tax;
        }

        private double CalculateFlatValueTax(double incomePerAnum)
        {
            var query = _dbcontext.FlatValueTaxes
                .Join(
                    _dbcontext.TaxPercentageRates,
                    FlatValueTaxes => FlatValueTaxes.TaxPercentageRateId,
                    TaxPercentageRates => TaxPercentageRates.TaxPercentageRateId,
                    (FlatValueTaxes, TaxPercentageRates) => new
                    {
                        FlatValueTaxes,
                        TaxPercentageRates
                    }
                )
                .Join(
                    _dbcontext.TaxIncomeBrackets,
                    FlatValueTaxesTaxCalculation => FlatValueTaxesTaxCalculation.FlatValueTaxes.TaxIncomeBracketId,
                    TaxIncomeBrackets => TaxIncomeBrackets.TaxIncomeBracketId,
                    (FlatValueTaxesTaxCalculation, TaxIncomeBrackets) => new
                    {
                        FlatValueTaxesTaxCalculation.TaxPercentageRates.PercentageRate,
                        TaxIncomeBrackets.ToIncomeBracket,
                        FlatValueTaxesTaxCalculation.FlatValueTaxes.Base
                    }
                ).OrderBy(TaxPercentageRates => TaxPercentageRates.PercentageRate).ThenBy(TaxIncomeBrackets => TaxIncomeBrackets.ToIncomeBracket).SingleOrDefault();           


        if (incomePerAnum <= query.ToIncomeBracket)
            {
                // If income is less than 200,000, apply a 5% tax rate.
                return incomePerAnum * (query.PercentageRate/100);
            }
            else
            {
                // If income is 200,000 or more, use a flat tax of 10,000 per year.
                return query.Base;
            }
        }

        public double CalculateFlatRateTax(double income)
        {
            var query = _dbcontext.FlatRateTaxes
            .Join(
                _dbcontext.TaxPercentageRates,
                FlatRateTaxes => FlatRateTaxes.TaxPercentageRateId,
                TaxPercentageRates => TaxPercentageRates.TaxPercentageRateId,
                (FlatValueTaxes, TaxPercentageRates) => new
                {                    
                    TaxPercentageRates.PercentageRate
                }
            ).OrderBy(TaxPercentageRates => TaxPercentageRates.PercentageRate).SingleOrDefault();

            // Calculate tax at a flat rate of 17.5% of the income.
            return income * query.PercentageRate / 100;
        }
    }

   


}

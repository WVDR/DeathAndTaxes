using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class FlatValueTax
    {
        public int FlatValueTaxId { get; set; }
        public string Description { get; set; }
        public double Base { get; set; }
        public int Months { get; set; }
        // Navigation properties
        public int TaxPercentageRateId { get; set; }
        public TaxPercentageRate TaxPercentageRate { get; set; }
        public int TaxIncomeBracketId { get; set; }
        public TaxIncomeBracket IncomeBracket { get; set; }
    }
}

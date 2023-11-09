using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class TaxPercentageRate
    {
        public int TaxPercentageRateId { get; set; }
        public double PercentageRate { get; set; }

        // Navigation properties
        public List<ProgressiveTax> ProgressiveTaxes { get; set; }
        public List<FlatValueTax> FlatValueTaxes { get; set; }
        public List<FlatRateTax> FlatRateTaxes { get; set; }
        
    }
}

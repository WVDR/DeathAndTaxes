using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class FlatRateTax
    {
        public int FlatRateTaxId { get; set; }
        public string Description { get; set; }
        // Navigation properties
        public int TaxPercentageRateId { get; set; }
        public TaxPercentageRate TaxPercentageRate { get; set; }
    }
}

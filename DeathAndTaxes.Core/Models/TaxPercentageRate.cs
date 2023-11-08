using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    internal class TaxPercentageRate
    {
        public int TaxPercentageId { get; set; }
        public string Percentage { get; set; }

        //// Navigation property for relating to TaxCalculationType
        //public int TaxCalculationTypeId { get; set; }
        //public TaxCalculationType TaxCalculationType { get; set; }
    }
}

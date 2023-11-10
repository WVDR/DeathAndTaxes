using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class PostalCode
    {
        public int PostalCodeId { get; set; }
        public string Code { get; set; }

        // Navigation properties
        public int TaxCalculationTypeId { get; set; }
        public TaxCalculationType TaxCalculationType { get; set; }
        public List<TaxScore> TaxScores { get; set; }
    }

}

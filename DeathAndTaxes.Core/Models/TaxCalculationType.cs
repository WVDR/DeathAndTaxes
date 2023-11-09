using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class TaxCalculationType
    {
        public int TaxCalculationTypeId { get; set; }
        public string Name { get; set; }

        // Navigation properties
        public List<PostalCode> PostalCodes { get; set; }
    }

}

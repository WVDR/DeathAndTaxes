using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class TaxIncomeBracket
    {
        public int TaxIncomeBracketId { get; set; }
        public string IncomeBracket { get; set; }

        // Navigation properties
        public List<ProgressiveTax> ProgressiveTaxes { get; set; }
        public List<FlatValueTax> FlatValueTaxes { get; set; }
    }
}

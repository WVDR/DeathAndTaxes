using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Core.Models
{
    public class TaxScore
    {
        public int TaxScoreId { get; set; }
        public string User { get; set; }// TODO: when we have authentication, allow the user id to be linked here        
        public double Income { get; set; }
        public double Tax { get; set; }
        public DateTime DateCapotured { get; set; }
        // Navigation properties        
        public int PostalCodeId { get; set; }
        public PostalCode PostalCode { get; set; }
    }
}

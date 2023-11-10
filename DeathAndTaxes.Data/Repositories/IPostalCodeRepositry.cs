using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data.Repositories
{
    public interface IPostalCodeRepositry
    {
        public string GetPostalCode(string postalcode);
        public string GetPostalCodes();
    }
}

using DeathAndTaxes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathAndTaxes.Data.Repositories
{
    
    public class PostalCodeRepositry : IPostalCodeRepositry
    {
        private readonly DeathAndTaxesDbContext _dbcontext;

        public PostalCodeRepositry(DeathAndTaxesDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<string> GetPostalCodes()
        {
            var query = _dbcontext.PostalCodes
                .Join(
                    _dbcontext.TaxCalculationTypes,
                    PostalCodes => PostalCodes.TaxCalculationTypeId,
                    TaxCalculationTypes => TaxCalculationTypes.TaxCalculationTypeId,
                    (PostalCodes, TaxCalculationTypes) =>  PostalCodes.Code+ ','+ TaxCalculationTypes.Name
                    
                ).ToList();
            return query;
        }

        public string GetTaxCalculationType(string postalcode)
        {
            var query = _dbcontext.PostalCodes.Where(pc => pc.Code == postalcode)
                .Join(
                    _dbcontext.TaxCalculationTypes,
                    PostalCodes => PostalCodes.TaxCalculationTypeId,
                    TaxCalculationTypes => TaxCalculationTypes.TaxCalculationTypeId,
                    (PostalCodes, TaxCalculationTypes) => new
                    {
                        code = PostalCodes.Code,
                        name = TaxCalculationTypes.Name
                    }
                ).SingleOrDefault();
            return query.name;
            
        }
    }
}

using DeathAndTaxes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Xml;
using Microsoft.Extensions.Options;

namespace DeathAndTaxes.Data.Repositories
{
    
    public class PostalCodeRepositry : IPostalCodeRepositry
    {
        private readonly DeathAndTaxesDbContext _dbcontext;

        private JsonSerializerOptions _jsonSerializerOptions = new()
        {
            WriteIndented = true
        };
        public PostalCodeRepositry(DeathAndTaxesDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public string GetPostalCodes()
        {
            var query = _dbcontext.PostalCodes
                .Join(
                    _dbcontext.TaxCalculationTypes,
                    PostalCodes => PostalCodes.TaxCalculationTypeId,
                    TaxCalculationTypes => TaxCalculationTypes.TaxCalculationTypeId,
                    (PostalCodes, TaxCalculationTypes) => new { PostalCodes.PostalCodeId, PostalCodes.Code, TaxCalculationTypes.Name }

                ).ToList();
            
            return JsonSerializer.Serialize(query, _jsonSerializerOptions);
        }

        public string GetPostalCode(string postalcode)
        {
            var query = _dbcontext.PostalCodes.Where(pc => pc.Code == postalcode)
                .Join(
                    _dbcontext.TaxCalculationTypes,
                    PostalCodes => PostalCodes.TaxCalculationTypeId,
                    TaxCalculationTypes => TaxCalculationTypes.TaxCalculationTypeId,
                    (PostalCodes, TaxCalculationTypes) => new { PostalCodes.PostalCodeId, PostalCodes.Code, TaxCalculationTypes.Name }
                ).SingleOrDefault();
            return JsonSerializer.Serialize(query, _jsonSerializerOptions);

        }
    }
}

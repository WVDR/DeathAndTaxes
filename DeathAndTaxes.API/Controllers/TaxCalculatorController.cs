using DeathAndTaxes.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeathAndTaxes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculatorRepositry _taxCalculatorRepositry;
        public TaxCalculatorController(ITaxCalculatorRepositry taxCalculatorRepositry)
        {
            _taxCalculatorRepositry = taxCalculatorRepositry;
        }

        // GET: api/<TaxCalculatorController>
        [HttpGet("ProgressiveTax")]
        public double GetProgressiveTax(double income)
        {
            var result = _taxCalculatorRepositry.calculateProgressive(income);
            return result;
        }        

        // POST api/<TaxCalculatorController>
        [HttpPost("CalculateAndStoreTax")]
        public double Post(string postcode, double income )
        {
            var result = _taxCalculatorRepositry.CalculateAndStoreTax(postcode,income);
            return result;

        }

        [HttpGet("TaxScores")]
        public string TaxScores()
        {
            var result = _taxCalculatorRepositry.TaxScores();
            return result;
        }
    }
}

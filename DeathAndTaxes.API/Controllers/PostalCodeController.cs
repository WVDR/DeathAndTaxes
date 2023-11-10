using DeathAndTaxes.Core.Models;
using DeathAndTaxes.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DeathAndTaxes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostalCodeController : ControllerBase
    {
        private readonly IPostalCodeRepositry _postalCodeRepositry;
        public PostalCodeController(IPostalCodeRepositry postalCodeRepositry)
        {
            _postalCodeRepositry = postalCodeRepositry;
        }

        // GET: api/<PostalCodeController>
        [HttpGet]
        public string Get()
        {
            var result = _postalCodeRepositry.GetPostalCodes();
            return result;
        }

        // GET api/<PostalCodeController>/7441
        [HttpGet("{postalcode}")]
        public string Get(string postalcode)
        {
            var result = _postalCodeRepositry.GetPostalCode(postalcode);
            return result;
        }      
    }
}

using DeathAndTaxes.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace DeathAndTaxes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string postcode, string income)
        {
            
            using (var client = new HttpClient())
            {
                var baseUrl = _config.GetValue<string>("ApiUrl"); // "appsettings inected"
                var apiUrl = baseUrl+"TaxCalculator/CalculateAndStoreTax";
                var request = new HttpRequestMessage(HttpMethod.Post, $"{apiUrl}?postcode={postcode}&income={income}");
                var content = new StringContent("", encoding: null, mediaType: "text/plain");
                request.Content = content;

                try
                {                    
                    var response = await client.SendAsync(request);                    
                    if (!response.IsSuccessStatusCode)
                    {
                        // Handle non-success status codes
                        var erormessage = $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                        ViewBag.Tax = erormessage;
                        Console.WriteLine(erormessage);
                    }
                    else
                    {
                        ViewBag.Tax = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (HttpRequestException ex)
                {
                    var erormessage = $"An error occurred: {ex.Message}";
                    // Handle exception
                    Console.WriteLine(erormessage);
                    ViewBag.Tax = erormessage;
                }


            }            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
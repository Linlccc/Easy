using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Easy.Tool.MergeJson.NuGetTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing11", "Bracing2", "Chilly3",
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = new Random().Next(53, 55),
                Summary = Summaries[new Random().Next(Summaries.Length)]
            })
            .ToArray();

            return result;
        }

        [HttpGet("Ex")]
        public string Ex()
        {
            throw new System.Exception("异常测试");
        }
    }
}

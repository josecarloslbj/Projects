using JC.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace JC.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , ICategoriaService categoriaService)
        {
            _logger = logger;
            _categoriaService = categoriaService;
        }



        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var result =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var teste =  _categoriaService.ObterCategoriasPermissao().Result;

            return result;
        }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        
        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.Get();
        }
    }
}

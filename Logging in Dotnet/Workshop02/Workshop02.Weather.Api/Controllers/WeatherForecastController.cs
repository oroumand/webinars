using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop02.Weather.Domain;

namespace Workshop02.Weather.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                         IWeatherForecastRepository weatherForecastRepository)
        {
            _logger = logger;
            _weatherForecastRepository = weatherForecastRepository;
            _logger.LogDebug("create new instance of controller {controller}", nameof(WeatherForecastController));
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogDebug("Start New Request for controller {controller} and action {ActionName}", nameof(WeatherForecastController), nameof(Get));
            return Ok(_weatherForecastRepository.GetAll());
        }

        [HttpGet("{cityName}")]
        public IActionResult Get(string cityName)
        {
            _logger.LogDebug("Start New Request for controller {controller} and action {ActionName} with city name {CityName}", nameof(WeatherForecastController), nameof(Get), cityName);
            var result = _weatherForecastRepository.GetByCityName(cityName);
            if (result == null)
                return NotFound(cityName);
            return Ok(result);
        }

    }
}

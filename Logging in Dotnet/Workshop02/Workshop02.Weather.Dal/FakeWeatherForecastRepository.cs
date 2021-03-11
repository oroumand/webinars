using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Workshop02.Weather.Domain;

namespace Workshop02.Weather.Dal
{
    public class FakeWeatherForecastRepository : IWeatherForecastRepository
    {
        private static List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast
            {
                CityName = "تهران",
                Summary = "سرد",
                TemperatureC = 9
            },
            new WeatherForecast
            {
                CityName = "اصفهان",
                Summary = "بسیار سرد",
                TemperatureC = 2
            },
            new WeatherForecast
            {
                CityName = "کردستان",
                Summary = "یخ بندان",
                TemperatureC = 0
            },
            new WeatherForecast
            {
                CityName = "زنجان",
                Summary = "برفی",
                TemperatureC = 2
            },
            new WeatherForecast
            {
                CityName = "تبریز",
                Summary = "برفی",
                TemperatureC = 1
            },
            new WeatherForecast
            {
                CityName = "رشت",
                Summary = "بارانی",
                TemperatureC = 17
            },
            new WeatherForecast
            {
                CityName = "مشهد",
                Summary = "وزش باد",
                TemperatureC = 3
            }
        };
        private readonly ILogger<FakeWeatherForecastRepository> _logger;

        public FakeWeatherForecastRepository(ILogger<FakeWeatherForecastRepository> logger)
        {
            _logger = logger;
        }
        public List<WeatherForecast> GetAll()
        {
            _logger.LogTrace("Start Execution of {MethodName}", nameof(GetAll));
            var result = _weatherForecasts;

            if (result == null)
            {
                _logger.LogWarning("There is no result in {MethodName}", nameof(GetAll));
            }
            else
            {
                _logger.LogDebug("There is {Count} result for Forecast", result.Count);
            }
            _logger.LogTrace("Finished Execution of {MethodName}", nameof(GetByCityName));

            return result;
        }

        public WeatherForecast GetByCityName(string cityName)
        {
            _logger.LogTrace("Start Execution of {MethodName} parameter is {parameter}", nameof(GetByCityName), cityName);
            var result = _weatherForecasts.FirstOrDefault(c => c.CityName == cityName);
            if (result == null)
            {
                _logger.LogWarning("WeatherForecast for city {City} not found", cityName);
            }
            _logger.LogTrace("Finished Execution of {MethodName} parameter is {parameter}", nameof(GetByCityName), cityName);
            return result;
        }
    }
}

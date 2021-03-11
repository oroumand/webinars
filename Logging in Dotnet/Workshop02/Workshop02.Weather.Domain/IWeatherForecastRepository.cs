using System.Collections.Generic;

namespace Workshop02.Weather.Domain
{
    public interface IWeatherForecastRepository
    {
        List<WeatherForecast> GetAll();
        WeatherForecast GetByCityName(string cityName);

    }
}

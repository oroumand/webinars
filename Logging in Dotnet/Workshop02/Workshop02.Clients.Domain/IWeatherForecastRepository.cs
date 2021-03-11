using System.Collections.Generic;
using System.Threading.Tasks;

namespace Workshop02.Clients.Domain
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetAllAsync();
        Task<WeatherForecast> GetByCityName(string cityName);

    }
}

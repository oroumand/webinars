using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Workshop02.Clients.Domain;

namespace Workshop02.Clients.Mvc.Services
{
    public class WeatherForecastWebClient : IWeatherForecastRepository
    {
        private readonly HttpClient client;
        private readonly ILogger<WeatherForecastWebClient> logger;
        public WeatherForecastWebClient(HttpClient client, ILogger<WeatherForecastWebClient> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task<List<WeatherForecast>> GetAllAsync()
        {
            try
            {
                var response = await client.GetAsync("/WeatherForecast");

                if (response.IsSuccessStatusCode)
                {
                    return await response.ReadContentAs<List<WeatherForecast>>();
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected exception occurred when loading event data");
            }

            return Array.Empty<WeatherForecast>().ToList();
        }

        public async Task<WeatherForecast> GetByCityName(string cityName)
        {
            using var scope = logger.BeginScope("Loading event {cityName}", cityName);

            try
            {
                var response = await client.GetAsync($"/WeatherForecast/{cityName}");

                if (response.IsSuccessStatusCode)
                {
                    var @event = await response.ReadContentAs<WeatherForecast>();

                    logger.LogDebug("Successfully loaded event '{GloboTicketEventName}'", @event.CityName);

                    return @event;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected exception occurred when loading event data");
            }

            logger.LogWarning("Returning null event");
            return null;
        }
    }
}

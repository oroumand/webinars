namespace Workshop02.Clients.Domain
{
    public class WeatherForecast
    {

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public string CityName { get; set; }
    }
}

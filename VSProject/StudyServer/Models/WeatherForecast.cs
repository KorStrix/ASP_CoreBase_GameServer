using System;

namespace StudyServer
{
    /// <summary>
    /// ASP.net Core 솔루션 생성하면 나오는 예제 Model
    /// </summary>
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}

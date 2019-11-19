using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class CurrentWeatherDisplayModel
    {
        public string Temperature { get; set; }

        public string Forecast { get; set; }

        public string LastUpdated { get; set; }

        public string MinTemp { get; set; }

        public string CurrentTemp { get; set; }

        public string MaxTemp { get; set; }

        public string Name { get; set; }
    }
}

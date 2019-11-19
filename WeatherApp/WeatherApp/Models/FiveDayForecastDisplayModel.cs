using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class FiveDayForecastDisplayModel
    {
        public string Temperature { get; set; }

        public DateTime Date { get; set; }

        public string Icon { get; set; }
        public string  Day { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class FiveDayForecastModel
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<WeatherList> list { get; set; }
    }

    [JsonObject("List")]
    public class WeatherList
    {
        public Main main { get; set; }

        public List<Weather> weather { get; set; }

        public string dt_txt { get; set; }
    }
}

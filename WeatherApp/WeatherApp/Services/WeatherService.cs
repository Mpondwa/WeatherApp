using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Resources;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private string _apiKey;

        public WeatherService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GetCurrentWeather(string latitude, string longitude)
        {
            string url =  $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";

            try
            {
                HttpClient client = new HttpClient();
                var results = await client.GetAsync(url);

                string data = await results.Content.ReadAsStringAsync();

                return data;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetCurrentWeather error: " + ex.Message);
                throw;
            }
        }

        public async Task<string> GetFiveDayForecast(string latitude, string longitude)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";

            try
            {
                HttpClient client = new HttpClient();
                var results = await client.GetAsync(url);

                string data = await results.Content.ReadAsStringAsync();

                return data;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("GetFiveDayForecast error: " + ex.Message);
                throw;
            }
        }

    }
}

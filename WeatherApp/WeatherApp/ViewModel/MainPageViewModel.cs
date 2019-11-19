using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Resources;
using WeatherApp.Services;
using Xamarin.Essentials;

namespace WeatherApp.ViewModel
{
    public class MainPageViewModel : BindableBase
    {
        WeatherService _service;
        CurrentWeatherModel _currentWeather;

        public MainPageViewModel()
        {
            _service = new WeatherService(AppResource.OpenWeatherKey);
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private string _currentWeatherBG = AppResource.SunnyBG;

        public string CurrentWeatherBG
        {
            get { return _currentWeatherBG; }
            set { SetProperty(ref _currentWeatherBG, value); }
        }

        private CurrentWeatherDisplayModel currentWeatherModel;

        public CurrentWeatherDisplayModel CurrentWeather
        {
            get { return currentWeatherModel; }
            set { SetProperty(ref currentWeatherModel, value); }
        }

        private FiveDayForecastDisplayModel _fiveDayForecastDisplayModel;

        private ObservableCollection<FiveDayForecastDisplayModel> _fiveDayForecast;

        public ObservableCollection<FiveDayForecastDisplayModel> FiveDayForecast
        {
            get { return _fiveDayForecast; }
            set { SetProperty(ref _fiveDayForecast, value); }
        }

        public async Task GetCurrentWeather()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                string lat = "";
                string lon = "";

                if (location != null)
                {
                    lat = location.Latitude.ToString();
                    lon = location.Longitude.ToString();
                }

                string jsonResult = await _service.GetCurrentWeather(lat, lon);

                _currentWeather = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentWeatherModel>(jsonResult);
                CurrentWeather = new CurrentWeatherDisplayModel
                {
                    Forecast = _currentWeather.weather[0].main,
                    Temperature = Convert.ToInt32(_currentWeather.main.temp).ToString(),
                    LastUpdated = DateTime.Now.ToString("dd MMM, hh:mm"),
                    CurrentTemp = Convert.ToInt32(_currentWeather.main.temp).ToString(),
                    MinTemp = Convert.ToInt32(_currentWeather.main.temp_min).ToString(),
                    MaxTemp = Convert.ToInt32(_currentWeather.main.temp_max).ToString(),
                    Name = _currentWeather.name
                };
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetFiveDayForecast()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                string lat = "";
                string lon = "";

                if (location != null)
                {
                    lat = location.Latitude.ToString();
                    lon = location.Longitude.ToString();
                }

                string jsonResult = await _service.GetFiveDayForecast(lat, lon);

                var fiveDayForecast = Newtonsoft.Json.JsonConvert.DeserializeObject<FiveDayForecastModel>(jsonResult);

                var tempFiveDayForecast = new List<FiveDayForecastDisplayModel>();

                foreach (var item in fiveDayForecast.list)
                {
                    DateTime date = DateTime.ParseExact(item.dt_txt, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    tempFiveDayForecast.Add(new FiveDayForecastDisplayModel
                    {
                        Day = date.DayOfWeek.ToString(),
                        Date = date,
                        Icon = GetIcon(item.weather[0].id),
                        Temperature = Convert.ToInt32(item.main.temp).ToString()
                    });

                }

                var distinctDays = new List<FiveDayForecastDisplayModel>();

                foreach (var item in tempFiveDayForecast)
                {
                    if (!distinctDays.Any(d => d.Day == item.Day))
                    {
                        distinctDays.Add(item);
                    }
                }

                FiveDayForecast = new ObservableCollection<FiveDayForecastDisplayModel>(distinctDays);

            }
            catch (Exception ex)
            {

            }
        }

        private string GetIcon(int weatherId)
        {
            if (weatherId >= 500 && weatherId < 531)
            {
                return AppResource.RainyIcon;
            }
            else if (weatherId == 800)
            {
                return AppResource.SunnyIcon;
            }
            if (weatherId >= 801 && weatherId < 805)
            {
                return AppResource.CloudyIcon;
            }

            return AppResource.SunnyIcon;
        }

        public void SetTheme()
        {
            if (_currentWeather.weather[0].id >= 500 && _currentWeather.weather[0].id < 531)
            {
                App.Current.Resources["themeColour"] = AppResource.RainyColor;
                CurrentWeatherBG = AppResource.RainyBG;
            }
            else if (_currentWeather.weather[0].id == 800)
            {
                App.Current.Resources["themeColour"] = AppResource.SunnyColor;
                CurrentWeatherBG = AppResource.SunnyBG;
            }
            if (_currentWeather.weather[0].id >= 801 && _currentWeather.weather[0].id < 805)
            {
                App.Current.Resources["themeColour"] = AppResource.CloudyColor;
                CurrentWeatherBG = AppResource.CloudyBG;
            }
        }
    }
}

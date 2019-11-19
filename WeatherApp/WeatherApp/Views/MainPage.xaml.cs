using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.ViewModel;
using Xamarin.Forms;

namespace WeatherApp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.IsLoading = true;

            await _viewModel.GetCurrentWeather();

            _viewModel.SetTheme();

            await _viewModel.GetFiveDayForecast();


            _viewModel.IsLoading = false;
        }
    }
}

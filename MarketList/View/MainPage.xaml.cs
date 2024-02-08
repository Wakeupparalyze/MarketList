
using FootballLeague;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using FootballLeague.Model;
using FootballLeague.Tools;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FootballLeague
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        } 

        protected override void OnAppearing()
        {
            leagueListView.ItemsSource = App.DB.GetLeagues();
        }

        private void LeagueButton_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Shell.Current.GoToAsync($"LeagueList?LeagueName={button.Text}");
        }
    }
}

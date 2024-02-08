
using System;
using FootballLeague;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FootballLeague.Model;
using FootballLeague.Tools;
using System.Collections.ObjectModel;

namespace FootballLeague
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {

        public AddPage()
        { 
           
            InitializeComponent();
            LeagueList.ItemsSource = new ObservableCollection<League>(App.DB.GetLeagues());
        }
     
        private void AddButton(object sender, EventArgs e)
        {
            League league = new League { Name = AddEntry.Text };
            App.DB.EditLeague(league);

            OnAppearing();
            messagelabel.Text = "Добавлено";
        }

        private void EditButton(object sender, EventArgs e)
        {

            if (LeagueList.SelectedItem != null)
            {
                League selectedLeague = LeagueList.SelectedItem as League;
                if (AddEntry.Text != null)
                {
                    selectedLeague.Name = AddEntry.Text;
                   
                    LeagueList.ItemsSource = new ObservableCollection<League>(App.DB.GetLeagues());
                    messagelabel.Text = "Сохранено";
                }
                else
                {
                    messagelabel.Text = "Введите название лиги";
                }
            }
            else
            {
                messagelabel.Text = "Выберите название клуба";
            }
        }
        protected override void OnAppearing()
        {
            LeagueList.ItemsSource = new ObservableCollection<League>(App.DB.GetLeagues().ToList());
        }


        private void DeleteButton(object sender, EventArgs e)
        {
            if (LeagueList.SelectedItem != null)
            {
                League selectedLeague = LeagueList.SelectedItem as League;
                App.DB.DeleteLeague(selectedLeague);

                OnAppearing();
                messagelabel.Text = "Удалено";
            }
            else
            {
                messagelabel.Text = "Выберите лигу для удаления";
            }
        }

        private void LeagueList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (LeagueList.SelectedItem != null)
            {
                AddEntry.Text = ((League)LeagueList.SelectedItem).Name;
            }
        }
    }
}

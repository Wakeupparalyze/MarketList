using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballLeague;
using FootballLeague.Model;
using FootballLeague.Tools;
using Microsoft.EntityFrameworkCore;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FootballLeague.MainPage;

namespace FootballLeague
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(LeagueName), "LeagueName")]  //Свойство запроса, LeagueName - это имя свойства. Идентификатор параметра запроса.
    public partial class LeagueList : ContentPage
    {
        public League _league ;
        public string leagueName;
        
        public string LeagueName
        {
            get => leagueName; //Метод доступа в C# - это часть свойства (property), которая определяет, как можно получить (через get) и установить (через set) значение свойства.
            set
            {             
                leagueName = value;
                League league = App.DB.GetLeagueByName(LeagueName);
                _league = league;
                AllItem.ItemsSource = new ObservableCollection<Club>(App.DB.GetClubs().Where(s => s.IdLeague == _league.ID));
            }
        }

        public LeagueList()
        {
            InitializeComponent();
        }

        private void AddButton(object sender, EventArgs e)
        {

            Club club = new Club { Name = AddEntry.Text, IdLeague = _league.ID };
            App.DB.EditClub(club);

            OnAppearing();
            messagelabel.Text = "Добавлено";
        }

        private void EditButton(object sender, EventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                Club selectedClub = AllItem.SelectedItem as Club;
                if (AddEntry.Text != null)
                {
                    selectedClub.Name = AddEntry.Text;

                    AllItem.ItemsSource = new ObservableCollection<Club>(App.DB.GetClubs());

                    OnAppearing();
                    messagelabel.Text = "Сохранено";
                }
                else
                {
                    messagelabel.Text = "Введите название клуба";
                }
            }
            else
            {
                messagelabel.Text = "Выберите название клуба";
            }
        }
    
        protected override void OnAppearing()
        {
            AllItem.ItemsSource = new ObservableCollection<Club>(App.DB.GetClubs().Where(s => _league != null && s.IdLeague == _league.ID));
        }

        private void DeleteButton(object sender, EventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                Club selectedClubs = AllItem.SelectedItem as Club;
                App.DB.DeleteClub(selectedClubs);
                OnAppearing();
                messagelabel.Text = "Удалено";
            }
            else
            {
                messagelabel.Text = "Выберите клуб для удаления";
            }
        }

        private void AllItem_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (AllItem.SelectedItem != null)
            {
                AddEntry.Text = ((Club)AllItem.SelectedItem).Name;
            }
        }
    }
}

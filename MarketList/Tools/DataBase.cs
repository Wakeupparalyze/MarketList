using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using FootballLeague.Model;
using Xamarin.Forms;

namespace FootballLeague.Tools
{
    public class DataBase
    {
        private const string FILE_NAME = "db.db";
        public DboContext dB;

        public DboContext DB
        {
            get
            {
                if (dB == null)
                {
                    string path = DependencyService.Get<IDBPath>().GetDBPath(FILE_NAME);
                    dB = new DboContext(path);
                    dB.Database.EnsureCreated();
                }
                return dB;
            }
        }

        public DataBase()
        {

            List<League> leagues = new List<League>
            {
                new League { ID = 1, Name = "Лалига" },
                new League { ID = 2, Name = "АПЛ" },
                new League { ID = 3, Name = "Лига1" }
            };

            List<Club> clubs = new List<Club>
            {
                new Club { ID = 1, Name = "Реал Мадрид", IdLeague = 1 },
                new Club { ID = 2, Name = "Барселона", IdLeague = 1 },
                new Club { ID = 3, Name = "Атлетимкум Мадрид", IdLeague = 1 },
                new Club { ID = 4, Name = "Манчестер Красный", IdLeague = 2 },
                new Club { ID = 5, Name = "Манчестер Синий", IdLeague = 2 },
                new Club { ID = 6, Name = "Арсенал", IdLeague = 2 },
                new Club { ID = 7, Name = "Лиль", IdLeague = 3 },
                new Club { ID = 8, Name = "ПСЖ", IdLeague = 3 },
                new Club { ID = 9, Name = "Монпелье", IdLeague = 3 }
            };
        }

       
        public League GetByLeagueID(int id)
        {
            return DB.Leagues.FirstOrDefault(c => c.ID == id);
        }
        public Club GetByClubsID(int id)
        {
            return DB.Clubs.FirstOrDefault(c => c.ID == id);
        }
        public League GetLeagueByName(string leagueName)
        {

            return DB.Leagues.FirstOrDefault(c => c.Name == leagueName);
            
        }

        public void EditClub(Club club)
        {
            if (club.ID == 0)
            {
                DB.Clubs.Add(club);
            }
            else
            {
                Club club1 = GetByClubsID(club.ID);
                DB.Entry(club1).CurrentValues.SetValues(club);
            }
            DB.SaveChanges();

        }

        public ObservableCollection<League> GetLeagues()
        {
            return new ObservableCollection<League>(DB.Leagues.ToList());

        }

        public ObservableCollection<Club> GetClubs()
        {
            return new ObservableCollection<Club>(DB.Clubs.ToList());
        }
        public League DeleteLeague(League league)
        {

            if (league != null)
            {
                DB.Leagues.Remove(league);
            }
            DB.SaveChanges();
            return league;
        }

        public void DeleteClub(Club club)
        {

            if (club != null)
            {
                DB.Clubs.Remove(club);
            }
           DB.SaveChanges();
        }

        public void EditLeague(League league)
        {
            if(league.ID == 0)
            {   
                DB.Leagues.Add(league);
            }
            else
            {
                League league1 = GetByLeagueID(league.ID);
                DB.Entry(league1).CurrentValues.SetValues(league);
            }
            DB.SaveChanges();
        }
    }
}

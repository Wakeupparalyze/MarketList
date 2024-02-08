﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballLeague;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace FootballLeague
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();         
            Routing.RegisterRoute("LeagueList", typeof(FootballLeague.LeagueList));
        }
    }
}

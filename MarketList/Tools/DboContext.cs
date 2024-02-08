using FootballLeague.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballLeague.Tools
{
    public class DboContext : DbContext
    {

        private string _filePath;

        public DboContext(string filePath)
        {
            _filePath = filePath;
        }

        public DbSet<Club> Clubs { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_filePath}");
            base.OnConfiguring(optionsBuilder);
        }
      
      
    }
}

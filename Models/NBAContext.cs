using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NBA_ManagementSystem.Models
{
    public class NBAContext : DbContext
    {
        public NBAContext() : base("NBA_DB") { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<UserTeamPlayer> UserTeamPlayers { get; set; }

    }
}
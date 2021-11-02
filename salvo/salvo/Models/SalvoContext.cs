﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salvo.Models
{
    public class SalvoContext : DbContext
    {
        public SalvoContext(DbContextOptions<SalvoContext> options): base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        //public DbSet<GamePlayer> GamePlayers { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}

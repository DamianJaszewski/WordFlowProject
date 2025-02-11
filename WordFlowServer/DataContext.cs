﻿using Microsoft.EntityFrameworkCore;
using WordFlowServer.Models;

namespace WordFlowServer
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<WordFlowServer.Models.Card> Card { get; set; } = default!;
    }
}

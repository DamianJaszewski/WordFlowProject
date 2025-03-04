
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordFlowServer.Models;

namespace WordFlowServer
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Card> Card { get; set; } = default!;
        public DbSet<Repetition> Repetitions { get; set; }
    }
}

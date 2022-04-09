using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _421final.Models;

namespace _421final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<_421final.Models.Team> Team { get; set; }
        public DbSet<_421final.Models.Position> Position { get; set; }
        public DbSet<_421final.Models.Player> Player { get; set; }
    }
}
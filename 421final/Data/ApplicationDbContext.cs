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
        public DbSet<_421final.Models.TeamStyle> TeamStyle { get; set; }
        public DbSet<_421final.Models.MyTeam> MyTeam { get; set; }
        public DbSet<_421final.Models.TeamRoot> TeamRoot { get; set; }
        public DbSet<_421final.Models.PlayerRoot> PlayerRoot { get; set; }
    }
}
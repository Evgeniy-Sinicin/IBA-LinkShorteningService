using LinkShorteningService.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LinkShorteningService.DataAccess.Repositories.EFRepositories.Models
{
    public class LinksContext : DbContext
    {
        public LinksContext(DbContextOptions<LinksContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<EfLink> Links { get; set; }
        public virtual DbSet<EfGroup> Groups { get; set; }
    }
}

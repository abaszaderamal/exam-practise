using AgencyBackend.Data.Configuration;
using AgencyBackend.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgencyBackend.Data.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.Entity<Portfolio>().HasData(
                
                new Portfolio { Id=1, Image= "watch.jpg",Title="Lorem Ipsum1",Info="Lorem Ipsum Dolor Sit Amet" },
                new Portfolio { Id=2, Image = "watch.jpg", Title = "Lorem Ipsum2", Info = "Lorem Ipsum Dolor Sit Amet" },
                new Portfolio { Id=3, Image = "watch.jpg", Title = "Lorem Ipsum3", Info = "Lorem Ipsum Dolor Sit Amet" },
                new Portfolio { Id=4, Image = "watch.jpg", Title = "Lorem Ipsum4", Info = "Lorem Ipsum Dolor Sit Amet" },
                new Portfolio { Id=5, Image = "watch.jpg", Title = "Lorem Ipsum5", Info = "Lorem Ipsum Dolor Sit Amet" },
                new Portfolio { Id=6, Image = "watch.jpg", Title = "Lorem Ipsum6", Info = "Lorem Ipsum Dolor Sit Amet" }

                );
            base.OnModelCreating(modelBuilder);
        }
    }
}

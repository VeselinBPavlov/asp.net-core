namespace IntroductionToAspNetCore.Data
{
    using Microsoft.EntityFrameworkCore;

    public class CatsDbContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public CatsDbContext(DbContextOptions<CatsDbContext> options)
            :base(options)
        {
        }
    }
} 

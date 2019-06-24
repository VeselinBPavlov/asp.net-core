namespace Panda.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Panda.Domain.Models;

    public class PandaDbContext : IdentityDbContext<PandaUser>
    {
        public DbSet<PandaUser> PandaUsers { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public PandaDbContext(DbContextOptions<PandaDbContext> options)
            : base(options)
        {
        }

        public PandaDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PandaUser>().HasMany(x => x.Packages)
                .WithOne(x => x.Recipient).HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PandaUser>().HasMany(x => x.Receipts)
                .WithOne(x => x.Recipient).HasForeignKey(x => x.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Receipt>().HasOne(r => r.Package)
                .WithOne(p => p.Receipt)
                .HasForeignKey<Receipt>(r => r.PackageId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}

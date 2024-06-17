using BasicWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BasicWebApi.Persistance
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Country>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Contact>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Contacts)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId);

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Contacts)
                .WithOne(e => e.Country)
                .HasForeignKey(e => e.CountryId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

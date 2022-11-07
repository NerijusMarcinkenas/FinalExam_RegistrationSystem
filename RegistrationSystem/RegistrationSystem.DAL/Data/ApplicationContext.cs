using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RegistrationSystem.Core.Enums;
using RegistrationSystem.Core.Models;

namespace RegistrationSystem.DAL.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<PersonImage> PersonImages => Set<PersonImage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(r => r.Role)
                  .HasConversion(
                    r => r.ToString(),
                    r => (Roles)Enum.Parse(typeof(Roles), r)
                );

            modelBuilder.Entity<User>()
                .Property(r => r.Role)
                .HasDefaultValue(Roles.User);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique(false);

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.PersonalNumber)
                .IsUnique(true);
        }
    }
}

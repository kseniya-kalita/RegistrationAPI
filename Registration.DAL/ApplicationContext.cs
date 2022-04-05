using Microsoft.EntityFrameworkCore;
using Registration.Abstractions.Models;

namespace Registration.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            // In a real project i would use migrations instead of this approach
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                    .Property(u => u.Id)
                    .HasColumnName("id");

            modelBuilder.Entity<Company>()
                    .HasAlternateKey(u => u.Name)
                    .HasName("name");

            modelBuilder.Entity<User>()
                    .HasKey(u => u.Id)
                    .HasName("id");
            
            modelBuilder.Entity<User>()
                    .Property(u => u.Email)
                    .HasColumnName("email");
            
            modelBuilder.Entity<User>()
                    .Property(u => u.Password)
                    .HasColumnName("password");
            
            modelBuilder.Entity<User>()
                    .Property(u => u.CompanyId)
                    .HasColumnName("company_id");

            base.OnModelCreating(modelBuilder);
        }
    }
}

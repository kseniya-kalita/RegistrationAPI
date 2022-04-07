using Microsoft.EntityFrameworkCore;
using Registration.Abstractions.DbEntities;

namespace Registration.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserDbEntity> Users { get; set; }
        public DbSet<CompanyDbEntity> Companies { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            // In a real project i would use migrations instead of this approach
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyDbEntity>()
                    .Property(u => u.Id)
                    .HasColumnName("id");

            modelBuilder.Entity<CompanyDbEntity>()
                    .HasAlternateKey(u => u.Name)
                    .HasName("name");

            modelBuilder.Entity<UserDbEntity>()
                    .HasKey(u => u.Id)
                    .HasName("id");
            
            modelBuilder.Entity<UserDbEntity>()
                    .Property(u => u.Email)
                    .HasColumnName("email");
            
            modelBuilder.Entity<UserDbEntity>()
                    .Property(u => u.Password)
                    .HasColumnName("password");
            
            modelBuilder.Entity<UserDbEntity>()
                    .Property(u => u.CompanyId)
                    .HasColumnName("company_id");

            base.OnModelCreating(modelBuilder);
        }
    }
}

using Domain.AggregatesModel.UserAggragate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder
                .Property(_ => _.Id)
                .HasColumnName("id");

            builder
                .Property<string>("_email")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("email")
                .HasAnnotation("email_index",
                    new IndexAttribute("Index") { IsUnique = true } )
                .IsRequired();

            builder
                .Property<string>("_password")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("password")
                .IsRequired();
        }
    }
}

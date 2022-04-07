using Domain.AggregatesModel.UserAggragate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration
{
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies", ApplicationContext.DEFAULT_SCHEMA);

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder
                .Property(_ => _.Name)
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("name")
                .IsRequired();

            var navigation = builder.Metadata.FindNavigation(nameof(Company.Users));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}

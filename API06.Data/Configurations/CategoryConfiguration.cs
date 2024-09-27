using API06.App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API06.Data.Configurations;

public class CategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow);
        
        builder.Property(c=>c.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
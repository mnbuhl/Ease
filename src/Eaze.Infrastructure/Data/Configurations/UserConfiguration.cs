using Eaze.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eaze.Infrastructure.Data.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Name)
            .HasMaxLength(255);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.NormalizedEmail)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(u => u.NormalizedUsername)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(u => new
        {
            u.Email,
            u.NormalizedEmail,
            u.Username,
            u.NormalizedUsername,
        })
            .IsUnique();
    }
}
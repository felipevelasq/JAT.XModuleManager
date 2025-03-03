using JAT.IdentityService.Domain;
using JAT.IdentityService.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JAT.IdentityService.Infrastructure.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Username);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.Email);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(MaxLengths.User.PasswordHash);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasIndex(x => x.Username)
            .IsUnique();

        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}
namespace LowlandTech.Accounts.Domain.Configurations;

public class AuthLoginConfiguration : IEntityTypeConfiguration<AuthLogin>
{
    public void Configure(EntityTypeBuilder<AuthLogin> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Provider).HasMaxLength(250);
        b.Property(x => x.ProviderUserId).HasMaxLength(250);
        b.Property(x => x.AccessToken).HasMaxLength(250);
        b.Property(x => x.RefreshToken).HasMaxLength(250);
        b.Property(x => x.Scopes).HasMaxLength(250);
    
        // OneToMany AuthLogin (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.AuthLogins)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}


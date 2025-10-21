namespace LowlandTech.Accounts.Domain.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Key).HasMaxLength(250);
        b.Property(x => x.KeyHash).HasMaxLength(250);
        b.Property(x => x.KeyPrefix).HasMaxLength(250);
    
        // OneToMany ApiKey (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.ApiKeys)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}


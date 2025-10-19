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
    
        // OneToMany ApiKey (dep) → Account (principal) via FK AccountId
        b.HasOne<Account>(x => x.Account)
            .WithMany(p => p.ApiKeys)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


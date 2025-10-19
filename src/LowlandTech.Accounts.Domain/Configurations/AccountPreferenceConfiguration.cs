namespace LowlandTech.Accounts.Domain.Configurations;

public class AccountPreferenceConfiguration : IEntityTypeConfiguration<AccountPreference>
{
    public void Configure(EntityTypeBuilder<AccountPreference> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Key).HasMaxLength(250);
        b.Property(x => x.Value).HasMaxLength(250);
        b.Property(x => x.ValueType).HasMaxLength(250);
    
        // OneToMany AccountPreference (dep) → Account (principal) via FK AccountId
        b.HasOne<Account>(x => x.Account)
            .WithMany(p => p.Preferences)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


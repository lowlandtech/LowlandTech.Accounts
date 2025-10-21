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
    
        // OneToMany AccountPreference (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.Preferences)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}


namespace LowlandTech.Accounts.Domain.Configurations;

public class RecoveryCodeConfiguration : IEntityTypeConfiguration<RecoveryCode>
{
    public void Configure(EntityTypeBuilder<RecoveryCode> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Code).HasMaxLength(250);
        b.Property(x => x.CodeHash).HasMaxLength(250);
    
        // OneToMany RecoveryCode (dep) → Account (principal) via FK AccountId
        b.HasOne<Account>(x => x.Account)
            .WithMany(p => p.RecoveryCodes)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


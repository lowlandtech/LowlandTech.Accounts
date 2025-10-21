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
    
        // OneToMany RecoveryCode (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.RecoveryCodes)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}


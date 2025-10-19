namespace LowlandTech.Accounts.Domain.Configurations;

public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Token).HasMaxLength(250);
    
        // OneToMany EmailVerificationToken (dep) → Account (principal) via FK AccountId
        b.HasOne<Account>(x => x.Account)
            .WithMany(p => p.EmailTokens)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


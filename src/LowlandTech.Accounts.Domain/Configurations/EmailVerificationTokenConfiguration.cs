namespace LowlandTech.Accounts.Domain.Configurations;

public class EmailVerificationTokenConfiguration : IEntityTypeConfiguration<EmailVerificationToken>
{
    public void Configure(EntityTypeBuilder<EmailVerificationToken> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Token).HasMaxLength(250);
    
        // OneToMany EmailVerificationToken (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.EmailTokens)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}


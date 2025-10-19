namespace LowlandTech.Accounts.Domain.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Kind).HasMaxLength(250);
        b.Property(x => x.Line1).HasMaxLength(250);
        b.Property(x => x.Line2).HasMaxLength(250);
        b.Property(x => x.City).HasMaxLength(250);
        b.Property(x => x.Region).HasMaxLength(250);
        b.Property(x => x.PostalCode).HasMaxLength(250);
        b.Property(x => x.Country).HasMaxLength(250);
    
        // OneToMany Address (dep) → Account (principal) via FK AccountId
        b.HasOne<Account>(x => x.Account)
            .WithMany(p => p.Addresses)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


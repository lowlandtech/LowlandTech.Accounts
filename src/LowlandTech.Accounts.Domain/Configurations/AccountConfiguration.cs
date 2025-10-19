namespace LowlandTech.Accounts.Domain.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Email).HasMaxLength(250);
        b.Property(x => x.DisplayName).HasMaxLength(250);
        b.Property(x => x.Phone).HasMaxLength(250);
        b.Property(x => x.PhotoUrl).HasMaxLength(250);
        b.Property(x => x.Timezone).HasMaxLength(250);
        b.Property(x => x.Locale).HasMaxLength(250);
        b.Property(x => x.PreferredCurrency).HasMaxLength(250);
    }
}


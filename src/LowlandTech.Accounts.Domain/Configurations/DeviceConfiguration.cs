namespace LowlandTech.Accounts.Domain.Configurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.DeviceId).HasMaxLength(250);
        b.Property(x => x.UserAgent).HasMaxLength(250);
        b.Property(x => x.Ip).HasMaxLength(250);
    
        // OneToMany Device (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.Devices)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired();
    }
}


namespace LowlandTech.Accounts.Domain.Configurations;

public class AuditEventConfiguration : IEntityTypeConfiguration<AuditEvent>
{
    public void Configure(EntityTypeBuilder<AuditEvent> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.Kind).HasMaxLength(250);
        b.Property(x => x.DataJson).HasMaxLength(250);
        b.Property(x => x.DeviceId).HasMaxLength(250);
        b.Property(x => x.Ip).HasMaxLength(250);
    
        // OneToMany AuditEvent (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.AuditEvents)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
            b.Property(x => x.AccountId).IsRequired(false);
    }
}


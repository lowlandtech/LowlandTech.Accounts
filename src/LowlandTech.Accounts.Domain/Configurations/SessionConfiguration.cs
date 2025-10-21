namespace LowlandTech.Accounts.Domain.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.IsActive).HasDefaultValue(true);
        b.Property(x => x.DeviceId).HasMaxLength(250);
    
        // OneToMany Session (dep) → UserAccount (principal) via FK AccountId
        b.HasOne<UserAccount>(x => x.UserAccount)
            .WithMany(p => p.Sessions)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(true);
    }
}



namespace LowlandTech.Accounts.Domain;

public class AccountsContext : DbContext
{
    public AccountsContext(DbContextOptions options) : base(options) { }
    public AccountsContext() : this(new DbContextOptions<AccountsContext>()) { }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<AccountPreference> AccountPreferences => Set<AccountPreference>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
    public DbSet<AuthLogin> AuthLogins => Set<AuthLogin>();
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<EmailVerificationToken> EmailVerificationTokens => Set<EmailVerificationToken>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
    public DbSet<RecoveryCode> RecoveryCodes => Set<RecoveryCode>();
    public DbSet<Session> Sessions => Set<Session>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AccountPreferenceConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ApiKeyConfiguration());
        modelBuilder.ApplyConfiguration(new AuditEventConfiguration());
        modelBuilder.ApplyConfiguration(new AuthLoginConfiguration());
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new EmailVerificationTokenConfiguration());
        modelBuilder.ApplyConfiguration(new PasswordResetTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RecoveryCodeConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
    }
}

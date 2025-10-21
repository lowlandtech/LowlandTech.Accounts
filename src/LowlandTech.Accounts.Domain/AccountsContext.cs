
namespace LowlandTech.Accounts.Domain;

public class AccountsContext : DbContext
{
    public AccountsContext(DbContextOptions options) : base(options) { }
    public AccountsContext() : this(new DbContextOptions<AccountsContext>()) { }

    // DbSets
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
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();

    /// <summary>
    /// Configures the database context with the necessary options.
    /// </summary>
    /// <remarks>
    /// This method is called automatically by the framework to configure the <see cref="DbContext"/>.
    /// If the <paramref name="optionsBuilder"/> is already configured, no additional configuration is applied.
    /// Otherwise, the method sets up an in-memory database with a unique name per instance and enables sensitive data logging.
    /// </remarks>
    /// <param name="optionsBuilder">A <see cref="DbContextOptionsBuilder"/> instance used to configure the database context. Must not be null.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Check if a connection string is provided
        if (optionsBuilder.IsConfigured) return;

        // If no connection string is provided, use an in-memory database with a unique name per instance
        var databaseName = $"Accounts_{Guid.NewGuid()}";
        optionsBuilder.UseInMemoryDatabase(databaseName);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Apply schema if not using SQLite or in-memory
        if (!Database.IsSqlite() && !Database.IsInMemory())
        {
            modelBuilder.HasDefaultSchema(AccountsConfig.Context.Schema);
        }

        // Apply entity configurations
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
        modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
    }
}

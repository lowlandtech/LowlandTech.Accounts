
namespace LowlandTech.Accounts.Backend;

[PluginId("cab4c162-986f-579e-aa20-ee4b713c4db5")]
public sealed class AccountsBackendPlugin : Plugin
{
    public override void Install(IServiceCollection services)
    {
        // --- EF Core (factory; provider is configured by host AddProvider<TContext>)
        services.AddProvider<AccountsContext>(prefix: "LowlandTech.Accounts.Domain");

        // --- MediatR (scan this Backend assembly)
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AccountsBackendPlugin).Assembly));

        // --- HttpClient + Polly v8 Resilience
        services.AddHttpClient("default")
            .AddStandardResilienceHandler(options =>
            {
                options.Retry.MaxRetryAttempts = 5;
                options.Retry.UseJitter = true;
                options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(30);
                options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(10);
                options.AttemptTimeout.Timeout = TimeSpan.FromSeconds(10);
            });

        // --- HealthChecks (include Db)
        services.AddHealthChecks().AddDbContextCheck<AccountsContext>("db");

        // --- ProblemDetails
        services.AddProblemDetails();

        services.AddAuthorization(options =>
        {
            // Generated policies per table
            options.AddPolicy("accountpreference::read",    policy => policy.Requirements.Add(new PermissionRequirement("accountpreference::read")));
            options.AddPolicy("accountpreference::create",  policy => policy.Requirements.Add(new PermissionRequirement("accountpreference::create")));
            options.AddPolicy("accountpreference::update",  policy => policy.Requirements.Add(new PermissionRequirement("accountpreference::update")));
            options.AddPolicy("accountpreference::delete",  policy => policy.Requirements.Add(new PermissionRequirement("accountpreference::delete")));
            options.AddPolicy("address::read",    policy => policy.Requirements.Add(new PermissionRequirement("address::read")));
            options.AddPolicy("address::create",  policy => policy.Requirements.Add(new PermissionRequirement("address::create")));
            options.AddPolicy("address::update",  policy => policy.Requirements.Add(new PermissionRequirement("address::update")));
            options.AddPolicy("address::delete",  policy => policy.Requirements.Add(new PermissionRequirement("address::delete")));
            options.AddPolicy("apikey::read",    policy => policy.Requirements.Add(new PermissionRequirement("apikey::read")));
            options.AddPolicy("apikey::create",  policy => policy.Requirements.Add(new PermissionRequirement("apikey::create")));
            options.AddPolicy("apikey::update",  policy => policy.Requirements.Add(new PermissionRequirement("apikey::update")));
            options.AddPolicy("apikey::delete",  policy => policy.Requirements.Add(new PermissionRequirement("apikey::delete")));
            options.AddPolicy("auditevent::read",    policy => policy.Requirements.Add(new PermissionRequirement("auditevent::read")));
            options.AddPolicy("auditevent::create",  policy => policy.Requirements.Add(new PermissionRequirement("auditevent::create")));
            options.AddPolicy("auditevent::update",  policy => policy.Requirements.Add(new PermissionRequirement("auditevent::update")));
            options.AddPolicy("auditevent::delete",  policy => policy.Requirements.Add(new PermissionRequirement("auditevent::delete")));
            options.AddPolicy("authlogin::read",    policy => policy.Requirements.Add(new PermissionRequirement("authlogin::read")));
            options.AddPolicy("authlogin::create",  policy => policy.Requirements.Add(new PermissionRequirement("authlogin::create")));
            options.AddPolicy("authlogin::update",  policy => policy.Requirements.Add(new PermissionRequirement("authlogin::update")));
            options.AddPolicy("authlogin::delete",  policy => policy.Requirements.Add(new PermissionRequirement("authlogin::delete")));
            options.AddPolicy("device::read",    policy => policy.Requirements.Add(new PermissionRequirement("device::read")));
            options.AddPolicy("device::create",  policy => policy.Requirements.Add(new PermissionRequirement("device::create")));
            options.AddPolicy("device::update",  policy => policy.Requirements.Add(new PermissionRequirement("device::update")));
            options.AddPolicy("device::delete",  policy => policy.Requirements.Add(new PermissionRequirement("device::delete")));
            options.AddPolicy("emailverificationtoken::read",    policy => policy.Requirements.Add(new PermissionRequirement("emailverificationtoken::read")));
            options.AddPolicy("emailverificationtoken::create",  policy => policy.Requirements.Add(new PermissionRequirement("emailverificationtoken::create")));
            options.AddPolicy("emailverificationtoken::update",  policy => policy.Requirements.Add(new PermissionRequirement("emailverificationtoken::update")));
            options.AddPolicy("emailverificationtoken::delete",  policy => policy.Requirements.Add(new PermissionRequirement("emailverificationtoken::delete")));
            options.AddPolicy("passwordresettoken::read",    policy => policy.Requirements.Add(new PermissionRequirement("passwordresettoken::read")));
            options.AddPolicy("passwordresettoken::create",  policy => policy.Requirements.Add(new PermissionRequirement("passwordresettoken::create")));
            options.AddPolicy("passwordresettoken::update",  policy => policy.Requirements.Add(new PermissionRequirement("passwordresettoken::update")));
            options.AddPolicy("passwordresettoken::delete",  policy => policy.Requirements.Add(new PermissionRequirement("passwordresettoken::delete")));
            options.AddPolicy("recoverycode::read",    policy => policy.Requirements.Add(new PermissionRequirement("recoverycode::read")));
            options.AddPolicy("recoverycode::create",  policy => policy.Requirements.Add(new PermissionRequirement("recoverycode::create")));
            options.AddPolicy("recoverycode::update",  policy => policy.Requirements.Add(new PermissionRequirement("recoverycode::update")));
            options.AddPolicy("recoverycode::delete",  policy => policy.Requirements.Add(new PermissionRequirement("recoverycode::delete")));
            options.AddPolicy("session::read",    policy => policy.Requirements.Add(new PermissionRequirement("session::read")));
            options.AddPolicy("session::create",  policy => policy.Requirements.Add(new PermissionRequirement("session::create")));
            options.AddPolicy("session::update",  policy => policy.Requirements.Add(new PermissionRequirement("session::update")));
            options.AddPolicy("session::delete",  policy => policy.Requirements.Add(new PermissionRequirement("session::delete")));
            options.AddPolicy("useraccount::read",    policy => policy.Requirements.Add(new PermissionRequirement("useraccount::read")));
            options.AddPolicy("useraccount::create",  policy => policy.Requirements.Add(new PermissionRequirement("useraccount::create")));
            options.AddPolicy("useraccount::update",  policy => policy.Requirements.Add(new PermissionRequirement("useraccount::update")));
            options.AddPolicy("useraccount::delete",  policy => policy.Requirements.Add(new PermissionRequirement("useraccount::delete")));
        });

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        // --- Observability (OpenTelemetry) — exporters configured by host
        services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService(serviceName: "LowlandTech.Accounts.Backend"))
            .WithTracing(t => t
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation())
            .WithMetrics(m => m
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation());

    }

    public override async Task Configure(IServiceProvider container, object? host = null)
    {
        // Apply migrations
        await container.UseMigration<AccountsContext>();

        // Seed workspace (optional) if you keep a UseCase for this workspace
         var scope = container.CreateAsyncScope();
        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<AccountsContext>>();
        var db = await factory.CreateDbContextAsync();
        await db.SeedAsync<SeedAccountsDataUseCase>(); // comment out if you don’t have a seeder
    }
}

// ===== Permission model =====
public sealed class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string policy) => Policy = policy;
    public string Policy { get; }
}

public sealed class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        // Expect claims named "perm" with values like "customer::read"
        foreach (var c in context.User.FindAll("perm"))
        {
            if (string.Equals(c.Value, requirement.Policy, StringComparison.OrdinalIgnoreCase))
            {
                context.Succeed(requirement);
                break;
            }
        }
        return Task.CompletedTask;
    }
}


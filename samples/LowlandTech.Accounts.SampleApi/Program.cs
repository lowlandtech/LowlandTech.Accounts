namespace LowlandTech.Accounts.SampleApi;

public partial class Program
{
    public static int Main(string[] args)
    {
        var app = BuildApp(args);
        app.Run();
        return 0;
    }

    public static WebApplication BuildApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Manually call the plugin's Install method to register services
        // This ensures services are registered even when Lamar isn't fully initialized
        var plugin = new LowlandTech.Accounts.Backend.AccountsBackendPlugin();
        plugin.Install(builder.Services);

        // Add rate limiting (required by controllers)
        builder.Services.AddRateLimiter(options =>
        {
            options.AddPolicy("strict", context =>
                System.Threading.RateLimiting.RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: context.Request.Headers.Host.ToString(),
                    factory: _ => new System.Threading.RateLimiting.FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 100,
                        Window = TimeSpan.FromMinutes(1),
                        QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));
        });

        // Add services to the container
        builder.Services.AddControllers()
            .AddApplicationPart(typeof(LowlandTech.Accounts.Backend.AccountsBackendPlugin).Assembly)
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // OpenAPI & Scalar documentation
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        // CORS configuration
        const string corsPolicy = "AllowAll";
        builder.Services.AddCors(p => p.AddPolicy(corsPolicy, policy =>
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        // Configure test authentication that grants all permissions (must be AFTER plugin.Install)
        builder.Services.AddAuthentication("TestScheme")
            .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, TestAuthenticationHandler>("TestScheme", options => { });

        var app = builder.Build();

        // Health check endpoint
        app.MapGet("/healthz", () => Results.Ok(new { status = "ok" }));

        // Sample weather forecast endpoint
        app.MapGet("/weatherforecast", () =>
        {
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecast = Enumerable.Range(1, 5).Select(index => new
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            });

            return Results.Ok(forecast);
        });

        // OpenAPI documentation endpoints
        app.MapOpenApi();
        app.MapScalarApiReference();
        app.MapScalarApiReference("/", options => options.WithTitle("Sample API"));

        // Configure pipeline
        app.UseRouting();
        app.UseCors(corsPolicy);
        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();

        // Map controllers
        app.MapControllers();

        return app;
    }
}

// Make Program accessible to integration tests
public partial class Program { }

// Test authentication handler that auto-grants all permissions for development/testing
public class TestAuthenticationHandler : Microsoft.AspNetCore.Authentication.AuthenticationHandler<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(
        Microsoft.Extensions.Options.IOptionsMonitor<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions> options,
        Microsoft.Extensions.Logging.ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<Microsoft.AspNetCore.Authentication.AuthenticateResult> HandleAuthenticateAsync()
    {
        // Create claims that grant all permissions required by the controllers
        var claims = new[]
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "TestUser"),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, "test-user-id"),
            // Grant all CRUD permissions for all entities
            new System.Security.Claims.Claim("perm", "accountpreference::read"),
            new System.Security.Claims.Claim("perm", "accountpreference::create"),
            new System.Security.Claims.Claim("perm", "accountpreference::update"),
            new System.Security.Claims.Claim("perm", "accountpreference::delete"),
            new System.Security.Claims.Claim("perm", "address::read"),
            new System.Security.Claims.Claim("perm", "address::create"),
            new System.Security.Claims.Claim("perm", "address::update"),
            new System.Security.Claims.Claim("perm", "address::delete"),
            new System.Security.Claims.Claim("perm", "apikey::read"),
            new System.Security.Claims.Claim("perm", "apikey::create"),
            new System.Security.Claims.Claim("perm", "apikey::update"),
            new System.Security.Claims.Claim("perm", "apikey::delete"),
            new System.Security.Claims.Claim("perm", "auditevent::read"),
            new System.Security.Claims.Claim("perm", "auditevent::create"),
            new System.Security.Claims.Claim("perm", "auditevent::update"),
            new System.Security.Claims.Claim("perm", "auditevent::delete"),
            new System.Security.Claims.Claim("perm", "authlogin::read"),
            new System.Security.Claims.Claim("perm", "authlogin::create"),
            new System.Security.Claims.Claim("perm", "authlogin::update"),
            new System.Security.Claims.Claim("perm", "authlogin::delete"),
            new System.Security.Claims.Claim("perm", "device::read"),
            new System.Security.Claims.Claim("perm", "device::create"),
            new System.Security.Claims.Claim("perm", "device::update"),
            new System.Security.Claims.Claim("perm", "device::delete"),
            new System.Security.Claims.Claim("perm", "emailverificationtoken::read"),
            new System.Security.Claims.Claim("perm", "emailverificationtoken::create"),
            new System.Security.Claims.Claim("perm", "emailverificationtoken::update"),
            new System.Security.Claims.Claim("perm", "emailverificationtoken::delete"),
            new System.Security.Claims.Claim("perm", "passwordresettoken::read"),
            new System.Security.Claims.Claim("perm", "passwordresettoken::create"),
            new System.Security.Claims.Claim("perm", "passwordresettoken::update"),
            new System.Security.Claims.Claim("perm", "passwordresettoken::delete"),
            new System.Security.Claims.Claim("perm", "recoverycode::read"),
            new System.Security.Claims.Claim("perm", "recoverycode::create"),
            new System.Security.Claims.Claim("perm", "recoverycode::update"),
            new System.Security.Claims.Claim("perm", "recoverycode::delete"),
            new System.Security.Claims.Claim("perm", "session::read"),
            new System.Security.Claims.Claim("perm", "session::create"),
            new System.Security.Claims.Claim("perm", "session::update"),
            new System.Security.Claims.Claim("perm", "session::delete"),
            new System.Security.Claims.Claim("perm", "useraccount::read"),
            new System.Security.Claims.Claim("perm", "useraccount::create"),
            new System.Security.Claims.Claim("perm", "useraccount::update"),
            new System.Security.Claims.Claim("perm", "useraccount::delete"),
        };

        var identity = new System.Security.Claims.ClaimsIdentity(claims, "TestScheme");
        var principal = new System.Security.Claims.ClaimsPrincipal(identity);
        var ticket = new Microsoft.AspNetCore.Authentication.AuthenticationTicket(principal, "TestScheme");

        return Task.FromResult(Microsoft.AspNetCore.Authentication.AuthenticateResult.Success(ticket));
    }
}

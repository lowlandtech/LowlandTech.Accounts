
namespace LowlandTech.Accounts.Tests.Fakes;

public sealed class AppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class, new()
{
    public AppFactory(AccountsContext? db = null)
    {
        Environment.SetEnvironmentVariable("DOTNET_USE_POLLING_FILE_WATCHER", "1");
        Environment.SetEnvironmentVariable("DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE", "false");

        var workspaceRoot = ResolveWorkspaceRoot();

        // Determine which sample project to use based on TProgram type
        // Check for App first (special case), default to Api
        var isApp = typeof(TProgram).Name.Contains("App", StringComparison.OrdinalIgnoreCase);
        var projectName = isApp
            ? "lowlandtech.accounts.SampleApp"
            : "lowlandtech.accounts.SampleApi";

        _contentRoot = System.IO.Path.Combine(workspaceRoot, "samples", projectName);

        Db = db ?? new AccountsContext();
        Db.Database.EnsureCreated();
    }

    private static string ResolveWorkspaceRoot()
    {
        var current = AppContext.BaseDirectory;

        while (!string.IsNullOrEmpty(current))
        {
            var candidate = new DirectoryInfo(current);
            var samplesFolder = System.IO.Path.Combine(candidate.FullName, "samples");
            var solution = Directory.EnumerateFiles(candidate.FullName, "*.sln", SearchOption.TopDirectoryOnly).FirstOrDefault();

            if (Directory.Exists(samplesFolder) && solution is not null)
            {
                return candidate.FullName;
            }

            current = candidate.Parent?.FullName;
        }

        var baseRoot = System.OperatingSystem.IsWindows() ? "C:\\Workspaces\\Generated" : "/home/workspaces/generated";
        return System.IO.Path.Combine(baseRoot, "lowlandtech.accounts");
    }

    private IHost? _host;
    private string _contentRoot;
    public AccountsContext Db { get; set; }

    private Action<IServiceCollection>? _extraServices;
    public void ConfigureTestServices(Action<IServiceCollection> configure)
        => _extraServices = configure;

    public Uri ServerAddress
    {
        get
        {
            EnsureServer();
            return ClientOptions.BaseAddress;
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseContentRoot(_contentRoot)
            .UseWebRoot("wwwroot")
            .UseStaticWebAssets()
            .UseKestrel()
            .PreferHostingUrls(true)
            .UseUrls("http://127.0.0.1:0")
            .UseEnvironment("Development");

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextFactory<AccountsContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddScoped<IDbContextFactory<AccountsContext>>(_ => new DbContextFactory<AccountsContext>(Db));
        });

        if (_extraServices is not null)
        {
            builder.ConfigureServices(_extraServices);
        }
    }

    private void EnsureServer()
    {
        if (_host is null)
        {
            using var _ = CreateDefaultClient();
        }
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var testHost = builder.UseEnvironment("Development").Build();
        testHost.Start();

        builder.ConfigureWebHost(webHost =>
        {
            webHost
                .UseKestrel(options => options.Listen(IPAddress.Loopback, 0))
                .PreferHostingUrls(true)
                .UseUrls("http://127.0.0.1:0")
                .UseEnvironment("Development");
        });

        _host = builder.Build();
        _host.Start();

        var server = _host.Services.GetRequiredService<IServer>();
        var feature = server.Features.Get<IServerAddressesFeature>();

        Uri? bound = null;
        var deadline = DateTime.UtcNow.AddSeconds(5);
        while (DateTime.UtcNow < deadline && bound is null)
        {
            var candidate = feature?.Addresses?.Select(a => new Uri(a)).FirstOrDefault(u => u.IsAbsoluteUri && u.Port != 0);
            if (candidate is not null)
            {
                bound = candidate;
                break;
            }
            Thread.Sleep(50);
        }

        if (bound is null)
        {
            try
            {
                using var http = new HttpClient { BaseAddress = new Uri("http://127.0.0.1/") };
                _ = http.GetAsync("/");
            }
            catch { }
            Thread.Sleep(100);
            bound = feature?.Addresses?.Select(a => new Uri(a)).FirstOrDefault(u => u.IsAbsoluteUri && u.Port != 0);
        }

        if (bound is null)
            throw new InvalidOperationException("Kestrel did not publish a bound address within the timeout.");

        var root = new Uri(bound.GetLeftPart(UriPartial.Authority) + "/");
        ClientOptions.BaseAddress = root;

        return testHost;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing) return;
        try { _host?.Dispose(); }
        catch { }
        _host = null;
    }
}

namespace LowlandTech.Samples.Api;

public sealed class Program
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

        // Add services to the container
        builder.Services.AddControllers()
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

        // Map controllers
        app.MapControllers();

        return app;
    }
}

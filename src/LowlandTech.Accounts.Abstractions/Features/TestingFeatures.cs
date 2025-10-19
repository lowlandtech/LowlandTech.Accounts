
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Bunit;

namespace LowlandTech.Accounts.Abstractions.Features;

/// <summary>
/// Represents an attribute used to annotate a class with a behavior-driven development (BDD) scenario,
/// specifying the scenario code, title, and Given/When/Then steps.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ScenarioAttribute : Attribute
{
    public ScenarioAttribute(string given, string? when = null, string? then = null)
    {
        if (string.IsNullOrWhiteSpace(given))
            throw new ArgumentException("Given step cannot be null or empty.", nameof(given));
        Given = given;
        When = when;
        Then = then;
    }

    public ScenarioAttribute(string specId, string title, string given, string? when = null, string? then = null)
    {
        if (string.IsNullOrWhiteSpace(specId))
            throw new ArgumentException("Scenario code cannot be null or empty.", nameof(specId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Scenario title cannot be null or empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(given))
            throw new ArgumentException("Given step cannot be null or empty.", nameof(given));
        SpecId = specId;
        Title = title;
        Given = given;
        When = when;
        Then = then;
    }

    public string? SpecId { get; }
    public string? Title { get; }
    public string? Given { get; }
    public string? When { get; }
    public string? Then { get; }

    public override string ToString()
    {
        if (!string.IsNullOrWhiteSpace(SpecId) && !string.IsNullOrWhiteSpace(Title))
            return $"{SpecId}: {Title}";
        return Given ?? string.Empty;
    }
}

/// <summary>
/// Specifies that a method represents a 'Then' step in a behavior-driven development (BDD) scenario.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class ThenAttribute : Attribute, ITraitAttribute
{
    public ThenAttribute(string description, string? code = null)
    {
        Description = description;
        Code = code;
    }

    public string Description { get; }
    public string? Code { get; set; }
}

/// <summary>
/// Provides a base class for test scenarios that involve a database context.
/// </summary>
public abstract class WhenUsingDatabase<TContext> where TContext : DbContext
{
    protected TContext Db = null!;

    protected virtual TContext CreateContext() => Activator.CreateInstance<TContext>();

    protected abstract Task GivenAsync();
    protected abstract Task WhenAsync();

    protected WhenUsingDatabase()
    {
        Setup().GetAwaiter().GetResult();
    }

    private async Task Setup()
    {
        Db = CreateContext();
        await Db.Database.EnsureDeletedAsync();
        await Db.Database.EnsureCreatedAsync();
        await GivenAsync();
        await WhenAsync();
    }
}

/// <summary>
/// Provides a base class for defining test scenarios with a 'Given-When-Then' structure.
/// </summary>
public abstract class WhenTestingFor<T>
{
    protected T Sut { get; set; } = default!;

    protected abstract T For();
    protected virtual void Given() { }
    protected virtual void When() { }

    protected WhenTestingFor()
    {
        Setup();
    }

    private void Setup()
    {
        Sut = For();
        Given();
        When();
    }
}

/// <summary>
/// Provides a base class for testing asynchronous operations with a specified state.
/// </summary>
public abstract class WhenTestingForAsync<TState>
{
    protected TState Sut { get; set; } = default!;

    protected abstract TState For();
    protected virtual Task GivenAsync() => Task.CompletedTask;
    protected virtual Task WhenAsync() => Task.CompletedTask;

    protected WhenTestingForAsync()
    {
        Sut = For();
        GivenAsync().GetAwaiter().GetResult();
        WhenAsync().GetAwaiter().GetResult();
    }
}

/// <summary>
/// Provides a base class for testing Blazor components with a 'Given-When-Then' structure.
/// </summary>
public abstract class WhenTestingComponent<T> : TestContext
{
    protected T Cut { get; set; } = default!;

    protected abstract T For();
    protected virtual void Given() { }
    protected virtual Task When() => Task.CompletedTask;

    protected WhenTestingComponent()
    {
        Setup();
    }

    private void Setup()
    {
        Cut = For();
        Given();
        When();
    }
}

/// <summary>
/// Async-only G/W/T harness for bUnit/xUnit. xUnit awaits InitializeAsync before any [Fact].
/// </summary>
public abstract class WhenTestingComponentAsync<T> : TestContext, IAsyncLifetime
{
    public WhenTestingComponentAsync()
    {
        try { JSInterop.Mode = JSRuntimeMode.Loose; } catch { }
    }

    protected T Cut { get; private set; } = default!;
    protected virtual CancellationToken TestCancellation => CancellationToken.None;

    protected abstract Task<T> ForAsync(CancellationToken ct);
    protected virtual Task GivenAsync(CancellationToken ct) => Task.CompletedTask;
    protected virtual Task WhenAsync(CancellationToken ct) => Task.CompletedTask;

    public async Task InitializeAsync()
    {
        var ct = TestCancellation;
        Cut = await ForAsync(ct).ConfigureAwait(false);
        await GivenAsync(ct).ConfigureAwait(false);
        await WhenAsync(ct).ConfigureAwait(false);
    }

    public virtual Task DisposeAsync() => Task.CompletedTask;
}

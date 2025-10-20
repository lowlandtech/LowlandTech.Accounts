
// Global using directives

global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Http;
global using System.Net.Http.Json;
global using System.Threading;
global using System.Threading.Tasks;

// xUnit
global using Xunit;
global using Xunit.Abstractions;

// Shouldly assertions
global using Shouldly;

// ASP.NET Core Testing
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Hosting.Server;
global using Microsoft.AspNetCore.Hosting.Server.Features;
global using Microsoft.AspNetCore.Mvc.Testing;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

// Entity Framework
global using Microsoft.EntityFrameworkCore;

// bUnit for Blazor component testing
global using Bunit;
global using Bunit.TestDoubles;

// Playwright for E2E testing
global using Microsoft.Playwright;

// LowlandTech.Abstractions for use cases
global using LowlandTech.Abstractions.UseCases;

// Test infrastructure from Abstractions
global using LowlandTech.Accounts.Abstractions.Features;

// Domain entities and DTOs
global using LowlandTech.Accounts.Domain;
global using LowlandTech.Accounts.Domain.Entities;
global using LowlandTech.Accounts.Domain.Extensions;

// Sample API for integration testing
global using LowlandTech.Accounts.SampleApi;

// Test fakes, helpers, and use cases
global using LowlandTech.Accounts.Tests.Fakes;
global using LowlandTech.Accounts.Tests.UseCases;

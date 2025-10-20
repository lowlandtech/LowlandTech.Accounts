
// === BCL ===
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;

// === ASP.NET Core ===
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.RateLimiting;
global using Microsoft.AspNetCore.Diagnostics;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;

// === MediatR ===
global using MediatR;

// === Lamar ===
global using Lamar;

// === EF Core ===
global using Microsoft.EntityFrameworkCore;

// === OpenTelemetry ===
global using OpenTelemetry.Metrics;
global using OpenTelemetry.Resources;
global using OpenTelemetry.Trace;

// === Misc ===
global using Polly;
global using Polly.Extensions.Http;

// === Diagnostics / Attributes ===
global using System.ComponentModel; // DescriptionAttribute etc.

// === This plugin’s contracts & domain ===
global using LowlandTech.Abstractions.Extensions;
global using LowlandTech.Abstractions.UseCases;
global using LowlandTech.Abstractions.Plugins;
global using LowlandTech.Abstractions.Plugins.Attributes;
global using LowlandTech.Accounts.Abstractions;
global using LowlandTech.Accounts.Abstractions.AccountPreferences;
global using LowlandTech.Accounts.Abstractions.Addresses;
global using LowlandTech.Accounts.Abstractions.ApiKeys;
global using LowlandTech.Accounts.Abstractions.AuditEvents;
global using LowlandTech.Accounts.Abstractions.AuthLogins;
global using LowlandTech.Accounts.Abstractions.Devices;
global using LowlandTech.Accounts.Abstractions.EmailVerificationTokens;
global using LowlandTech.Accounts.Abstractions.PasswordResetTokens;
global using LowlandTech.Accounts.Abstractions.RecoveryCodes;
global using LowlandTech.Accounts.Abstractions.Sessions;
global using LowlandTech.Accounts.Abstractions.UserAccounts;
global using LowlandTech.Accounts.Backend.AccountPreferences;
global using LowlandTech.Accounts.Backend.Addresses;
global using LowlandTech.Accounts.Backend.ApiKeys;
global using LowlandTech.Accounts.Backend.AuditEvents;
global using LowlandTech.Accounts.Backend.AuthLogins;
global using LowlandTech.Accounts.Backend.Devices;
global using LowlandTech.Accounts.Backend.EmailVerificationTokens;
global using LowlandTech.Accounts.Backend.PasswordResetTokens;
global using LowlandTech.Accounts.Backend.RecoveryCodes;
global using LowlandTech.Accounts.Backend.Sessions;
global using LowlandTech.Accounts.Backend.UserAccounts;
global using LowlandTech.Accounts.Backend.UseCases;
global using LowlandTech.Accounts.Abstractions.Enums;
global using LowlandTech.Accounts.Domain;
global using LowlandTech.Accounts.Domain.Entities;



// === BCL ===
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using System.ComponentModel.DataAnnotations;
global using System.Net.Http.Json;
global using System.Text.Json;
global using System.Text.Json.Serialization;

// === Misc. ===
global using MediatR;
global using Lamar;
global using MudBlazor;

// === Diagnostics / Attributes ===
global using System.ComponentModel; // DescriptionAttribute etc.

// === Microsoft libraries ===
global using Microsoft.Extensions.Logging;

// === This plugin’s contracts & domain ===
global using LowlandTech.Accounts.Abstractions;
global using LowlandTech.Accounts.Abstractions.Enums;
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


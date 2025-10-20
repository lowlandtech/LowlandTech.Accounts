namespace LowlandTech.Accounts.Domain.Extensions;

/// <summary>
/// Extension methods for AccountsContext providing Upsert functionality.
/// </summary>
public static class AccountsContextExtensions
{

    /// <summary>
    /// Upserts a list of AccountPreferences into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<AccountPreference> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.AccountPreferences.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.AccountPreferences.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Key = entity.Key,
                    Value = entity.Value,
                    ValueType = entity.ValueType,
                });
            }
            else
            {
                var e = db.AccountPreferences.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Key = entity.Key;
                   e.Value = entity.Value;
                   e.ValueType = entity.ValueType;
                db.AccountPreferences.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of Addresses into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<Address> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.Addresses.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.Addresses.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Kind = entity.Kind,
                    Line1 = entity.Line1,
                    Line2 = entity.Line2,
                    City = entity.City,
                    Region = entity.Region,
                    PostalCode = entity.PostalCode,
                    Country = entity.Country,
                    IsPrimary = entity.IsPrimary,
                });
            }
            else
            {
                var e = db.Addresses.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Kind = entity.Kind;
                   e.Line1 = entity.Line1;
                   e.Line2 = entity.Line2;
                   e.City = entity.City;
                   e.Region = entity.Region;
                   e.PostalCode = entity.PostalCode;
                   e.Country = entity.Country;
                   e.IsPrimary = entity.IsPrimary;
                db.Addresses.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of ApiKeys into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<ApiKey> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.ApiKeys.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.ApiKeys.Add(new()
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    CreatedUtc = entity.CreatedUtc,
                    IsActive = entity.IsActive,
                    Key = entity.Key,
                    KeyHash = entity.KeyHash,
                    KeyPrefix = entity.KeyPrefix,
                    LastUsedUtc = entity.LastUsedUtc,
                    Name = entity.Name,
                    RevokedUtc = entity.RevokedUtc,
                });
            }
            else
            {
                var e = db.ApiKeys.Single(e => e.Id == entity.Id);
                   e.AccountId = entity.AccountId;
                   e.CreatedUtc = entity.CreatedUtc;
                   e.IsActive = entity.IsActive;
                   e.Key = entity.Key;
                   e.KeyHash = entity.KeyHash;
                   e.KeyPrefix = entity.KeyPrefix;
                   e.LastUsedUtc = entity.LastUsedUtc;
                   e.Name = entity.Name;
                   e.RevokedUtc = entity.RevokedUtc;
                db.ApiKeys.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of AuditEvents into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<AuditEvent> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.AuditEvents.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.AuditEvents.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Kind = entity.Kind,
                    DataJson = entity.DataJson,
                    DeviceId = entity.DeviceId,
                    Ip = entity.Ip,
                    CreatedUtc = entity.CreatedUtc,
                });
            }
            else
            {
                var e = db.AuditEvents.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Kind = entity.Kind;
                   e.DataJson = entity.DataJson;
                   e.DeviceId = entity.DeviceId;
                   e.Ip = entity.Ip;
                   e.CreatedUtc = entity.CreatedUtc;
                db.AuditEvents.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of AuthLogins into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<AuthLogin> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.AuthLogins.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.AuthLogins.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Provider = entity.Provider,
                    ProviderUserId = entity.ProviderUserId,
                    AccessToken = entity.AccessToken,
                    RefreshToken = entity.RefreshToken,
                    ExpiresUtc = entity.ExpiresUtc,
                    Scopes = entity.Scopes,
                });
            }
            else
            {
                var e = db.AuthLogins.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Provider = entity.Provider;
                   e.ProviderUserId = entity.ProviderUserId;
                   e.AccessToken = entity.AccessToken;
                   e.RefreshToken = entity.RefreshToken;
                   e.ExpiresUtc = entity.ExpiresUtc;
                   e.Scopes = entity.Scopes;
                db.AuthLogins.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of Devices into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<Device> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.Devices.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.Devices.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    DeviceId = entity.DeviceId,
                    UserAgent = entity.UserAgent,
                    Ip = entity.Ip,
                    FirstSeenUtc = entity.FirstSeenUtc,
                    LastSeenUtc = entity.LastSeenUtc,
                    IsTrusted = entity.IsTrusted,
                });
            }
            else
            {
                var e = db.Devices.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.DeviceId = entity.DeviceId;
                   e.UserAgent = entity.UserAgent;
                   e.Ip = entity.Ip;
                   e.FirstSeenUtc = entity.FirstSeenUtc;
                   e.LastSeenUtc = entity.LastSeenUtc;
                   e.IsTrusted = entity.IsTrusted;
                db.Devices.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of EmailVerificationTokens into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<EmailVerificationToken> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.EmailVerificationTokens.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.EmailVerificationTokens.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Token = entity.Token,
                    ExpiresUtc = entity.ExpiresUtc,
                    UsedUtc = entity.UsedUtc,
                });
            }
            else
            {
                var e = db.EmailVerificationTokens.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Token = entity.Token;
                   e.ExpiresUtc = entity.ExpiresUtc;
                   e.UsedUtc = entity.UsedUtc;
                db.EmailVerificationTokens.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of PasswordResetTokens into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<PasswordResetToken> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.PasswordResetTokens.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.PasswordResetTokens.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    Token = entity.Token,
                    ExpiresUtc = entity.ExpiresUtc,
                    UsedUtc = entity.UsedUtc,
                });
            }
            else
            {
                var e = db.PasswordResetTokens.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.Token = entity.Token;
                   e.ExpiresUtc = entity.ExpiresUtc;
                   e.UsedUtc = entity.UsedUtc;
                db.PasswordResetTokens.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of RecoveryCodes into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<RecoveryCode> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.RecoveryCodes.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.RecoveryCodes.Add(new()
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    Code = entity.Code,
                    CodeHash = entity.CodeHash,
                    IsActive = entity.IsActive,
                    Name = entity.Name,
                    UsedUtc = entity.UsedUtc,
                });
            }
            else
            {
                var e = db.RecoveryCodes.Single(e => e.Id == entity.Id);
                   e.AccountId = entity.AccountId;
                   e.Code = entity.Code;
                   e.CodeHash = entity.CodeHash;
                   e.IsActive = entity.IsActive;
                   e.Name = entity.Name;
                   e.UsedUtc = entity.UsedUtc;
                db.RecoveryCodes.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of Sessions into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<Session> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.Sessions.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.Sessions.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    AccountId = entity.AccountId,
                    DeviceId = entity.DeviceId,
                    CreatedUtc = entity.CreatedUtc,
                    ExpiresUtc = entity.ExpiresUtc,
                    RevokedUtc = entity.RevokedUtc,
                });
            }
            else
            {
                var e = db.Sessions.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.AccountId = entity.AccountId;
                   e.DeviceId = entity.DeviceId;
                   e.CreatedUtc = entity.CreatedUtc;
                   e.ExpiresUtc = entity.ExpiresUtc;
                   e.RevokedUtc = entity.RevokedUtc;
                db.Sessions.Update(e);
            }
        }

        return db;
    }

    
    /// <summary>
    /// Upserts a list of UserAccounts into the database.
    /// </summary>
    public static AccountsContext Upsert(this AccountsContext db, List<UserAccount> entities)
    {
        foreach (var entity in entities)
        {
            var exists = db.UserAccounts.Any(e => e.Id == entity.Id);
            if (!exists)
            {
                db.UserAccounts.Add(new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    IsActive = entity.IsActive,
                    Email = entity.Email,
                    DisplayName = entity.DisplayName,
                    Phone = entity.Phone,
                    PhotoUrl = entity.PhotoUrl,
                    Timezone = entity.Timezone,
                    Locale = entity.Locale,
                    PreferredCurrency = entity.PreferredCurrency,
                    TwoFactorEnabled = entity.TwoFactorEnabled,
                    CreatedUtc = entity.CreatedUtc,
                    LastLoginUtc = entity.LastLoginUtc,
                });
            }
            else
            {
                var e = db.UserAccounts.Single(e => e.Id == entity.Id);
                   e.Name = entity.Name;
                   e.IsActive = entity.IsActive;
                   e.Email = entity.Email;
                   e.DisplayName = entity.DisplayName;
                   e.Phone = entity.Phone;
                   e.PhotoUrl = entity.PhotoUrl;
                   e.Timezone = entity.Timezone;
                   e.Locale = entity.Locale;
                   e.PreferredCurrency = entity.PreferredCurrency;
                   e.TwoFactorEnabled = entity.TwoFactorEnabled;
                   e.CreatedUtc = entity.CreatedUtc;
                   e.LastLoginUtc = entity.LastLoginUtc;
                db.UserAccounts.Update(e);
            }
        }

        return db;
    }

    }

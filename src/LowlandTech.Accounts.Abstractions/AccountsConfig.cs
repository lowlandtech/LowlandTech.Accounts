
namespace LowlandTech.Accounts.Abstractions;

/// <summary>
/// Contains the plugin constants for the Accounts plugin.
/// </summary>
public static class AccountsConfig
{
    /// <summary>
    /// Gets the workspace plugin identifier.
    /// </summary>
    public const string PluginId = "cab4c162-986f-579e-aa20-ee4b713c4db5";

    /// <summary>
    /// Provides constants and identifiers related to the Accounts frontend plugin.
    /// </summary>
    public static class Frontend
    {
        /// <summary>
        /// Gets the plugin identifier for the Accounts frontend plugin.
        /// </summary>
        public const string PluginId = "17fffd82-3388-5849-86ef-ed14eb894d3d";

        /// <summary>
        /// Component identifiers for the Accounts frontend.
        /// </summary>
        public static class Components
        {
            public static Guid Dashboard = new("734179b4-0d1b-5a96-8e2c-15a105b24a29");
        }
    }

    /// <summary>
    /// Provides constants and identifiers related to the Accounts backend plugin.
    /// </summary>
    public static class Backend
    {
        /// <summary>
        /// Gets the plugin identifier for the Accounts backend plugin.
        /// </summary>
        public const string PluginId = "70897293-152b-55f3-a8fb-b73cc097566c";
    }

    /// <summary>
    /// Provides constants that represent metadata and configuration for the Accounts domain context.
    /// </summary>
    /// <remarks>
    /// This class contains immutable values that can be used to identify and configure the domain
    /// context. These constants are intended for use in scenarios where consistent and predefined values are required,
    /// such as database schema definitions or unique context identification.
    /// </remarks>
    public static class Context
    {
        /// <summary>
        /// Represents a unique identifier for the Accounts domain context.
        /// </summary>
        public const string Id = "4083a4cc-33fd-5c02-9bbc-fef7e965ba89";

        /// <summary>
        /// Represents the default schema name used in the Accounts database.
        /// </summary>
        public const string Schema = "Accounts";
    }
}

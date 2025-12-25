namespace TwistedTaleweaver.Core.Database;

/// <summary>
/// Provides methods for accessing the database
/// </summary>
public interface IDatabaseAccessor
{
    /// <summary>
    /// Queries the database for a single record or returns the default value if none is found
    /// </summary>
    Task<T?> QuerySingleOrDefaultAsync<T>(string sql, object? param = null);

    /// <summary>
    /// Queries the database for a single record
    /// </summary>    
    Task<T> QuerySingleAsync<T>(string sql, object? param = null);

    /// <summary>
    /// Queries the database for multiple records
    /// </summary>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);

    /// <summary>
    /// Executes a command against the database without output
    /// </summary>
    Task ExecuteAsync(string sql, object? param = null);

    /// <summary>
    /// Executes a command against the database and returns a single record
    /// </summary>    
    Task<T> ExecuteWithOutputAsync<T>(string sql, object? param = null);
}
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;
using Npgsql;
using TwistedTaleweaver.DataAccess.Common;
using TwistedTaleweaver.DataAccess.Common.Extensions;
using TwistedTaleweaver.DataAccess.Expeditions.Entities;

namespace TwistedTaleweaver.DataAccess.Expeditions.Repositories;

public interface IExpeditionOutcomeRepository : IRepository
{
    /// <summary>
    /// Adds a new expedition outcome.
    /// </summary>
    Task AddAsync(ExpeditionOutcome outcome, NpgsqlTransaction? transaction = null);
}

internal class ExpeditionOutcomeRepository(IDbConnectionFactory connectionFactory) : IExpeditionOutcomeRepository
{
    public async Task AddAsync(ExpeditionOutcome outcome, NpgsqlTransaction? transaction = null)
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        
        await connectionFactory.ExecuteAsync(async (connection, tx) =>
        {
            const string sql = @"
                INSERT INTO expedition_outcomes 
                (expedition_id, result_id, narrations, encounters)
                VALUES (@ExpeditionId, @ResultId, @Narrations::json, @Encounters::jsonb)";

            await connection.ExecuteAsync(sql, new
            {
                ExpeditionId = outcome.ExpeditionId,
                ResultId = (byte)outcome.Result,
                Narrations = JsonSerializer.Serialize(outcome.Narrations),
                Encounters = JsonSerializer.Serialize(outcome.Encounters, options)
            }, tx);
        }, transaction);
    }
}
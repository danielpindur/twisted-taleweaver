using System.Data;
using System.Text.Json;
using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace TwistedTaleweaver.DataAccess.Common.TypeHandlers;

internal class JsonTypeHandler<TEntity> : SqlMapper.TypeHandler<TEntity>
{
    public override void SetValue(IDbDataParameter parameter, TEntity? value)
    {
        if (parameter is NpgsqlParameter npgsqlParam)
        {
            npgsqlParam.NpgsqlDbType = NpgsqlDbType.Jsonb;
            npgsqlParam.Value = value == null ? DBNull.Value : JsonSerializer.Serialize(value, SerializationConfiguration.SerializationOptions);
        }
        else
        {
            parameter.DbType = DbType.String;
            parameter.Value = value == null ? DBNull.Value : JsonSerializer.Serialize(value, SerializationConfiguration.SerializationOptions);
        }
    }

    public override TEntity? Parse(object value)
    {
        return value switch
        {
            null or DBNull => default,
            string json when !string.IsNullOrWhiteSpace(json) => JsonSerializer.Deserialize<TEntity>(json, SerializationConfiguration.SerializationOptions),
            string => default, // Empty or whitespace string
            _ => throw new InvalidCastException($"Cannot convert {value.GetType().Name} to {typeof(TEntity).Name}")
        };
    }
}
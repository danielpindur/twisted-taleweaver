using System.Data;
using System.Text.Json;
using Dapper;

namespace TwistedTaleweaver.DataAccess.Common.TypeHandlers;

internal class JsonDocumentTypeHandler : SqlMapper.TypeHandler<JsonDocument>
{
    public override void SetValue(IDbDataParameter parameter, JsonDocument? value)
    {
        parameter.Value = value?.RootElement.GetRawText() ?? (object)DBNull.Value;
    }

    public override JsonDocument? Parse(object? value)
    {
        if (value is null || value == DBNull.Value)
        {
            return null;
        }
        
        return JsonDocument.Parse(value.ToString()!);
    }
}
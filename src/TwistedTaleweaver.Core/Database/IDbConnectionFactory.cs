using System.Data.Common;

namespace TwistedTaleweaver.Core.Database;

public interface IDbConnectionFactory
{
    DbConnection CreateConnection();
}

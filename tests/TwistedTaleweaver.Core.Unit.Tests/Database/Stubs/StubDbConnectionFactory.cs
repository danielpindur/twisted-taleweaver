using System.Data.Common;
using TwistedTaleweaver.Core.Database;

namespace TwistedTaleweaver.Core.Unit.Tests.Database.Stubs;

public class StubDbConnectionFactory : IDbConnectionFactory
{
    public DbConnection CreateConnection()
    {
        return new StubDbConnection();
    }
}
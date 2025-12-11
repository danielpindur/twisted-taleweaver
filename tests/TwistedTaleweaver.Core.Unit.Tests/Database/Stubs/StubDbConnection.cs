using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace TwistedTaleweaver.Core.Unit.Tests.Database.Stubs;

public class StubDbConnection : DbConnection
{
    public bool ConnectionOpen { get; private set; }
    
    public override void Open()
    {
        ConnectionOpen = true;
    }

    [AllowNull] public override string ConnectionString { get; set; }
    public override string Database { get; }
    public override ConnectionState State { get; }
    public override string DataSource { get; }
    public override string ServerVersion { get; }

    public override void Close()
    {
        ConnectionOpen = false;
    }

    protected override DbCommand CreateDbCommand()
    {
        throw new NotImplementedException();
    }

    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    {
        return new StubDbTransaction();
    }

    public override void ChangeDatabase(string databaseName)
    {
        throw new NotImplementedException();
    }

    protected override void Dispose(bool disposing)
    {
        Close();
        base.Dispose(disposing);
    }
}
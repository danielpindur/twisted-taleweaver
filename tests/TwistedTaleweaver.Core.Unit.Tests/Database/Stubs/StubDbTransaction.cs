using System.Data;
using System.Data.Common;

namespace TwistedTaleweaver.Core.Unit.Tests.Database.Stubs;

public class StubDbTransaction : DbTransaction
{
    public bool TransactionCommited { get; private set; }
    
    public bool TransactionRolledBack { get; private set; }
    
    public override void Commit()
    {
        TransactionCommited = true;
    }

    public override void Rollback()
    {
        TransactionRolledBack = true;
    }

    protected override DbConnection? DbConnection { get; }
    public override IsolationLevel IsolationLevel { get; }
}
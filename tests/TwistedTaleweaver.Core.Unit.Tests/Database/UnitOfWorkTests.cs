using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.Enums;
using TwistedTaleweaver.Core.Unit.Tests.Database.Stubs;

namespace TwistedTaleweaver.Core.Unit.Tests.Database;

[TestFixture]
public class UnitOfWorkTests
{
    private StubDbConnectionFactory _factory;

    [SetUp]
    public void SetUp()
    {
        _factory = new StubDbConnectionFactory();
    }

    // ---------------------------------------------------------
    // Construction
    // ---------------------------------------------------------

    [Test]
    public void Constructor_OpensConnection()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var connection = (StubDbConnection)uow.Connection;

        Assert.That(connection.ConnectionOpen, Is.True, "Connection should be opened in constructor.");
    }

    // ---------------------------------------------------------
    // Read-only scenarios
    // ---------------------------------------------------------

    [Test]
    public void GetTransaction_ReadOnly_SuppressScope_ReturnsNull()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Suppress);

        var tx = uow.GetTransaction(ConnectionMode.ReadOnly);

        Assert.That(tx, Is.Null, "Suppress scope must return null for read-only transaction.");
    }

    [Test]
    public void GetTransaction_ReadOnly_RequiredScope_ReturnsNull_NoTransactionCreated()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var tx = uow.GetTransaction(ConnectionMode.ReadOnly);

        Assert.That(tx, Is.Null);
    }

    // ---------------------------------------------------------
    // Write scenarios
    // ---------------------------------------------------------

    [Test]
    public void GetTransaction_ReadWrite_FirstRequest_CreatesTransaction()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var tx = uow.GetTransaction(ConnectionMode.ReadWrite);

        Assert.That(tx, Is.Not.Null, "Write request should create a transaction.");
    }

    [Test]
    public void GetTransaction_ReadWrite_SecondRequest_ReturnsSameTransaction()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var first = uow.GetTransaction(ConnectionMode.ReadWrite);
        var second = uow.GetTransaction(ConnectionMode.ReadWrite);

        Assert.That(first, Is.SameAs(second), "Second write request must reuse the same transaction.");
    }

    [Test]
    public void GetTransaction_ReadWrite_SuppressScope_ReturnsNull()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Suppress);

        var tx = uow.GetTransaction(ConnectionMode.ReadWrite);

        Assert.That(tx, Is.Null, "Write request inside Suppress must return null.");
    }

    // ---------------------------------------------------------
    // Mixed Escalation: Read → Write
    // ---------------------------------------------------------

    [Test]
    public void GetTransaction_ReadThenWrite_EscalatesToTransaction()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var readTx = uow.GetTransaction(ConnectionMode.ReadOnly);
        Assert.That(readTx, Is.Null, "Read-only should not create a transaction.");

        var writeTx = uow.GetTransaction(ConnectionMode.ReadWrite);
        Assert.That(writeTx, Is.Not.Null, "Write request must escalate and create a transaction.");
    }

    // ---------------------------------------------------------
    // Mixed: Write → Read
    // ---------------------------------------------------------

    [Test]
    public void GetTransaction_WriteThenRead_ReturnsSameTransaction()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var writeTx = uow.GetTransaction(ConnectionMode.ReadWrite);
        var readTx = uow.GetTransaction(ConnectionMode.ReadOnly);

        Assert.That(writeTx, Is.SameAs(readTx), "Reads after writes must return active transaction.");
    }

    // ---------------------------------------------------------
    // Commit tests
    // ---------------------------------------------------------

    [Test]
    public async Task CommitAsync_ActiveTransaction_CommitsAndDisposesTransaction()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var tx = (StubDbTransaction)uow.GetTransaction(ConnectionMode.ReadWrite)!;

        await uow.CommitAsync();

        Assert.That(tx.TransactionCommited, Is.True, "Transaction commit must be called.");
    }

    [Test]
    public void CommitAsync_NoTransaction_Throws()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.CommitAsync();
        });
    }

    // ---------------------------------------------------------
    // Rollback tests
    // ---------------------------------------------------------

    [Test]
    public async Task RollbackAsync_ActiveTransaction_RollsBackAndDisposes()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        var tx = (StubDbTransaction)uow.GetTransaction(ConnectionMode.ReadWrite)!;

        await uow.RollbackAsync();

        Assert.That(tx.TransactionRolledBack, Is.True, "Rollback must be invoked.");
    }

    [Test]
    public void RollbackAsync_NoTransaction_Throws()
    {
        var uow = new UnitOfWork(
            _factory,
            ConnectionMode.ReadOnly,
            UnitOfWorkScopeOption.Required);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.RollbackAsync();
        });
    }

    // ---------------------------------------------------------
    // Dispose tests
    // ---------------------------------------------------------

    [Test]
    public async Task DisposeAsync_NoActiveTransaction_DisposesConnection()
    {
        var uow = new UnitOfWork(
            _factory, ConnectionMode.ReadOnly, UnitOfWorkScopeOption.Required);

        var connection = (StubDbConnection)uow.Connection;

        await uow.DisposeAsync();

        Assert.That(connection.ConnectionOpen, Is.False, "Connection should be closed on dispose.");
    }

    [Test]
    public void DisposeAsync_ActiveTransaction_Throws()
    {
        var uow = new UnitOfWork(
            _factory, ConnectionMode.ReadOnly, UnitOfWorkScopeOption.Required);

        uow.GetTransaction(ConnectionMode.ReadWrite);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.DisposeAsync();
        });
    }
}

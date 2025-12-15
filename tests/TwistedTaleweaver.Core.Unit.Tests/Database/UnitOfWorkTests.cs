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

    #region Construction
    
    [Test]
    public void Constructor_OpensConnection()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var connection = (StubDbConnection)uow.Connection;

        Assert.That(connection.ConnectionOpen, Is.True);
    }
    
    #endregion

    #region UnitOfWorkScopeOption.Suppress scenarios

    [Test]
    public void GetTransaction_ReadOnly_SuppressScope_ReturnsNull()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Suppressed);

        var tx = uow.GetTransaction(TransactionMode.ReadOnly);

        Assert.That(tx, Is.Null);
    }
    
    [Test]
    public void GetTransaction_ReadWrite_SuppressScope_ReturnsNull()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Suppressed);

        var tx = uow.GetTransaction(TransactionMode.ReadWrite);

        Assert.That(tx, Is.Null);
    }

    #endregion

    #region UnitOfWorkScopeOption.Required scenarios

    [Test]
    public void GetTransaction_FirstRequest_ReadOnly_DoesNotCreatesTransaction()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var tx = uow.GetTransaction(TransactionMode.ReadOnly);

        Assert.That(tx, Is.Null);
    }
    
    [Test]
    public void GetTransaction_FirstRequest_ReadWrite_CreatesTransaction()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var tx = uow.GetTransaction(TransactionMode.ReadWrite);

        Assert.That(tx, Is.Not.Null);
    }

    [Test]
    public void GetTransaction_SecondRequest_ReadOnly_ReturnsSameTransaction()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var first = uow.GetTransaction(TransactionMode.ReadWrite);
        var second = uow.GetTransaction(TransactionMode.ReadOnly);

        Assert.That(first, Is.SameAs(second));
    }
    
    [Test]
    public void GetTransaction_SecondRequest_ReadWrite_ReturnsSameTransaction()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var first = uow.GetTransaction(TransactionMode.ReadWrite);
        var second = uow.GetTransaction(TransactionMode.ReadWrite);

        Assert.That(first, Is.SameAs(second));
    }

    #endregion

    #region Commit tests

    [Test]
    public async Task CommitAsync_ActiveTransaction_CommitsAndDisposesTransaction()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var tx = (StubDbTransaction)uow.GetTransaction(TransactionMode.ReadWrite)!;

        await uow.CommitAsync();

        Assert.That(tx.TransactionCommited, Is.True);
    }
    
    [Test]
    public async Task CommitAsync_NoTransaction_DoesNotThrow()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);
        await uow.CommitAsync();
    }

    [Test]
    public void CommitAsync_Suppressed_Throws()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Suppressed);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.CommitAsync();
        });
    }

    #endregion

    #region Rollback tests

    [Test]
    public async Task RollbackAsync_ActiveTransaction_RollsBackAndDisposes()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var tx = (StubDbTransaction)uow.GetTransaction(TransactionMode.ReadWrite)!;

        await uow.RollbackAsync();

        Assert.That(tx.TransactionRolledBack, Is.True);
    }
    
    [Test]
    public async Task RollbackAsync_NoTransaction_DoesNotThrow()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);
        await uow.RollbackAsync();
    }

    [Test]
    public void RollbackAsync_Suppressed_Throws()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Suppressed);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.RollbackAsync();
        });
    }

    #endregion

    #region Dispose tests

    [Test]
    public async Task DisposeAsync_NoActiveTransaction_DisposesConnection()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        var connection = (StubDbConnection)uow.Connection;

        await uow.DisposeAsync();

        Assert.That(connection.ConnectionOpen, Is.False);
    }

    [Test]
    public void DisposeAsync_ActiveTransaction_Throws()
    {
        var uow = new UnitOfWork(_factory, UnitOfWorkScopeOption.Required);

        uow.GetTransaction(TransactionMode.ReadWrite);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await uow.DisposeAsync();
        });
    }

    #endregion
}

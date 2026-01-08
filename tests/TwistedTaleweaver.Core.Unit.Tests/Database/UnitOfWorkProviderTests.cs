using TwistedTaleweaver.Core.Database;
using TwistedTaleweaver.Core.Database.Enums;
using TwistedTaleweaver.Core.Unit.Tests.Database.Stubs;

namespace TwistedTaleweaver.Core.Unit.Tests.Database;

[TestFixture]
public class UnitOfWorkProviderTests
{
    private StubDbConnectionFactory _connectionFactory = null!;

    [SetUp]
    public void SetUp()
    {
        _connectionFactory = new StubDbConnectionFactory();
    }

    private UnitOfWorkProvider CreateProvider()
    {
        return new UnitOfWorkProvider(_connectionFactory);
    }

    /// <summary>
    /// No UnitOfWorkScope was created, but DatabaseAccessor requested UnitOfWork
    /// </summary>
    [Test]
    public void GetActiveUnitOfWork_Without_UnitOfWork_Returns_Null()
    {
        var provider = CreateProvider();
        var unitOfWork = provider.GetActiveUnitOfWork();

        Assert.That(unitOfWork, Is.Null);
    }

    /// <summary>
    /// No UnitOfWorkScope was created, but DatabaseAccessor requested OperationUnitOfWork
    /// </summary>
    [Test]
    public void CreateOperationUnitOfWork_Creates_Suppressed_Not_Tracked_UnitOfWork()
    {
        var provider = CreateProvider();

        var unitOfWork = provider.CreateOperationUnitOfWork();
        
        Assert.That(unitOfWork.Connection, Is.Not.Null);
        Assert.That(unitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork, Is.Null);
    }

    /// <summary>
    /// New Suppress UnitOfWorkScope created with no previous UnitOfWorks
    /// </summary>
    [Test]
    public void CreateUnitOfWork_Suppressed_Creates_UnitOfWork()
    {
        var provider = CreateProvider();
        var createdUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);

        Assert.That(createdUnitOfWork.Connection, Is.Not.Null);
        Assert.That(createdUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var unitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(unitOfWork, Is.SameAs(createdUnitOfWork));
    }

    /// <summary>
    /// New Required UnitOfWorkScope created with no previous UnitOfWorks
    /// </summary>
    [Test]
    public void CreateUnitOfWork_Required_Creates_UnitOfWork()
    {
        var provider = CreateProvider();
        var createdUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        Assert.That(createdUnitOfWork.Connection, Is.Not.Null);
        Assert.That(createdUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));

        var unitOfWork = provider.GetActiveUnitOfWork();

        Assert.That(unitOfWork, Is.SameAs(createdUnitOfWork));
    }

    /// <summary>
    /// Suppressed UnitOfWorkScope ReadOnly has no transaction
    /// </summary>
    [Test]
    public void CreateUnitOfWork_Suppressed_ReadOnly_Transaction_Logic()
    {
        var provider = CreateProvider();
        var createdUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);

        Assert.That(createdUnitOfWork.Connection, Is.Not.Null);
        Assert.That(createdUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var unitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(unitOfWork, Is.SameAs(createdUnitOfWork));

        var transaction = unitOfWork.GetTransaction(TransactionMode.ReadOnly);
        Assert.That(transaction, Is.Null);
    }

    /// <summary>
    /// Suppressed UnitOfWorkScope ReadWrite has no transaction
    /// </summary>
    [Test]
    public void CreateUnitOfWork_Suppressed_ReadWrite_Transaction_Logic()
    {
        var provider = CreateProvider();
        var createdUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);

        Assert.That(createdUnitOfWork.Connection, Is.Not.Null);
        Assert.That(createdUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var unitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(unitOfWork, Is.SameAs(createdUnitOfWork));

        var transaction = unitOfWork.GetTransaction(TransactionMode.ReadWrite);
        Assert.That(transaction, Is.Null);
    }

    /// <summary>
    /// Required UnitOfWorkScope creates transaction and reuses it
    /// </summary>
    [Test]
    public void CreateUnitOfWork_Required_Transaction_Logic()
    {
        var provider = CreateProvider();
        var createdUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        Assert.That(createdUnitOfWork.Connection, Is.Not.Null);
        Assert.That(createdUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));

        var unitOfWork = provider.GetActiveUnitOfWork();

        Assert.That(unitOfWork, Is.SameAs(createdUnitOfWork));

        var createdTransaction = unitOfWork.GetTransaction(TransactionMode.ReadWrite);
        Assert.That(createdTransaction, Is.Not.Null);

        var transaction = unitOfWork.GetTransaction(TransactionMode.ReadWrite);
        Assert.That(transaction, Is.Not.Null);
        Assert.That(transaction, Is.SameAs(createdTransaction));

        transaction = unitOfWork.GetTransaction(TransactionMode.ReadOnly);
        Assert.That(transaction, Is.Not.Null);
        Assert.That(transaction, Is.SameAs(createdTransaction));
    }

    /// <summary>
    /// If a suppressed UnitOfWork is requested when one already exists on the top of the stack, the same instance is returned
    /// </summary>
    [Test]
    public void Returns_Same_Suppressed_UnitOfWork_If_New_Suppressed_Is_Requested()
    {
        var provider = CreateProvider();

        var firstUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        Assert.That(firstUnitOfWork.Connection, Is.Not.Null);
        Assert.That(firstUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var secondUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        Assert.That(secondUnitOfWork.Connection, Is.Not.Null);
        Assert.That(secondUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        Assert.That(secondUnitOfWork, Is.SameAs(firstUnitOfWork));

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(secondUnitOfWork));
    }

    /// <summary>
    /// If a required UnitOfWork is requested when one already exists on the top of the stack, the same instance is returned
    /// </summary>
    [Test]
    public void Returns_Same_Required_UnitOfWork_If_New_Required_Is_Requested()
    {
        var provider = CreateProvider();

        var firstUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        Assert.That(firstUnitOfWork.Connection, Is.Not.Null);
        Assert.That(firstUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));

        var secondUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        Assert.That(secondUnitOfWork.Connection, Is.Not.Null);
        Assert.That(secondUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));

        Assert.That(secondUnitOfWork, Is.SameAs(firstUnitOfWork));

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(activeUnitOfWork, Is.SameAs(secondUnitOfWork));
    }

    /// <summary>
    /// We have accessed the database, but now want to create a Required scope to handle insert/update operations
    /// </summary>
    [Test]
    public void Returns_New_Required_UnitOfWork_If_Current_Scope_Is_Suppressed()
    {
        var provider = CreateProvider();

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        Assert.That(suppressedUnitOfWork.Connection, Is.Not.Null);
        Assert.That(suppressedUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));

        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        Assert.That(requiredUnitOfWork.Connection, Is.Not.Null);
        Assert.That(requiredUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(requiredUnitOfWork, Is.Not.SameAs(suppressedUnitOfWork));
        Assert.That(requiredUnitOfWork.Connection, Is.Not.SameAs(suppressedUnitOfWork.Connection));

        var newActiveUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(newActiveUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(newActiveUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(newActiveUnitOfWork, Is.SameAs(requiredUnitOfWork));
    }

    /// <summary>
    /// We have an active Required scope, but now want to write to the database outside of that transaction
    /// </summary>
    [Test]
    public void Returns_New_Suppressed_UnitOfWork_If_Current_Scope_Is_Required()
    {
        var provider = CreateProvider();

        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        Assert.That(requiredUnitOfWork.Connection, Is.Not.Null);
        Assert.That(requiredUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(activeUnitOfWork, Is.SameAs(requiredUnitOfWork));

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        Assert.That(suppressedUnitOfWork.Connection, Is.Not.Null);
        Assert.That(suppressedUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(suppressedUnitOfWork, Is.Not.SameAs(requiredUnitOfWork));
        Assert.That(suppressedUnitOfWork.Connection, Is.Not.SameAs(requiredUnitOfWork.Connection));

        var newActiveUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(newActiveUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(newActiveUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(newActiveUnitOfWork, Is.SameAs(suppressedUnitOfWork));
    }

    /// <summary>
    /// Disposes upper Required scope after disconnect with commit
    /// </summary>
    [Test]
    public async Task Disposes_Upper_Required_Scope_After_Disconnect_With_Commit()
    {
        var provider = CreateProvider();

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // Should just skip the commit if we don't have a transaction
        await provider.CommitUnitOfWorkAsync();
        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));
    }

    /// <summary>
    /// Disposes upper Required scope after disconnect with commit and transaction
    /// </summary>
    [Test]
    public async Task Disposes_Upper_Required_Scope_After_Disconnect_With_Commit_And_Transaction()
    {
        var provider = CreateProvider();

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        requiredUnitOfWork.GetTransaction(TransactionMode.ReadWrite);

        await provider.CommitUnitOfWorkAsync();
        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));
    }

    /// <summary>
    /// Disposes upper Required scope after disconnect without commit
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task Disposes_Upper_Required_Scope_After_Disconnect_Without_Commit()
    {
        var provider = CreateProvider();

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));
    }

    /// <summary>
    /// Throws on dispose upper Required scope after disconnect with transaction without commit
    /// </summary>
    [Test]
    public void Throws_On_Dispose_Upper_Required_Scope_After_Disconnect_With_Transaction()
    {
        var provider = CreateProvider();

        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        requiredUnitOfWork.GetTransaction(TransactionMode.ReadWrite);

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await provider.CloseUnitOfWorkAsync();
        });
    }

    /// <summary>
    /// Disposes upper Required scope after disconnect with multiple connects
    /// </summary>
    [Test]
    public async Task Disposes_Upper_Required_Scope_After_Disconnect_With_Multiple_Connects()
    {
        var provider = CreateProvider();

        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(activeUnitOfWork, Is.SameAs(requiredUnitOfWork));

        await provider.CloseUnitOfWorkAsync();

        activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));
    }

    /// <summary>
    /// Disposes upper Suppressed scope after disconnect
    /// </summary>
    [Test]
    public async Task Disposes_Upper_Suppressed_Scope_After_Disconnect()
    {
        var provider = CreateProvider();

        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);

        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(activeUnitOfWork, Is.SameAs(requiredUnitOfWork));
    }

    /// <summary>
    /// Disposes upper Suppressed scope after disconnect with multiple connects
    /// </summary>
    [Test]
    public async Task Disposes_Upper_Suppressed_Scope_After_Disconnect_With_Multiple_Connects()
    {
        var provider = CreateProvider();

        var requiredUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        var suppressedUnitOfWork = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);

        await provider.CloseUnitOfWorkAsync();

        var activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Suppressed));
        Assert.That(activeUnitOfWork, Is.SameAs(suppressedUnitOfWork));

        await provider.CloseUnitOfWorkAsync();

        activeUnitOfWork = provider.GetActiveUnitOfWork();
        Assert.That(activeUnitOfWork!.Connection, Is.Not.Null);
        Assert.That(activeUnitOfWork.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required));
        Assert.That(activeUnitOfWork, Is.SameAs(requiredUnitOfWork));
    }

    /// <summary>
    /// When there is no active UnitOfWork, we cannot be in an active transaction
    /// </summary>
    [Test]
    public void IsInActiveTransaction_ReturnsFalse_WhenNoUnitOfWork()
    {
        var provider = CreateProvider();
        Assert.That(provider.IsInActiveTransaction, Is.False);
    }

    /// <summary>
    /// When the active UnitOfWork is Suppressed, we are not in an active transaction
    /// </summary>
    [Test]
    public void IsInActiveTransaction_ReturnsFalse_WhenSuppressedUnitOfWork()
    {
        var provider = CreateProvider();
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppressed);
        Assert.That(provider.IsInActiveTransaction, Is.False);
    }

    /// <summary>
    /// When the active UnitOfWork is Required, we are in an active transaction
    /// </summary>
    [Test]
    public void IsInActiveTransaction_ReturnsTrue_WhenRequiredUnitOfWork()
    {
        var provider = CreateProvider();
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        Assert.That(provider.IsInActiveTransaction, Is.True);
    }
}

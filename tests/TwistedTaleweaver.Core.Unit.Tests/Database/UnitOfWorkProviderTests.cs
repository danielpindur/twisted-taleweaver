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

    /*
    // ---------------------------------------------------------
    // TC-UOWP-002 — Required inside existing Required increments connection counter
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_002_RequiredInsideRequired_IncrementsConnectionCounter()
    {
        var provider = CreateProvider();
        var first = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        // After creating first, connections count should be 1
        var topEntry = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntry), Is.EqualTo(1));

        var second = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // Should have reused same entry (stack size still 1) and incremented count
        var stack = GetStack(provider);
        Assert.That(stack.Count, Is.EqualTo(1));
        var topEntryAfter = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntryAfter), Is.EqualTo(2));
        var topUow = GetUnitOfWorkFromEntry(topEntryAfter);
        Assert.That(second, Is.SameAs(topUow));
    }

    // ---------------------------------------------------------
    // TC-UOWP-003 — Suppress under Required pushes new entry
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_003_SuppressUnderRequired_PushesNewEntry()
    {
        var provider = CreateProvider();
        var required = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        var suppressed = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        var stack = GetStack(provider);
        Assert.That(stack.Count, Is.EqualTo(2), "A new entry should be pushed for Suppress under Required.");

        // The returned UoW should be the stack top and should have scope Suppress
        var topEntry = GetTopEntry(provider);
        var topUow = GetUnitOfWorkFromEntry(topEntry);
        Assert.That(suppressed, Is.SameAs(topUow));
        Assert.That(GetScopeOption(topUow), Is.EqualTo(UnitOfWorkScopeOption.Suppress));
    }

    // ---------------------------------------------------------
    // TC-UOWP-004 — Required under Suppress throws
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_004_RequiredUnderSuppress_Throws()
    {
        var provider = CreateProvider();
        // create suppress as top
        var suppress = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        Assert.Throws<InvalidOperationException>(() =>
        {
            provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        }, "Creating Required within active Suppress must throw InvalidOperationException.");
    }

    // ---------------------------------------------------------
    // TC-UOWP-005 — Suppress under Suppress reuses the same entry
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_005_SuppressUnderSuppress_ReusesEntryAndIncrementsCount()
    {
        var provider = CreateProvider();
        var first = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        var beforeEntry = GetTopEntry(provider);
        var beforeCount = GetConnectionsCount(beforeEntry);
        Assert.That(beforeCount, Is.EqualTo(1));

        var second = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        // Should reuse same entry (stack size 1) and increment count
        var stack = GetStack(provider);
        Assert.That(stack.Count, Is.EqualTo(1));
        var afterEntry = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(afterEntry), Is.EqualTo(2));
        var topUow = GetUnitOfWorkFromEntry(afterEntry);
        Assert.That(second, Is.SameAs(topUow));
    }

    // ---------------------------------------------------------
    // TC-UOWP-010 — GetActiveUnitOfWork pops and returns top entry
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_010_GetActiveUnitOfWork_PopsAndReturnsTop()
    {
        var provider = CreateProvider();
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        var stackBefore = GetStack(provider);
        Assert.That(stackBefore.Count, Is.EqualTo(1));

        var active = provider.GetActiveUnitOfWork();

        var stackAfter = GetStack(provider);
        Assert.That(stackAfter.Count, Is.EqualTo(0), "GetActiveUnitOfWork pops the top entry.");
        Assert.That(active, Is.Not.Null);
    }

    // ---------------------------------------------------------
    // TC-UOWP-011 — GetActiveUnitOfWork on empty stack creates default
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_011_GetActiveUnitOfWork_OnEmpty_CreatesDefault()
    {
        var provider = CreateProvider();

        // ensure stack empty: call GetActiveUnitOfWork to trigger creation
        var active = provider.GetActiveUnitOfWork();

        Assert.That(active, Is.Not.Null);
        Assert.That(active.ScopeOption, Is.EqualTo(UnitOfWorkScopeOption.Required),
            "Default scope must be Required.");
        // After this call, stack should contain the created default entry (count 1)
        var stack = GetStack(provider);
        Assert.That(stack.Count, Is.EqualTo(1));
    }

    // ---------------------------------------------------------
    // TC-UOWP-020 — Close decrements connection count
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_020_Close_DecrementsConnectionCount()
    {
        var provider = CreateProvider();
        // create required and then second required to increment count
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required); // increments connection count to 2

        var topEntryBefore = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntryBefore), Is.EqualTo(2));

        await provider.CloseUnitOfWorkAsync();

        var topEntryAfter = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntryAfter), Is.EqualTo(1), "Close should decrement the connection count but not dispose while >0.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_021 — Close disposing last handle when count reaches zero
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_021_Close_DisposesWhenCountReachesZero()
    {
        var provider = CreateProvider();
        var uow = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // find the top entry and its UnitOfWork and StubDbConnection
        var topEntry = GetTopEntry(provider);
        var topUow = GetUnitOfWorkFromEntry(topEntry);
        var connection = (StubDbConnection)topUow.Connection;

        // ensure connection open initially
        Assert.That(connection.ConnectionOpen, Is.True, "Connection should be opened by UnitOfWork constructor.");

        // Close -> should call Disconnect and because count becomes 0, provider will call DisposeAsync() on the popped entry's UoW.
        await provider.CloseUnitOfWorkAsync();

        // After close, the original UnitOfWork should have been disposed; connection closed.
        Assert.That(connection.ConnectionOpen, Is.False, "DisposeAsync should close the connection of the popped UnitOfWork.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_030 — Commit decrements count and commits when last reference closed
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_030_Commit_DecrementsAndCommitsWhenLastReference()
    {
        var provider = CreateProvider();
        var uow = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // create a transaction on the active UoW
        var tx = (StubDbTransaction)uow.GetTransaction(ConnectionMode.ReadWrite)!;

        // Commit; since _connectionsCount == 1, CommitUnitOfWorkAsync should call CommitAsync on the popped entry
        await provider.CommitUnitOfWorkAsync();

        Assert.That(tx.TransactionCommited, Is.True, "CommitAsync must be called on the UnitOfWork's transaction when last reference closes.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_031 — Commit does not commit suppressed scope
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_031_Commit_DoesNotCommitWhenSuppressScope()
    {
        var provider = CreateProvider();
        var suppressed = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        // create a transaction on the suppressed UoW (note: suppressed UoW's GetTransaction returns null in UnitOfWork -> no transaction)
        // So to assert no commit occurs, we'll ensure CommitUnitOfWorkAsync does not throw and does not flip any StubDbTransaction flag.
        // Because suppressed UoW returns null for transactions we cannot get a StubDbTransaction; simply call Commit to ensure no exceptions and stack changes.
        await provider.CommitUnitOfWorkAsync();

        // After commit, suppressed entry should have its connection count decremented (to 0) and provider will not call CommitAsync.
        var topEntry = GetTopEntry(provider);
        // topEntry here is now the default pushed by GetActiveUnitOfWork inside provider flow; we just ensure no exceptions were thrown.
        Assert.Pass("CommitUnitOfWorkAsync returned without calling CommitAsync for suppressed scope (no exception).");
    }

    // ---------------------------------------------------------
    // TC_UOWP_032 — Commit with multiple nested references does not commit until last
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_032_Commit_DoesNotCommitUntilLastReference()
    {
        var provider = CreateProvider();
        var uow1 = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        var uow2 = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required); // increments count to 2

        // Create transaction on the active unit (shared)
        var tx = (StubDbTransaction)uow2.GetTransaction(ConnectionMode.ReadWrite)!;

        // First commit call: should only decrement count to 1 and not commit
        await provider.CommitUnitOfWorkAsync();

        var topEntry = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntry), Is.EqualTo(1), "First commit should only decrement connections count.");
        Assert.That(tx.TransactionCommited, Is.False, "Transaction should not be committed until last reference is closed.");

        // Second commit call should commit
        await provider.CommitUnitOfWorkAsync();
        Assert.That(tx.TransactionCommited, Is.True, "Transaction must be committed on final commit when count reached zero.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_040 — Rollback when last reference closes
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_040_Rollback_InvokedWhenLastReferenceCloses()
    {
        var provider = CreateProvider();
        var uow = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        var tx = (StubDbTransaction)uow.GetTransaction(ConnectionMode.ReadWrite)!;

        await provider.RollbackUnitOfWorkAsync();

        Assert.That(tx.TransactionRolledBack, Is.True, "Rollback must be invoked when last reference closes.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_041 — Rollback does not execute for suppressed scope
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_041_Rollback_DoesNotExecuteForSuppressScope()
    {
        var provider = CreateProvider();
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        // Calling rollback should not throw and should not attempt rollback on a suppressed scope
        await provider.RollbackUnitOfWorkAsync();

        Assert.Pass("RollbackUnitOfWorkAsync completed without invoking rollback for suppressed scope.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_050 — Nested suppressed UoW unwinds correctly
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_050_NestedSuppress_UnwindsCorrectly()
    {
        var provider = CreateProvider();

        // Step 1: Required -> push
        var required = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // Step 2: Suppress -> push
        var suppress = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        // Step 3: Close suppressed
        await provider.CloseUnitOfWorkAsync();

        // After closing suppressed, the suppressed entry should have been disposed (its connection closed)
        // The stack should still contain the required entry as top
        var stack = GetStack(provider);
        Assert.That(stack.Count, Is.EqualTo(1), "After unwinding suppressed, stack should contain the required entry.");

        // Step 4: Close required
        await provider.CloseUnitOfWorkAsync();

        // After closing required, stack should contain the default entry pushed by GetActiveUnitOfWork
        Assert.That(stack.Count, Is.EqualTo(1), "After closing required, provider ensures a default entry is present.");
    }

    // ---------------------------------------------------------
    // TC_UOWP_051 — Nested required UoW shares underlying UoW until count reaches zero
    // ---------------------------------------------------------
    [Test]
    public async Task TC_UOWP_051_NestedRequired_SharesUnderlyingUoWUntilZero()
    {
        var provider = CreateProvider();

        // Required (conn count = 1)
        var r1 = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // Required (conn count = 2)
        var r2 = provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);

        // The two returned references should be the same underlying UoW instance
        Assert.That(r1, Is.SameAs(r2));

        // Create transaction
        var tx = (StubDbTransaction)r2.GetTransaction(ConnectionMode.ReadWrite)!;

        // Close one (decrement to 1)
        await provider.CloseUnitOfWorkAsync();
        var topEntry = GetTopEntry(provider);
        Assert.That(GetConnectionsCount(topEntry), Is.EqualTo(1));

        // Close second (decrement to 0 -> dispose)
        await provider.CloseUnitOfWorkAsync();

        // Transaction should have been disposed via DisposeAsync on popped entry; connection closed
        Assert.That(tx.TransactionCommited || tx.TransactionRolledBack || !((StubDbConnection)r2.Connection).ConnectionOpen,
            Is.True, "After final close the underlying UoW should be finalized (disposed or closed connection).");
    }

    // ---------------------------------------------------------
    // TC_UOWP_060 — Creating transactional UoW inside suppressed scope throws
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_060_SuppressThenRequired_Throws()
    {
        var provider = CreateProvider();
        provider.CreateUnitOfWork(UnitOfWorkScopeOption.Suppress);

        Assert.Throws<InvalidOperationException>(() =>
        {
            provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        });
    }

    // ---------------------------------------------------------
    // TC_UOWP_061 — Unhandled scope combination throws
    // ---------------------------------------------------------
    [Test]
    public void TC_UOWP_061_UnhandledCombination_Throws()
    {
        var provider = CreateProvider();

        // To trigger the final throw in CreateUnitOfWork we must create a path not covered by the existing ifs.
        // Given current implementation, all pairs are covered except if GetActiveUnitOfWork() returns something surprising.
        // We'll simulate invalid state by manually pushing a dummy UnitOfWorkStackEntry with an unexpected scope (via reflection).
        var stackField = typeof(UnitOfWorkProvider).GetField("_unitsOfWork", BindingFlags.NonPublic | BindingFlags.Instance);
        var stack = stackField!.GetValue(provider);
        var stackType = stack!.GetType();

        // Create a UnitOfWorkStackEntry with an unsupported combination via reflection by instantiating the nested type.
        var nestedType = typeof(UnitOfWorkProvider).GetNestedType("UnitOfWorkStackEntry", BindingFlags.NonPublic);
        var ctor = nestedType!.GetConstructor(new[] { typeof(IDbConnectionFactory), typeof(ConnectionMode), typeof(UnitOfWorkScopeOption) });
        var weirdEntry = ctor!.Invoke(new object[] { _connectionFactory, ConnectionMode.ReadOnly, (UnitOfWorkScopeOption)999 });

        // Push the weird entry onto the stack using reflection (call Push)
        var pushMethod = stackType.GetMethod("Push");
        pushMethod!.Invoke(stack, new[] { weirdEntry });

        // Now try to create a required unit of work with an unexpected active scope - should reach the final throw
        Assert.Throws<InvalidOperationException>(() =>
        {
            provider.CreateUnitOfWork(UnitOfWorkScopeOption.Required);
        });
    }
    */
}

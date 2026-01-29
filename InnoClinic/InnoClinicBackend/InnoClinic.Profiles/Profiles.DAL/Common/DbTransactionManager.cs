using Microsoft.EntityFrameworkCore.Storage;

namespace Profiles.DAL.Common;

public class DbTransactionManager(ProfilesDbContext dbContext) : IDbTransactionManager
{
    private IDbContextTransaction? _currentTransaction;

    public async Task BeginTransactionAsync(CancellationToken cancellationToken) =>
        _currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
        if (_currentTransaction != null) await _currentTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if (_currentTransaction != null) await _currentTransaction.RollbackAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _currentTransaction?.Dispose();
}

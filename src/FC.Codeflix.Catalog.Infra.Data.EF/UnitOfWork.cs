using FC.Codeflix.Catalog.Application.Interfaces;

namespace FC.Codeflix.Catalog.Infra.Data.EF;

public class UnitOfWork(CodeflixCatalogDbContext context) : IUnitOfWork
{
    private readonly CodeflixCatalogDbContext _context = context;

    public Task Commit(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task Rollback(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

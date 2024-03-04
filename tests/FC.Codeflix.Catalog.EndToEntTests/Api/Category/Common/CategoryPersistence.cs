using FC.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.EndToEntTests.Api.Category.Common;

public class CategoryPersistence(CodeflixCatalogDbContext context)
{
    private readonly CodeflixCatalogDbContext _context = context;

    public async Task<DomainEntity.Category?> GetById(Guid id) =>
        await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
}

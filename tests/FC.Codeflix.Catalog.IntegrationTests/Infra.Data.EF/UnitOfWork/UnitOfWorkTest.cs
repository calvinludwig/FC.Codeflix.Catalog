using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UnitOfWorkInfra = FC.Codeflix.Catalog.Infra.Data.EF;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.UnitOfWork;

[Collection(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTest
{
    private readonly UnitOfWorkTestFixture _fixture = new();

    [Fact(DisplayName = nameof(ShouldCommitChanges))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task ShouldCommitChanges()
    {
        var dbContext = _fixture.CreateDbContext();
        var exempleCategoriesList = _fixture.GetExampleCategoriesList();
        await dbContext.AddRangeAsync(exempleCategoriesList);
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);

        await unitOfWork.Commit(CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var savedCategories = assertDbContext.Categories.AsNoTracking().ToList();

        savedCategories.Should().HaveCount(exempleCategoriesList.Count);
    }

    [Fact(DisplayName = nameof(ShouldRollbackChanges))]
    [Trait("Integration/Infra.Data", "UnitOfWork - Persistence")]
    public async Task ShouldRollbackChanges()
    {
        var dbContext = _fixture.CreateDbContext();
        var unitOfWork = new UnitOfWorkInfra.UnitOfWork(dbContext);
        var task = async () => await unitOfWork.Rollback(CancellationToken.None);
        await task.Should().NotThrowAsync();
    }
}

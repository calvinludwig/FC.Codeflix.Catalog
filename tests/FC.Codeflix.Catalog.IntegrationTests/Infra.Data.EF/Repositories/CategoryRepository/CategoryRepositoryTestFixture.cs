using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.Infra.Data.EF;
using FC.Codeflix.Catalog.IntegrationTests.Common;
using Microsoft.EntityFrameworkCore;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

public class CategoryRepositoryTestFixture : BaseFixture
{
    public CodeflixCatalogDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new CodeflixCatalogDbContext(
            new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );

        if (!preserveData)
            context.Database.EnsureDeleted();

        return context;
    }

    public Category GetExampleCategory() =>
        new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBool());

    public List<Category> GetExampleCategoriesList(int length = 10) =>
        Enumerable.Range(1, length).Select(_ => GetExampleCategory()).ToList();

    public List<Category> GetExampleCategoriesListWithNames(IEnumerable<string> names) =>
        names
            .Select(name =>
            {
                var category = GetExampleCategory();
                category.Update(name);
                return category;
            })
            .ToList();

    public List<Category> CloneCategoriesListOrdered(List<Category> categoriesList, string orderBy, SearchOrder order)
    {
        var listClone = new List<Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name),
        };
        return [.. orderedEnumerable];
    }

    public string GetValidCategoryName()
    {
        var name = "";
        while (name.Length is < 3 or > 255)
            name = Faker.Commerce.Categories(1)[0];

        return name;
    }

    public string GetValidCategoryDescription()
    {
        var description = "";
        while (description.Length > 10_000 || description == "")
            description = Faker.Commerce.ProductDescription();

        return description;
    }

    public static bool GetRandomBool()
    {
        return new Random().NextDouble() < 0.5;
    }
}

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixtureCollection
    : ICollectionFixture<CategoryRepositoryTestFixture>;

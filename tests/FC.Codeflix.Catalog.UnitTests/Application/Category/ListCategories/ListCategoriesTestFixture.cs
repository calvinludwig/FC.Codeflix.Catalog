using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>;

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    public List<Catalog.Domain.Entity.Category> GetExampleCategoriesList(int len = 1)
    {
        var list = new List<Catalog.Domain.Entity.Category>();
        for (var i = 0; i < len; i++)
            list.Add(GetExampleCategory());
        return list;
    }

    public ListCategoriesInput GetExampleInput()
    {
        var random = new Random();
        return new ListCategoriesInput(
            random.Next(1, 10),
            random.Next(15, 100),
            Faker.Commerce.ProductName(),
            Faker.Commerce.ProductName(),
            random.Next(0, 10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
        );
    }
}

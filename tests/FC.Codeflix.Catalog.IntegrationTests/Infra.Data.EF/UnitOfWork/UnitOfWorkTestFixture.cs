using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.IntegrationTests.Common;

namespace FC.Codeflix.Catalog.IntegrationTests;

public class UnitOfWorkTestFixture : BaseFixture
{
    public Category GetExampleCategory() =>
           new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBool());

    public List<Category> GetExampleCategoriesList(int length = 10) =>
        Enumerable.Range(1, length).Select(_ => GetExampleCategory()).ToList();

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

[CollectionDefinition(nameof(UnitOfWorkTestFixture))]
public class UnitOfWorkTestFixtureCollection : ICollectionFixture<UnitOfWorkTestFixture>;

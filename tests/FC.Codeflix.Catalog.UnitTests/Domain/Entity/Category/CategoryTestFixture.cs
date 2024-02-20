using FC.Codeflix.Catalog.UnitTests.Common;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
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

    public DomainEntity.Category GetValidCategory()
    {
        return new DomainEntity.Category(GetValidCategoryName(), GetValidCategoryDescription());
    }
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>;

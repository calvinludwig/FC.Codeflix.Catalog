using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>;

public class UpdateCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public Category GetExampleCategory()
        => new(GetValidCategoryName(), GetValidCategoryDescription(), GetRandomBool());

    public string GetValidCategoryName()
    {
        var name = "";
        while (name.Length is < 3 or > 255)
        {
            name = Faker.Commerce.Categories(1)[0];
        }

        return name;
    }

    public string GetValidCategoryDescription()
    {
        var description = "";
        while (description.Length > 10_000 || description == "")
        {
            description = Faker.Commerce.ProductDescription();
        }

        return description;
    }

    public bool GetRandomBool() => (new Random()).NextDouble() < 0.5;
}
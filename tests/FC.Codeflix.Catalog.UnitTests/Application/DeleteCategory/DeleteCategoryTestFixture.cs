using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory;

public class DeleteCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public Category GetValidCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription()
        );

    private string GetValidCategoryName()
    {
        var name = "";
        while (name.Length is < 3 or > 255)
        {
            name = Faker.Commerce.Categories(1)[0];
        }

        return name;
    }

    private string GetValidCategoryDescription()
    {
        var description = "";
        while (description.Length > 10_000 || description == "")
        {
            description = Faker.Commerce.ProductDescription();
        }

        return description;
    }
}

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>;
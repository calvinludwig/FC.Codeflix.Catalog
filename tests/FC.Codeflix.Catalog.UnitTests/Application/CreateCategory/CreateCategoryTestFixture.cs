using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.UnitTests.Common;
using Moq;

namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTestFixture : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
    
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
            description = Faker.Commerce.ProductDescription();

        return description;
    }

    private static bool GetRandomBool() => (new Random()).NextDouble() < 0.5;

    public CreateCategoryInput GetInput()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBool());

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var invalidInput = GetInput();
        invalidInput.Name = invalidInput.Name[..2];
        return invalidInput;
    }

    public CreateCategoryInput GetInvalidInputLongName()
    {
        var invalidInput = GetInput();
        invalidInput.Name = Faker.Lorem.Letter(256);
        return invalidInput;
    }

    public CreateCategoryInput GetInvalidInputLongDescription()
    {
        var invalidInput = GetInput();
        invalidInput.Description = Faker.Lorem.Letter(10_001);
        return invalidInput;
    }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>;
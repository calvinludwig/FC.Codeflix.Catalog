using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public CreateCategoryInput GetInput()
    {
        return new CreateCategoryInput(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBool()
        );
    }

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

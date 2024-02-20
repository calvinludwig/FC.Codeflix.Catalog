using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.UnitTests.Application.Category.Common;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>;

public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryInput GetValidInput(Guid? id = null)
    {
        return new UpdateCategoryInput(
            id ?? Guid.NewGuid(),
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBool()
        );
    }
}

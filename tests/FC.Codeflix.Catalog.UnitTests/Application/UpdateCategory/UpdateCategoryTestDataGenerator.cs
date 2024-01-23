using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();

        for (var i = 0; i < times; i++)
        {
            var category = fixture.GetExampleCategory();
            var input = new UseCase.UpdateCategoryInput(
                category.Id,
                fixture.GetValidCategoryName(),
                fixture.GetValidCategoryDescription(),
                fixture.GetRandomBool()
            );

            yield return [category, input];
        }
    }
}
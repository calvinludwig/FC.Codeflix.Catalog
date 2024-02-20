namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (var i = 0; i < times; i++)
        {
            var category = fixture.GetExampleCategory();
            var input = fixture.GetValidInput(category.Id);
            yield return [category, input];
        }
    }
}

using FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

public class ListCategoriesTestDataGenerator
{
    public static IEnumerable<object[]> GetInputsWithoutAllParameters(int times = 18)
    {
        var fixture = new ListCategoriesTestFixture();
        var inputExample = fixture.GetExampleInput();

        for (var i = 0; i < 15; i++)
            switch (i % 6)
            {
                case 1:
                    yield return [new ListCategoriesInput(inputExample.Page)];
                    break;
                case 2:
                    yield return [new ListCategoriesInput(inputExample.Page, inputExample.PerPage)];
                    break;
                case 3:
                    yield return
                    [
                        new ListCategoriesInput(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search
                        )
                    ];
                    break;
                case 4:
                    yield return
                    [
                        new ListCategoriesInput(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search,
                            inputExample.Sort
                        )
                    ];
                    break;
                case 5:
                    yield return [inputExample];
                    break;
                default:
                    yield return [new ListCategoriesInput()];
                    break;
            }
    }
}

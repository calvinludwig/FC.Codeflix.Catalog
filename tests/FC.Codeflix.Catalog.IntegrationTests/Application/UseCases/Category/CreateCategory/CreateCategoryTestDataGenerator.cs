namespace FC.Codeflix.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(
                        [
                            fixture.GetInvalidInputShortName(),
                            "Name should have at least 3 characters"
                        ]
                    );
                    break;
                case 1:
                    invalidInputsList.Add(
                        [
                            fixture.GetInvalidInputTooLongName(),
                            "Name should have at most 255 characters"
                        ]
                    );
                    break;
                case 2:
                    invalidInputsList.Add(
                        [fixture.GetInvalidInputCategoryNull(), "Description should not be null"]
                    );
                    break;
                case 3:
                    invalidInputsList.Add(
                        [
                            fixture.GetInvalidInputTooLongDescription(),
                            "Description should have at most 10000 characters"
                        ]
                    );
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}

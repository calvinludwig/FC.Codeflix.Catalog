namespace FC.Codeflix.Catalog.EndToEntTests.Api.Category.CreateCategory;

public class CreateCategoryApiTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryApiTestFixture();
        var invalidInputsList = new List<object[]>();

        for (int index = 0; index < 3; index++)
        {
            var input = fixture.GetExampleInput();
            switch (index % 3)
            {
                case 0:
                    input.Name = fixture.GetInvalidNameTooShort();
                    invalidInputsList.Add([input, "Name should have at least 3 characters"]);
                    break;
                case 1:
                    input.Name = fixture.GetInvalidNameTooLong();
                    invalidInputsList.Add(
                        [input, "Name should have at most 255 characters"]
                    );
                    break;
                case 2:
                    input.Description = fixture.GetInvalidDescriptionTooLong();
                    invalidInputsList.Add(
                        [input, "Description should have at most 10000 characters"]
                    );
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}

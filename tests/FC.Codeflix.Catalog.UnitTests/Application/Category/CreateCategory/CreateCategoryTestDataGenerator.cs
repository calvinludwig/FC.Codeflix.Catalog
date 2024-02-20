namespace FC.Codeflix.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateCategoryTestFixture();
        var invalidInputList = new List<object[]>();

        for (var i = 0; i < times; i++)
            switch (i % 3)
            {
                case 0:
                    invalidInputList.Add(
                        [
                            fixture.GetInvalidInputShortName(),
                            "Name should have at least 3 characters"
                        ]
                    );
                    break;
                case 1:
                    invalidInputList.Add(
                        [
                            fixture.GetInvalidInputLongName(),
                            "Name should have at most 255 characters"
                        ]
                    );
                    break;
                case 2:
                    invalidInputList.Add(
                        [
                            fixture.GetInvalidInputLongDescription(),
                            "Description should have at most 10000 characters"
                        ]
                    );
                    break;
            }

        return invalidInputList;
    }
}

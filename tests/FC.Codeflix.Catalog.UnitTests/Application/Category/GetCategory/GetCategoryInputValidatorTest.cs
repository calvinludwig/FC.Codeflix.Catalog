using FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryInputValidatorTest(GetCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ItShouldPassAValidInput))]
    [Trait("Application", "GetCategoryInputValidation - Use Case")]
    public void ItShouldPassAValidInput()
    {
        var validInput = new GetCategoryInput(Guid.NewGuid());
        var validator = new GetCategoryInputValidator();
        var result = validator.Validate(validInput);
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(ItShouldNotPassAInvalidInput))]
    [Trait("Application", "GetCategoryInputValidation - Use Case")]
    public void ItShouldNotPassAInvalidInput()
    {
        var validInput = new GetCategoryInput(Guid.Empty);
        var validator = new GetCategoryInputValidator();
        var result = validator.Validate(validInput);
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }
}

using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest(UpdateCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ItShouldPassWhenValidInput))]
    [Trait("Application", "Update Category Input - Use Cases")]
    public void ItShouldPassWhenValidInput()
    {
        var input = fixture.GetValidInput();
        var validator = new UpdateCategoryInputValidator();
        var result = validator.Validate(input);
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }

    [Fact(DisplayName = nameof(ItShouldNotPassEmptyId))]
    [Trait("Application", "Update Category Input - Use Cases")]
    public void ItShouldNotPassEmptyId()
    {
        var input = fixture.GetValidInput(Guid.Empty);
        var validator = new UpdateCategoryInputValidator();
        var result = validator.Validate(input);
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
    }
}

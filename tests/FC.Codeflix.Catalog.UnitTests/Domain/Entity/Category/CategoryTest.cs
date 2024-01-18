using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest(CategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ItShouldBeAbleToInstantiateACategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldBeAbleToInstantiateACategory()
    {
        var validData = new { Name = "Category Name", Description = "Category Description" };
        var datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validData.Name, validData.Description);
        var datetimeAfter = DateTime.Now;
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        category.IsActive.Should().BeTrue();
    }

    [Theory(DisplayName = nameof(ItShouldBeAbleToInstantiateACategoryWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void ItShouldBeAbleToInstantiateACategoryWithIsActive(bool isActive)
    {
        var validData = new { Name = "Category Name", Description = "Category Description" };
        var datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);
        var datetimeAfter = DateTime.Now;
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default);
        (category.CreatedAt > datetimeBefore).Should().BeTrue();
        (category.CreatedAt < datetimeAfter).Should().BeTrue();
        category.IsActive.Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(ItShouldThrowWhenNameIsEmptyOnInstantiate))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ItShouldThrowWhenNameIsEmptyOnInstantiate(string? name)
    {
        Action action = () => _ = new DomainEntity.Category(name!, "Category Description");
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be null or empty");
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenDescriptionIsNullOnInstantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldThrowWhenDescriptionIsNullOnInstantiate()
    {
        Action action = () => _ = new DomainEntity.Category("Category Name", null!);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should not be null");
    }

    [Theory(DisplayName = nameof(ItShouldThrowWhenNameHasLessThan3CharactersOnInstantiate))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(GetNamesWithLessThan3Characters), parameters: 10)]
    public void ItShouldThrowWhenNameHasLessThan3CharactersOnInstantiate(string invalidName)
    {
        Action action = () => _ = new DomainEntity.Category(invalidName, "Category Description");
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should have at least 3 characters");
    }

    public static IEnumerable<object[]> GetNamesWithLessThan3Characters(int numberOfTests = 6)
    {
        var fixture = new CategoryTestFixture();
        for (var i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;
            yield return [fixture.GetValidCategoryName()[..(isOdd ? 1 : 2)]];
        }
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenNameHasMoreThan255CharactersOnInstantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldThrowWhenNameHasMoreThan255CharactersOnInstantiate()
    {
        var invalidName = new string('a', 256);
        Action action = () => _ = new DomainEntity.Category(invalidName, "Category Description");
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should have at most 255 characters");
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenDescriptionHasMoreThan10_000CharactersOnInstantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldThrowWhenDescriptionHasMoreThan10_000CharactersOnInstantiate()
    {
        var invalidDescription = new string('a', 10_001);
        Action action = () => _ = new DomainEntity.Category("Category Name", invalidDescription);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should have at most 10000 characters");
    }

    [Fact(DisplayName = nameof(ItShouldBeAbleToActivateACategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldBeAbleToActivateACategory()
    {
        var validData = new { Name = "Category Name", Description = "Category Description" };
        var category = new DomainEntity.Category(validData.Name, validData.Description, false);
        category.Activate();
        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(ItShouldBeAbleToDeactivateACategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldBeAbleToDeactivateACategory()
    {
        var category = fixture.GetValidCategory();
        category.Deactivate();
        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(ItShouldBeAbleToUpdateTheCategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldBeAbleToUpdateTheCategory()
    {
        var category = fixture.GetValidCategory();
        var newValues = new { Name = "New Category Name", Description = "New Category Description" };
        category.Update(newValues.Name, newValues.Description);
        category.Name.Should().Be(newValues.Name);
        category.Description.Should().Be(newValues.Description);
    }

    [Fact(DisplayName = nameof(ItShouldBeAbleToUpdateOnlyNameOfTheCategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldBeAbleToUpdateOnlyNameOfTheCategory()
    {
        var category = fixture.GetValidCategory();
        var newValues = new { Name = "New Category Name" };
        var currentDescription = category.Description;
        category.Update(newValues.Name);
        category.Name.Should().Be(newValues.Name);
        category.Description.Should().Be(currentDescription);
    }

    [Theory(DisplayName = nameof(ItShouldThrowWhenNameIsEmptyOnUpdate))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void ItShouldThrowWhenNameIsEmptyOnUpdate(string? invalidName)
    {
        var category = fixture.GetValidCategory();
        var action = () => category.Update(invalidName!);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be null or empty");
    }

    [Theory(DisplayName = nameof(ItShouldThrowWhenNameIsLessThan3CharactersOnUpdate))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("a")]
    [InlineData("ab")]
    public void ItShouldThrowWhenNameIsLessThan3CharactersOnUpdate(string invalidName)
    {
        var category = fixture.GetValidCategory();
        var action = () => category.Update(invalidName);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should have at least 3 characters");
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenNameIsMoreThan255CharactersOnUpdate))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldThrowWhenNameIsMoreThan255CharactersOnUpdate()
    {
        var invalidName = fixture.Faker.Lorem.Letter(256);
        var category = fixture.GetValidCategory();
        var action = () => category.Update(invalidName!);
        action.Should().Throw<EntityValidationException>()
            .WithMessage($"Name should have at most 255 characters");
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenDescriptionIsMoreThan10_000CharactersOnUpdate))]
    [Trait("Domain", "Category - Aggregates")]
    public void ItShouldThrowWhenDescriptionIsMoreThan10_000CharactersOnUpdate()
    {
        var invalidDescription = fixture.Faker.Lorem.Letter(10_001);
        var category = fixture.GetValidCategory();
        var action = () => category.Update("Category Name", invalidDescription!);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should have at most 10000 characters");
    }
}

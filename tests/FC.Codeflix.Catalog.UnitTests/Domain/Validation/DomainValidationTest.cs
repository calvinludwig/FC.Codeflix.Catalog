using Bogus;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FC.Codeflix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.Codeflix.Catalog.UnitTests.Domain.Validation;

public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "Domain Validation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Commerce.ProductName();
        var action = () => DomainValidation.NotNull(value, "Value");
        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "Domain Validation - Validation")]
    public void NotNullThrowWhenNull()
    {
        string? value = null;
        var action = () => DomainValidation.NotNull(value!, "FieldName");
        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null");
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "Domain Validation - Validation")]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData(null)]
    public void NotNullOrEmptyThrowWhenEmpty(string? target)
    {
        var action = () => DomainValidation.NotNullOrEmpty(target!, "FieldName");
        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null or empty");
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "Domain Validation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanLength), 10)]
    public void MinLengthThrowWhenLess(string target, int minLength)
    {
        var action = () => DomainValidation.MinLength(target, minLength, "FieldName");
        action.Should().Throw<EntityValidationException>()
            .WithMessage($"FieldName should have at least {minLength} characters");
    }

    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "Domain Validation - Validation")]
    [MemberData(nameof(GetValuesBiggerThanLength), 3)]
    public void MinLengthOk(string target, int minLength)
    {
        var action = () => DomainValidation.MinLength(target, minLength, "FieldName");
        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "Domain Validation - Validation")]
    [MemberData(nameof(GetValuesBiggerThanLength), 10)]
    public void MaxLengthThrowWhenGreater(string target, int minLength)
    {
        var action = () => DomainValidation.MaxLength(target, minLength, "FieldName");
        action.Should().Throw<EntityValidationException>()
            .WithMessage($"FieldName should have at most {minLength} characters");
    }

    [Theory(DisplayName = nameof(MaxLengthOk))]
    [Trait("Domain", "Domain Validation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanLength), 3)]
    public void MaxLengthOk(string target, int minLength)
    {
        var action = () => DomainValidation.MaxLength(target, minLength, "FieldName");
        action.Should().NotThrow();
    }

    public static IEnumerable<object[]> GetValuesSmallerThanLength(int numberOfValues)
    {
        yield return ["ab", 3];
        var faker = new Faker();
        for (var i = 0; i < (numberOfValues - 1); i++)
        {
            var value = faker.Commerce.ProductName();
            var minLength = value.Length + (new Random()).Next(1, 20);
            yield return [value, minLength];
        }
    }

    public static IEnumerable<object[]> GetValuesBiggerThanLength(int numberOfValues)
    {
        yield return ["abcd", 3];
        var faker = new Faker();
        for (var i = 0; i < (numberOfValues - 1); i++)
        {
            var value = faker.Commerce.ProductName();
            var length = value.Length - (new Random()).Next(1, 5);
            if (length < 0)
                length = 0;
            yield return [value, length];
        }
    }
}
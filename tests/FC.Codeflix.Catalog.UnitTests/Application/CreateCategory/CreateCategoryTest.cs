using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest(CreateCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ItShouldBeAbleToCreateACategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void ItShouldBeAbleToCreateACategory()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );
        var input = fixture.GetInput();
        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repo => repo.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()),
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(input.IsActive);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBe(default);
    }

    [Theory(DisplayName = nameof(ItShouldThrowWhenCanNotInstantiateTheCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 12,
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async void ItShouldThrowWhenCanNotInstantiateTheCategory(
        UseCases.CreateCategoryInput input,
        string exceptionMessage
    )
    {
        var useCase = new UseCases.CreateCategory(
            fixture.GetRepositoryMock().Object,
            fixture.GetUnitOfWorkMock().Object
        );
        var action = async () => await useCase.Handle(input, CancellationToken.None);
        await action.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}
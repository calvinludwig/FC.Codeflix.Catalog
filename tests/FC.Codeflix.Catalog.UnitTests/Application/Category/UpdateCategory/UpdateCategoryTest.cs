using FC.Codeflix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest(UpdateCategoryTestFixture fixture)
{
    [Theory(DisplayName = nameof(ItShouldBeAbleToUpdateCategory))]
    [Trait("Application", "Update Category - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ItShouldBeAbleToUpdateCategory(
        Catalog.Domain.Entity.Category category,
        UseCase.UpdateCategoryInput input
    )
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        repositoryMock
            .Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be((bool)input.IsActive!);
        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(ItShouldThrowWhenCategoryNotFound))]
    [Trait("Application", "Update Category - Use Cases")]
    public async Task ItShouldThrowWhenCategoryNotFound()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var input = fixture.GetValidInput();
        repositoryMock
            .Setup(x => x.Get(input.Id, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException($"Category '{input.Id}' not found."));
        var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);
        var task = async () => await useCase.Handle(input, CancellationToken.None);
        await task.Should().ThrowAsync<NotFoundException>();
        repositoryMock.Verify(x => x.Get(input.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory(DisplayName = nameof(ItShouldBeAbleToUpdateCategoryWithoutIsActive))]
    [Trait("Application", "Update Category - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ItShouldBeAbleToUpdateCategoryWithoutIsActive(
        Catalog.Domain.Entity.Category category,
        UseCase.UpdateCategoryInput exampleInput
    )
    {
        var input = new UseCase.UpdateCategoryInput(
            exampleInput.Id,
            exampleInput.Name,
            exampleInput.Description
        );
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        repositoryMock
            .Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActive.Should().Be(category.IsActive);
        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory(DisplayName = nameof(ItShouldBeAbleToUpdateCategoryWithoutDescription))]
    [Trait("Application", "Update Category - Use Cases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ItShouldBeAbleToUpdateCategoryWithoutDescription(
        Catalog.Domain.Entity.Category category,
        UseCase.UpdateCategoryInput exampleInput
    )
    {
        var input = new UseCase.UpdateCategoryInput(exampleInput.Id, exampleInput.Name);
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        repositoryMock
            .Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var useCase = new UseCase.UpdateCategory(repositoryMock.Object, unitOfWorkMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(category.Description);
        output.IsActive.Should().Be(category.IsActive);
        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}

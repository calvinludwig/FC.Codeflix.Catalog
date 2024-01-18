using FC.Codeflix.Catalog.Application.Exceptions;
using Moq;
using FluentAssertions;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest(DeleteCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ItShouldBeAbleToDeleteACategory))]
    [Trait("Application", "DeleteCategory - Use Case")]
    public async Task ItShouldBeAbleToDeleteACategory()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var category = fixture.GetValidCategory();

        repositoryMock
            .Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(category);

        var input = new UseCase.DeleteCategoryInput(category.Id);
        var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);
        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Delete(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact(DisplayName = nameof(ItShouldThrowWhenCategoryDoesNotExists))]
    [Trait("Application", "DeleteCategory - Use Case")]
    public async Task ItShouldThrowWhenCategoryDoesNotExists()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var unitOfWorkMock = fixture.GetUnitOfWorkMock();
        var id = Guid.NewGuid();

        repositoryMock
            .Setup(x => x.Get(id, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new NotFoundException($"Category '{id}' not found."));

        var input = new UseCase.DeleteCategoryInput(id);
        var useCase = new UseCase.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);
        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();
        repositoryMock.Verify(x => x.Get(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Entity;
using FluentAssertions;
using Moq;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest(UpdateCategoryTestFixture fixture)
{
    [Theory(DisplayName = nameof(ItShouldBeAbleToUpdateCategory))]
    [Trait("Application", "Update Category - Use Cases")]
    [MemberData(nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator))]
    public async Task ItShouldBeAbleToUpdateCategory(Category category, UseCase.UpdateCategoryInput input)
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
        output.IsActive.Should().Be(input.IsActive);
        repositoryMock.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
        repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FluentAssertions;
using Moq;
using UseCase = FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest(ListCategoriesTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldBeAbleToListWithAllParameters))]
    [Trait("Application", "List Categories - Use Cases")]
    public async Task ShouldBeAbleToListWithAllParameters()
    {
        var categoriesExampleList = fixture.GetExampleCategoriesList();
        var repositoryMock = fixture.GetRepositoryMock();
        var input = fixture.GetExampleInput();
        var outputRepositorySearch = new SearchOutput<Catalog.Domain.Entity.Category>(
            input.Page,
            input.PerPage,
            items: categoriesExampleList,
            total: new Random().Next(50, 200)
        );
        repositoryMock
            .Setup(x =>
                x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<CategoryOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x =>
                x.Id == outputItem.Id
            );
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryCategory!.Name);
            outputItem.Description.Should().Be(repositoryCategory.Description);
            outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
            outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
        });

        repositoryMock.Verify(
            x =>
                x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                    ),
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Theory(DisplayName = nameof(ShouldBeAbleToListWithoutAllParameters))]
    [Trait("Application", "List Categories - Use Cases")]
    [MemberData(
        nameof(ListCategoriesTestDataGenerator.GetInputsWithoutAllParameters),
        18,
        MemberType = typeof(ListCategoriesTestDataGenerator)
    )]
    public async Task ShouldBeAbleToListWithoutAllParameters(UseCase.ListCategoriesInput input)
    {
        var categoriesExampleList = fixture.GetExampleCategoriesList();
        var repositoryMock = fixture.GetRepositoryMock();
        var outputRepositorySearch = new SearchOutput<Catalog.Domain.Entity.Category>(
            input.Page,
            input.PerPage,
            items: categoriesExampleList,
            total: new Random().Next(50, 200)
        );
        repositoryMock
            .Setup(x =>
                x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<CategoryOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x =>
                x.Id == outputItem.Id
            );
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryCategory!.Name);
            outputItem.Description.Should().Be(repositoryCategory.Description);
            outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
            outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
        });

        repositoryMock.Verify(
            x =>
                x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                    ),
                    It.IsAny<CancellationToken>()
                ),
            Times.Once
        );
    }

    [Fact(DisplayName = nameof(ShouldBeAbleToListWhenRepositoryIsEmpty))]
    [Trait("Application", "List Categories - Use Cases")]
    public async Task ShouldBeAbleToListWhenRepositoryIsEmpty()
    {
        var repositoryMock = fixture.GetRepositoryMock();
        var input = fixture.GetExampleInput();
        var outputRepositorySearch = new SearchOutput<Catalog.Domain.Entity.Category>(
            input.Page,
            input.PerPage,
            items: new List<Catalog.Domain.Entity.Category>().AsReadOnly(),
            total: 0
        );
        repositoryMock
            .Setup(x =>
                x.Search(
                    It.Is<SearchInput>(searchInput =>
                        searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.Search == input.Search
                        && searchInput.OrderBy == input.Sort
                    ),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListCategories(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);
    }
}

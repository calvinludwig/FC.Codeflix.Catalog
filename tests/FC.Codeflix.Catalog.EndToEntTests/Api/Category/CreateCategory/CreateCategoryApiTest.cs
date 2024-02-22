using System.Net;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FluentAssertions;

namespace FC.Codeflix.Catalog.EndToEntTests.Api.Category.CreateCategory;

[Collection(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTest(CreateCategoryApiTestFixture fixture)
{
	private readonly CreateCategoryApiTestFixture _fixture = fixture;

	[Fact(DisplayName = "")]
	[Trait("EndToEnd/API", "Category - Endpoints")]
	public async Task ShouldCreateCategory()
	{
		var input = _fixture.GetExampleInput();

		var (response, output) = await _fixture.ApiClient.Post<CategoryOutput>("/categories", input);

		response.Should().NotBeNull();
		response!.StatusCode.Should().Be(HttpStatusCode.Created);
		output.Should().NotBeNull();
		output.Name.Should().Be(input.Name);
		output.Description.Should().Be(input.Description);
		output.IsActive.Should().Be(input.IsActive);
		output.Id.Should().NotBeEmpty();
		output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

		var dbCategory = await _fixture.Persistence.GetById(output.Id);
		dbCategory.Should().NotBeNull();
		dbCategory!.Name.Should().Be(output.Name);
		dbCategory.Description.Should().Be(output.Description);
		dbCategory.IsActive.Should().Be(output.IsActive);
		dbCategory.Id.Should().Be(output.Id);
		dbCategory.CreatedAt.Should().Be(output.CreatedAt);
	}
}

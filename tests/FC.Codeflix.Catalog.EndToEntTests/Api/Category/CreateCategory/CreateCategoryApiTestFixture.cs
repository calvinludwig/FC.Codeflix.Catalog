using FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using FC.Codeflix.Catalog.EndToEntTests.Api.Category.Common;

namespace FC.Codeflix.Catalog.EndToEntTests.Api.Category.CreateCategory;

public class CreateCategoryApiTestFixture : CategoryBaseFixture
{
	public CreateCategoryInput GetExampleInput()
		=> new(
			GetValidCategoryName(),
			GetValidCategoryDescription(),
			getRandomBoolean()
		);
}

[CollectionDefinition(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTestFixtureCollection : ICollectionFixture<CreateCategoryApiTestFixture>;

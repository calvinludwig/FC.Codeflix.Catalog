namespace FC.Codeflix.Catalog.EndToEntTests;

public class GetCategoryApiTest
{
	public Task ShouldBeAbleToGetCategory()
	{
		// Arrange
		var category = new Category
		{
			Id = Guid.NewGuid(),
			Name = "Category 1"
		};
		_context.Categories.Add(category);
		_context.SaveChanges();
		// Act
		var response = await _client.GetAsync($"/api/categories/{category.Id}");
		var categoryResponse = await response.Content.ReadFromJsonAsync<CategoryResponse>();
		// Assert
		response.EnsureSuccessStatusCode();
		categoryResponse.Should().NotBeNull();
		categoryResponse.Id.Should().Be(category.Id);
		categoryResponse.Name.Should().Be(category.Name);
		return Task.CompletedTask;
	}
}

using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryInput(string name, string? description = null, bool isActive = true)
    : IRequest<CategoryOutput>
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description ?? "";
    public bool IsActive { get; } = isActive;
}
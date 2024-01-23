using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategoryInput(Guid id, string name, string? description = null, bool isActive = true)
    : IRequest<CategoryOutput>
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description ?? "";
    public bool IsActive { get; } = isActive;
}
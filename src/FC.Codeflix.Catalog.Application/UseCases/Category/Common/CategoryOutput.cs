using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.Common;

public class CategoryOutput(
    Guid id,
    string name,
    string description,
    bool isActive,
    DateTime createdAt
)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public bool IsActive { get; } = isActive;
    public DateTime CreatedAt { get; } = createdAt;

    public static CategoryOutput FromCategory(DomainEntity.Category category)
    {
        return new CategoryOutput(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );
    }
}

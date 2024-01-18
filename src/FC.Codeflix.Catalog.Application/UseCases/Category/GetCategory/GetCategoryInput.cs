using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInput(Guid id) : IRequest<CategoryOutput>
{
    public Guid Id { get; } = id;
}

using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Application.UseCases.Common;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesOutput(
    int page,
    int perPage,
    int total,
    IReadOnlyList<CategoryOutput> items
) : PaginatedListOutput<CategoryOutput>(page, perPage, total, items)
{ }

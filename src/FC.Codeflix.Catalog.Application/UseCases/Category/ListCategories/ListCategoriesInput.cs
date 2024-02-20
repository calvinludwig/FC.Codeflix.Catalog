using FC.Codeflix.Catalog.Application.UseCases.Common;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInput(
    int page = 1,
    int perPage = 15,
    string search = "",
    string sort = "",
    SearchOrder dir = SearchOrder.Asc
) : PaginatedListInput(page, perPage, search, sort, dir), IRequest<ListCategoriesOutput>;

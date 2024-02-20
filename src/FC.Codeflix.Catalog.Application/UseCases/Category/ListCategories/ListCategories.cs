using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategories(ICategoryRepository categoryRepository) : IListCategories
{
    public async Task<ListCategoriesOutput> Handle(
        ListCategoriesInput request,
        CancellationToken cancellationToken
    )
    {
        var searchOutput = await categoryRepository.Search(
            new SearchInput(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );
        return new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items.Select(CategoryOutput.FromCategory).ToList()
        );
    }
}

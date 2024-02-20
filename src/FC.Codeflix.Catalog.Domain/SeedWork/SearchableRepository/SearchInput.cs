namespace FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

public class SearchInput(int page, int perPage, string search, string orderBy, SearchOrder order)
{
    public int Page { get; set; } = page;
    public int PerPage { get; set; } = perPage;
    public string Search { get; set; } = search;
    public string OrderBy { get; set; } = orderBy;
    public SearchOrder Order { get; set; } = order;
}

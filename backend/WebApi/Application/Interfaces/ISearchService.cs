using Application.DTO.Search;

namespace Application.Interfaces;

public interface ISearchService
{
    Task<IReadOnlyList<SearchResultDto>> SearchAsync( SearchFilterDto filter, CancellationToken ct );
}

using Domain.Filters;
using Domain.Results;

namespace Domain.Services;

public interface ISearchService
{
    Task<IReadOnlyList<SearchResult>> SearchAsync( SearchFilter filter, CancellationToken ct = default );
}

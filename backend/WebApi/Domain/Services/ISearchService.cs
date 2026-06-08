using Domain.Filters;
using Domain.Results;

namespace Domain.Services;

public interface ISearchService
{
    Task<IEnumerable<SearchResult>> SearchAsync( SearchFilter filter, CancellationToken ct = default );
}

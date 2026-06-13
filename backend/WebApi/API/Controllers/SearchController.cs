using API.DTO;
using API.Mappers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController( ISearchService searchService )
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SearchResultDto>>> Search( [FromQuery] SearchRequest request, CancellationToken ct )
    {
        var filter = request.MapToSearchFilterDto();
        var results = await _searchService.SearchAsync( filter, ct );

        return Ok( results
            .Select( r => r.MapToSearchResultDto() ) );
    }
}

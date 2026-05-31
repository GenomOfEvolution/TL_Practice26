using API.DTO;
using API.Mappers;
using Domain.Filters;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "search" )]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController( ISearchService searchService )
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SearchResultDTO>>> Search( [FromQuery] SearchFilter filter )
    {
        var results = await _searchService.SearchAsync( filter );

        return Ok( results
            .Select( EntityToSearchResultDtoMapper.Map )
            .ToList() );
    }
}

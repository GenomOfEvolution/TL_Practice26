using API.DTO;
using API.Mappers;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "[controller]" )]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController( ISearchService searchService )
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<List<SearchResultDto>>> Search( [FromQuery] SearchRQ request )
    {
        var filter = SearchRQToSearchFilterDtoMapper.Map( request );
        var results = await _searchService.SearchAsync( filter );

        return Ok( results
            .Select( EntityToSearchResultDtoMapper.Map ) );
    }
}

using API.DTO;
using API.Mappers;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IRoomTypeService _roomTypeService;

    public PropertiesController(
        IPropertyService propertyService,
        IRoomTypeService roomTypeService )
    {
        _propertyService = propertyService;
        _roomTypeService = roomTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PropertyRP>>> GetProperties()
    {
        IReadOnlyList<PropertyDto> properties = await _propertyService.GetAllAsync();

        return Ok( properties
            .Select( PropertyDtoToPropertyRPMapper.Map ) );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<PropertyRP>> GetById( [FromRoute] int id )
    {
        var property = await _propertyService.GetByIdAsync( id );

        if ( property is null )
        {
            return NotFound();
        }

        return PropertyDtoToPropertyRPMapper.Map( property );
    }

    [HttpGet( "{id:int}/roomtypes" )]
    public async Task<ActionResult<List<RoomTypeRP>>> GetRoomTypes( [FromRoute] int id )
    {
        IReadOnlyList<RoomTypeDto> roomTypes = await _roomTypeService.GetByPropertyIdAsync( id );

        return Ok( roomTypes
            .Select( RoomTypeDtoToRoomTypeRPMapper.Map ) );
    }

    [HttpPost]
    public async Task<ActionResult<PropertyRP>> AddProperty( [FromBody] CreatePropertyRQ request )
    {
        CreatePropertyDto dto = CreatePropertyRQToCreatePropertyDtoMapper.Map( request );
        int id = await _propertyService.CreateAsync( dto );

        return CreatedAtAction(
            nameof( GetById ),
            new { id },
            null );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateProperty( [FromRoute] int id, [FromBody] UpdatePropertyRQ request )
    {
        PropertyDto? existing = await _propertyService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        var dto = UpdatePropertyRQToUpdatePropertyDtoMapper.Map( request, id );

        await _propertyService.UpdateAsync( dto );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteProperty( [FromRoute] int id )
    {
        PropertyDto? existing = await _propertyService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        await _propertyService.DeleteAsync( id );

        return NoContent();
    }
}

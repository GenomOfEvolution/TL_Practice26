using API.DTO;
using API.Mappers;
using Domain.Services;
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
    public async Task<ActionResult<List<PropertyDTO>>> GetProperties()
    {
        var properties = await _propertyService.GetAllAsync();

        return Ok( properties
            .Select( EntityToPropertyDtoMapper.Map )
            .ToList() );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<PropertyDTO>> GetById( [FromRoute] int id )
    {
        var property = await _propertyService.GetByIdAsync( id );

        if ( property is null )
        {
            return NotFound();
        }

        return EntityToPropertyDtoMapper.Map( property );
    }

    [HttpGet( "{id:int}/roomtypes" )]
    public async Task<ActionResult<List<RoomTypeDTO>>> GetRoomTypes( [FromRoute] int id )
    {
        var roomTypes = await _roomTypeService.GetByPropertyIdAsync( id );

        return Ok( roomTypes
            .Select( EntityToRoomTypeDtoMapper.Map )
            .ToList() );
    }

    [HttpPost]
    public async Task<ActionResult<PropertyDTO>> AddProperty( [FromBody] PropertyDTO propertyDTO )
    {
        var entity = PropertyDtoToEntityMapper.Map( propertyDTO );
        var created = await _propertyService.CreateAsync( entity );

        return CreatedAtAction(
            nameof( GetById ),
            new { id = created.Id },
            EntityToPropertyDtoMapper.Map( created ) );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateProperty( [FromRoute] int id, [FromBody] PropertyDTO propertyDTO )
    {
        var existing = await _propertyService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        var property = PropertyDtoToEntityMapper.Map( propertyDTO, id );

        await _propertyService.UpdateAsync( property );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteProperty( [FromRoute] int id )
    {
        var existing = await _propertyService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        await _propertyService.DeleteAsync( id );

        return NoContent();
    }

}

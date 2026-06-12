using API.DTO;
using API.Mappers;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
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
    public async Task<ActionResult<List<PropertyRP>>> GetProperties( CancellationToken ct )
    {
        IReadOnlyList<PropertyDto> properties = await _propertyService.GetAllAsync( ct );

        return Ok( properties
            .Select( PropertyDtoToPropertyRPMapper.Map ) );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<PropertyRP>> GetById( [FromRoute] int id, CancellationToken ct )
    {
        PropertyDto property = await _propertyService.GetByIdAsync( id, ct );

        return PropertyDtoToPropertyRPMapper.Map( property );
    }

    [HttpGet( "{id:int}/roomtypes" )]
    public async Task<ActionResult<List<RoomTypeRP>>> GetRoomTypes( [FromRoute] int id, CancellationToken ct )
    {
        IReadOnlyList<RoomTypeDto> roomTypes = await _roomTypeService.GetByPropertyIdAsync( id, ct );

        return Ok( roomTypes
            .Select( RoomTypeDtoToRoomTypeRPMapper.Map ) );
    }

    [HttpPost]
    public async Task<ActionResult<PropertyRP>> AddProperty( [FromBody] CreatePropertyRQ request, CancellationToken ct )
    {
        CreatePropertyDto dto = CreatePropertyRQToCreatePropertyDtoMapper.Map( request );
        int id = await _propertyService.CreateAsync( dto, ct );

        return CreatedAtAction(
            nameof( GetById ),
            new { id },
            null );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateProperty( [FromRoute] int id, [FromBody] UpdatePropertyRQ request, CancellationToken ct )
    {
        var dto = UpdatePropertyRQToUpdatePropertyDtoMapper.Map( request, id );

        await _propertyService.UpdateAsync( dto, ct );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteProperty( [FromRoute] int id, CancellationToken ct )
    {
        await _propertyService.DeleteAsync( id, ct );

        return NoContent();
    }
}

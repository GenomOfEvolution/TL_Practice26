using API.DTO;
using API.Mappers;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "roomtypes" )]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypeController( IRoomTypeService roomTypeService )
    {
        _roomTypeService = roomTypeService;
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<RoomTypeDTO>> GetById( [FromRoute] int id )
    {
        var roomType = await _roomTypeService.GetByIdAsync( id );

        if ( roomType is null )
        {
            return NotFound();
        }

        return EntityToRoomTypeDtoMapper.Map( roomType );
    }

    [HttpPost]
    public async Task<ActionResult<RoomTypeDTO>> AddRoomType( [FromBody] RoomTypeDTO roomTypeDTO )
    {
        var entity = RoomTypeDtoToEntityMapper.Map( roomTypeDTO );
        var id = await _roomTypeService.CreateAsync( entity );

        return CreatedAtAction(
            nameof( GetById ),
            new { id },
            null );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateRoomType( [FromRoute] int id, [FromBody] RoomTypeDTO roomTypeDTO )
    {
        var existing = await _roomTypeService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        var roomType = RoomTypeDtoToEntityMapper.Map( roomTypeDTO, id );

        await _roomTypeService.UpdateAsync( roomType );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteRoomType( [FromRoute] int id )
    {
        var existing = await _roomTypeService.GetByIdAsync( id );

        if ( existing is null )
        {
            return NotFound();
        }

        await _roomTypeService.DeleteAsync( id );

        return NoContent();
    }
}

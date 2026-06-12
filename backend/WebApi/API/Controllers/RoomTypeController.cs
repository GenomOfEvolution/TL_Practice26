using API.DTO;
using API.Mappers;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "[controller]" )]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;

    public RoomTypeController( IRoomTypeService roomTypeService )
    {
        _roomTypeService = roomTypeService;
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<RoomTypeRP>> GetById( [FromRoute] int id, CancellationToken ct )
    {
        RoomTypeDto? roomType = await _roomTypeService.GetByIdAsync( id, ct );

        if ( roomType is null )
        {
            return NotFound();
        }

        return RoomTypeDtoToRoomTypeRPMapper.Map( roomType );
    }

    [HttpPost]
    public async Task<ActionResult<RoomTypeRP>> AddRoomType( [FromBody] CreateRoomTypeRQ request, CancellationToken ct )
    {
        var dto = CreateRoomTypeRQToCreateRoomTypeDtoMapper.Map( request );
        int id = await _roomTypeService.CreateAsync( dto, ct );

        return CreatedAtAction(
            nameof( GetById ),
            new { id },
            null );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateRoomType( [FromRoute] int id, [FromBody] UpdateRoomTypeRQ request, CancellationToken ct )
    {
        RoomTypeDto? existing = await _roomTypeService.GetByIdAsync( id, ct );

        if ( existing is null )
        {
            return NotFound();
        }

        var dto = UpdateRoomTypeRQToUpdateRoomTypeDtoMapper.Map( request, id );

        await _roomTypeService.UpdateAsync( dto, ct );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteRoomType( [FromRoute] int id, CancellationToken ct )
    {
        RoomTypeDto? existing = await _roomTypeService.GetByIdAsync( id, ct );

        if ( existing is null )
        {
            return NotFound();
        }

        await _roomTypeService.DeleteAsync( id, ct );

        return NoContent();
    }
}

using API.DTO;
using API.Mappers;
using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
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
        RoomTypeDto roomType = await _roomTypeService.GetByIdAsync( id, ct );

        return roomType.MapToRoomTypeRP();
    }

    [HttpPost]
    public async Task<ActionResult<RoomTypeRP>> AddRoomType( [FromBody] CreateRoomTypeRQ request, CancellationToken ct )
    {
        var dto = request.MapToCreateRoomTypeDto();
        int id = await _roomTypeService.CreateAsync( dto, ct );

        return CreatedAtAction(
            nameof( GetById ),
            new { id },
            null );
    }

    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> UpdateRoomType( [FromRoute] int id, [FromBody] UpdateRoomTypeRQ request, CancellationToken ct )
    {
        var dto = request.MapToUpdateRoomTypeDto( id );

        await _roomTypeService.UpdateAsync( dto, ct );

        return NoContent();
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteRoomType( [FromRoute] int id, CancellationToken ct )
    {
        await _roomTypeService.DeleteAsync( id, ct );

        return NoContent();
    }
}

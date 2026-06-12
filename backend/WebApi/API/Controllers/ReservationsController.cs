using API.DTO;
using API.Mappers;
using Application.Interfaces;
using Application.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationRP>> CreateReservation( [FromBody] CreateReservationRQ request, CancellationToken ct )
    {
        var dto = CreateReservationRQToCreateReservationDtoMapper.Map( request );

        int id = await _reservationService.CreateAsync( dto, ct );

        return CreatedAtAction(
            nameof( GetReservationById ),
            new { id },
            null );
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationRP>>> GetReservations( [FromQuery] ReservationFilterRQ request, CancellationToken ct )
    {
        ReservationFilterDto filter = ReservationFilterRQToReservationFilterDtoMapper.Map( request );
        IReadOnlyList<ReservationDto> reservations = await _reservationService.GetListAsync( filter, ct );

        return Ok( reservations
            .Select( ReservationDtoToReservationRPMapper.Map ) );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<ReservationRP>> GetReservationById( [FromRoute] int id, CancellationToken ct )
    {
        ReservationDto? reservation = await _reservationService.GetByIdAsync( id, ct );

        if ( reservation == null )
        {
            return NotFound();
        }

        return Ok( ReservationDtoToReservationRPMapper.Map( reservation ) );
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteReservation( [FromRoute] int id, CancellationToken ct )
    {
        await _reservationService.CancelAsync( id, ct );

        return NoContent();
    }
}

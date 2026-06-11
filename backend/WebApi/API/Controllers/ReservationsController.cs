using API.DTO;
using API.Mappers;
using Application.Interfaces;
using Application.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "[controller]" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationRP>> CreateReservation( [FromBody] CreateReservationRQ request )
    {
        var dto = CreateReservationRQToCreateReservationDtoMapper.Map( request );

        int id = await _reservationService.CreateAsync( dto );

        return CreatedAtAction(
            nameof( GetReservationById ),
            new { id },
            null );
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationRP>>> GetReservations( [FromQuery] ReservationFilterRQ request )
    {
        ReservationFilterDto filter = ReservationFilterRQToReservationFilterDtoMapper.Map( request );
        IReadOnlyList<ReservationDto> reservations = await _reservationService.GetListAsync( filter );

        return Ok( reservations
            .Select( ReservationDtoToReservationRPMapper.Map ) );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<ReservationRP>> GetReservationById( [FromRoute] int id )
    {
        ReservationDto? reservation = await _reservationService.GetByIdAsync( id );

        if ( reservation == null )
        {
            return NotFound();
        }

        return Ok( ReservationDtoToReservationRPMapper.Map( reservation ) );
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteReservation( [FromRoute] int id )
    {
        await _reservationService.CancelAsync( id );

        return NoContent();
    }
}

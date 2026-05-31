using API.DTO;
using API.Mappers;
using Domain.Filters;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route( "reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController( IReservationService reservationService )
    {
        _reservationService = reservationService;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> CreateReservation( [FromBody] CreateReservationRequest request )
    {
        var reservation = CreateReservationRequestToEntityMapper.Map( request );

        var created = await _reservationService.CreateAsync( reservation );

        return CreatedAtAction(
            nameof( GetReservationById ),
            new { id = created.Id },
            EntityToReservationDtoMapper.Map( created ) );
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservations( [FromQuery] ReservationFilter filter )
    {
        var reservations = await _reservationService.GetListAsync( filter );

        return Ok( reservations
            .Select( EntityToReservationDtoMapper.Map )
            .ToList() );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<ReservationDTO>> GetReservationById( [FromRoute] int id )
    {
        var reservation = await _reservationService.GetByIdAsync( id );

        if ( reservation == null )
        {
            return NotFound();
        }

        return Ok( EntityToReservationDtoMapper.Map( reservation ) );
    }

    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteReservation( [FromRoute] int id )
    {
        await _reservationService.CancelAsync( id );

        return NoContent();
    }
}

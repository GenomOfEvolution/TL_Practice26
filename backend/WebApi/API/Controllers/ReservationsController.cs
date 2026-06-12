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
    public async Task<ActionResult<ReservationResponse>> CreateReservation( [FromBody] CreateReservationRequest request, CancellationToken ct )
    {
        var dto = request.MapToCreateReservationDto();

        int id = await _reservationService.CreateAsync( dto, ct );

        return CreatedAtAction(
            nameof( GetReservationById ),
            new { id },
            null );
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationResponse>>> GetReservations( [FromQuery] ReservationFilterRequest request, CancellationToken ct )
    {
        ReservationFilterDto filter = request.MapToReservationFilterDto();
        IReadOnlyList<ReservationDto> reservations = await _reservationService.GetAllAsync( filter, ct );

        return Ok( reservations
            .Select( r => r.MapToReservationResponse() ) );
    }

    [HttpGet( "{id:int}" )]
    public async Task<ActionResult<ReservationResponse>> GetReservationById( [FromRoute] int id, CancellationToken ct )
    {
        ReservationDto reservation = await _reservationService.GetByIdAsync( id, ct );

        return Ok( reservation.MapToReservationResponse() );
    }

    [HttpPost( "{id:int}/cancel" )]
    public async Task<IActionResult> DeleteReservation( [FromRoute] int id, CancellationToken ct )
    {
        await _reservationService.CancelAsync( id, ct );

        return NoContent();
    }
}

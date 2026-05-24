using Domain.Repositories;
using Infrastructure.Foundation.Repositories;

namespace Infrastructure.Foundation;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IPropertyRepository? _properties;
    private IRoomTypeRepository? _roomTypes;
    private IReservationRepository? _reservations;

    public UnitOfWork( AppDbContext context )
    {
        _context = context;
    }

    public IPropertyRepository Properties =>
        _properties ??= new PropertyRepository( _context );

    public IRoomTypeRepository RoomTypes =>
        _roomTypes ??= new RoomTypeRepository( _context );

    public IReservationRepository Reservations =>
        _reservations ??= new ReservationRepository( _context );

    public async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default )
    {
        return await _context.SaveChangesAsync( cancellationToken );
    }
}

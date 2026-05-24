namespace Domain.Repositories;

public interface IUnitOfWork
{
    IPropertyRepository Properties { get; }
    IRoomTypeRepository RoomTypes { get; }
    IReservationRepository Reservations { get; }
    Task<int> SaveChangesAsync( CancellationToken cancellationToken = default );
}

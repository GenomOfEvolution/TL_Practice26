using Infrastructure.Foundation.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation;

public class AppDbContext : DbContext
{
    public AppDbContext( DbContextOptions options )
        : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
    }
}

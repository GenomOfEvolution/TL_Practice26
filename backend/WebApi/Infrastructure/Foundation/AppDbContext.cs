using Domain.Entities;
using Infrastructure.Foundation.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation;

public class AppDbContext : DbContext
{
    public AppDbContext( DbContextOptions options )
        : base( options )
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new PropertyConfiguration() );
        modelBuilder.ApplyConfiguration( new ReservationConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomTypeConfiguration() );
    }
}

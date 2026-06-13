using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure( EntityTypeBuilder<Reservation> builder )
    {
        builder.ToTable( nameof( Reservation ) );
        builder.HasKey( r => r.Id );
        builder.Property( r => r.Id ).HasColumnName( "id_reservation" );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne<RoomType>()
            .WithMany()
            .HasForeignKey( r => r.RoomTypeId )
            .OnDelete( DeleteBehavior.Restrict );

        builder.Property( r => r.PropertyId )
            .HasColumnName( "id_property" );

        builder.Property( r => r.RoomTypeId )
            .HasColumnName( "id_roomtype" );

        builder.Property( r => r.ArrivalDate )
            .IsRequired();

        builder.Property( r => r.ArrivalTime )
            .IsRequired();

        builder.Property( r => r.DepartureDate )
            .IsRequired();

        builder.Property( r => r.DepartureTime )
            .IsRequired();

        builder.Property( r => r.GuestName )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( r => r.GuestPhoneNumber )
            .HasMaxLength( 20 )
            .IsRequired();

        builder.Property( r => r.Total )
            .HasPrecision( 18, 2 );

        builder.Property( r => r.Currency )
            .IsRequired();

        builder.Property( r => r.IsCanceled )
            .IsRequired();

        builder.HasIndex( r => new { r.RoomTypeId, r.PropertyId } )
            .HasDatabaseName( "IX_Reservation_RoomTypeId_PropertyId" );
    }
}

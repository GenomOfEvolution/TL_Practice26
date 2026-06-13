using Domain.Entities;
using Domain.Enums;
using Infrastructure.Foundation.DbSeeds;
using Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
{
    public void Configure( EntityTypeBuilder<RoomType> builder )
    {
        builder.ToTable( nameof( RoomType ) );
        builder.HasKey( rt => rt.Id );
        builder.Property( rt => rt.Id ).HasColumnName( "id_roomtype" );

        builder.HasOne<Property>()
            .WithMany()
            .HasForeignKey( r => r.PropertyId )
            .OnDelete( DeleteBehavior.Cascade );

        builder.Property( r => r.PropertyId )
            .HasColumnName( "id_property" );

        builder.Property( r => r.Name )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( r => r.DailyPrice )
            .HasPrecision( 10, 2 )
            .IsRequired();

        builder.Property( r => r.MinPersonCount )
            .IsRequired();

        builder.Property( r => r.MaxPersonCount )
            .IsRequired();

        builder.Property( r => r.TotalRoomsCount )
            .IsRequired();

        builder.Property( r => r.Currency )
            .IsRequired();

        builder.Property( r => r.Services )
            .HasConversion( new EnumListToStringConverter<ServiceType>() );

        builder.Property( r => r.Amenities )
             .HasConversion( new EnumListToStringConverter<AmenitiesType>() );

        builder.HasIndex( r => r.PropertyId )
            .HasDatabaseName( "IX_RoomType_PropertyId" );

        builder.HasData( RoomTypeDbSeed.GetData() );
    }
}

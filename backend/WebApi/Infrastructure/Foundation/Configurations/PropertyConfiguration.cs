using Domain.Entities;
using Infrastructure.Foundation.DbSeeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure( EntityTypeBuilder<Property> builder )
    {
        builder.ToTable( nameof( Property ) );
        builder.HasKey( p => p.Id );
        builder.Property( p => p.Id ).HasColumnName( "id_property" );

        builder.Property( p => p.Name )
            .HasMaxLength( 100 )
            .IsRequired();

        builder.Property( p => p.Country )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( p => p.City )
            .HasMaxLength( 50 )
            .IsRequired();

        builder.Property( p => p.Address )
            .HasMaxLength( 500 )
            .IsRequired();

        builder.Property( p => p.Latitude )
            .IsRequired();

        builder.Property( p => p.Longitude )
            .IsRequired();

        builder.HasData( PropertyDbSeed.GetData() );
    }
}

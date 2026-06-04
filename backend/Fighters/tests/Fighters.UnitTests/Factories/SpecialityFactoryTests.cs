using Fighters.Factories;
using Fighters.Models.Specialities;

namespace Fighters.UnitTests.Factories;

public class SpecialityFactoryTests
{
    [Fact]
    public void Create_CorrectIndex_ReturnsCorrectSpeciality()
    {
        // Arrange
        var factory = new SpecialityFactory();

        // Act & Assert
        Assert.IsType<NoSpeciality>( factory.Create( 0 ) );
        Assert.IsType<Warrior>( factory.Create( 1 ) );
        Assert.IsType<Ranger>( factory.Create( 2 ) );
        Assert.IsType<Assassin>( factory.Create( 3 ) );
    }

    [Theory]
    [InlineData( -1 )]
    [InlineData( 4 )]
    public void Create_InvalidIndex_ThrowsArgumentOutOfRangeException( int invalidIndex )
    {
        // Arrange
        var factory = new SpecialityFactory();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( invalidIndex ) );
    }
}

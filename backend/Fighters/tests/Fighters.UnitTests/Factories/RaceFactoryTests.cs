using Fighters.Factories;
using Fighters.Models.Races;

namespace Fighters.UnitTests.Factories;

public class RaceFactoryTests
{
    [Fact]
    public void Create_CorrectIndex_ReturnsCorrectRace()
    {
        // Arrange
        var factory = new RaceFactory();

        // Act & Assert
        Assert.IsType<HumanRace>( factory.Create( 0 ) );
        Assert.IsType<DrowRace>( factory.Create( 1 ) );
        Assert.IsType<GnomeRace>( factory.Create( 2 ) );
        Assert.IsType<GoblinRace>( factory.Create( 3 ) );
        Assert.IsType<StoneGiantRace>( factory.Create( 4 ) );
    }

    [Theory]
    [InlineData( -1 )]
    [InlineData( 5 )]
    public void Create_InvalidIndex_ThrowsArgumentOutOfRangeException( int invalidIndex )
    {
        // Arrange
        var factory = new RaceFactory();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( invalidIndex ) );
    }
}

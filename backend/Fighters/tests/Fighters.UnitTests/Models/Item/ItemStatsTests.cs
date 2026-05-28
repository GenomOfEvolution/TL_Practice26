using Fighters.Models.Item;

namespace Fighters.UnitTests.Models.Item;

public class ItemStatsTests
{
    [Theory]
    [InlineData( 0, 0, 0 )]
    [InlineData( 50, 50, 50 )]
    [InlineData( 10, 20, 30 )]
    public void ItemStats_PropertiesCanBeSet( int strength, int dexterity, int intelligence )
    {
        // Arrange
        var stats = new ItemStats();

        // Act
        stats.Strength = strength;
        stats.Dexterity = dexterity;
        stats.Intelligence = intelligence;

        // Assert
        Assert.Equal( strength, stats.Strength );
        Assert.Equal( dexterity, stats.Dexterity );
        Assert.Equal( intelligence, stats.Intelligence );
    }

    [Theory]
    [InlineData( -1 )]
    [InlineData( 51 )]
    public void ItemStats_ValidateThrowsOnInvalidValue( int invalidValue )
    {
        // Arrange
        var stats = new ItemStats();

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => stats.Strength = invalidValue );
        Assert.Throws<ArgumentOutOfRangeException>( () => stats.Dexterity = invalidValue );
        Assert.Throws<ArgumentOutOfRangeException>( () => stats.Intelligence = invalidValue );
    }
}

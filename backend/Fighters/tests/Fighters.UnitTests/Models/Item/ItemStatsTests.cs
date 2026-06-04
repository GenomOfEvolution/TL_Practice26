using System.Reflection;
using Fighters.Models.Item;

namespace Fighters.UnitTests.Models.Item;

public class ItemStatsTests
{
    [Theory]
    [InlineData( 0, 0, 0 )]
    [InlineData( 50, 50, 50 )]
    [InlineData( 10, 20, 30 )]
    public void ItemStats_ValidValues_PropertiesCanBeSet( int strength, int dexterity, int intelligence )
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
    [InlineData( -1, nameof( ItemStats.Strength ) )]
    [InlineData( -1, nameof( ItemStats.Dexterity ) )]
    [InlineData( -1, nameof( ItemStats.Intelligence ) )]
    [InlineData( 51, nameof( ItemStats.Strength ) )]
    [InlineData( 51, nameof( ItemStats.Dexterity ) )]
    [InlineData( 51, nameof( ItemStats.Intelligence ) )]
    public void ItemStats_InvalidValue_ThrowsArgumentOutOfRangeException( int invalidValue, string propertyName )
    {
        // Arrange
        var stats = new ItemStats();
        var property = typeof( ItemStats ).GetProperty( propertyName );

        // Act
        Action act = () => property!.SetValue( stats, invalidValue );

        // Assert 
        var targetEx = Assert.Throws<TargetInvocationException>( act );
        Assert.IsType<ArgumentOutOfRangeException>( targetEx.InnerException );
    }
}

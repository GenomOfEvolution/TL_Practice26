using Fighters.Extensions;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Extensions;

public class IFighterExtensionsTests
{
    [Fact]
    public void IsAlive_HealthAboveZero_ReturnsTrue()
    {
        // Arrange
        var fighter = FighterBuilder.CreateDefault();

        // Act
        bool result = fighter.IsAlive();

        // Assert
        Assert.True( result );
    }

    [Fact]
    public void IsAlive_HealthZero_ReturnsFalse()
    {
        // Arrange
        var fighter = FighterBuilder.CreateDefault();
        fighter.TakeDamage( fighter.GetMaxHealth() + 1 );

        // Act
        bool result = fighter.IsAlive();

        // Assert
        Assert.False( result );
    }
}

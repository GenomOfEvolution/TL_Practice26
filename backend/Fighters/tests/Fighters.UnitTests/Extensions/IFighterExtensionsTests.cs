using Fighters.Extensions;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Extensions;

public class IFighterExtensionsTests
{
    [Fact]
    public void IsAlive_HealthAboveZero_ReturnsTrue()
    {
        var fighter = FighterBuilder.CreateDefault();
        Assert.True( fighter.IsAlive() );
    }

    [Fact]
    public void IsAlive_HealthZero_ReturnsFalse()
    {
        var fighter = FighterBuilder.CreateDefault();
        fighter.TakeDamage( 999999 );
        Assert.False( fighter.IsAlive() );
    }
}

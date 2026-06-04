using Fighters.Factories;
using Fighters.Models.Fighters;
using Fighters.Models.PointsBudget;

namespace Fighters.UnitTests.Factories;

[Collection( "Console tests" )]
public class FighterStatFactoryTests
{
    [Fact]
    public void TryCreate_NotZeroPoints_CreatesStats()
    {
        // Arrange
        var budget = new SharedPointsBudget( 30 );
        var factory = new FighterStatFactory();
        factory.SetBudget( budget );

        Console.SetIn( new StringReader( "10\n10\n10\n" ) );

        // Act
        bool result = factory.TryCreate( 0, out FighterStats? stats );

        // Assert
        Assert.True( result );
        Assert.NotNull( stats );
        Assert.Equal( 10, stats.Strength );
        Assert.Equal( 10, stats.Dexterity );
        Assert.Equal( 10, stats.Intelligence );
        Assert.Equal( 0, budget.RemainingPoints );
    }
}

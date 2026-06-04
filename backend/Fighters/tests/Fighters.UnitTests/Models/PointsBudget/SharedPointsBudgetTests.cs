using Fighters.Models.PointsBudget;

namespace Fighters.UnitTests.Models.PointsBudget;

public class SharedPointsBudgetTests
{
    [Fact]
    public void SharedPointsBudget_WithInitialValue_RemainingPointsMatches()
    {
        // Arrange
        var budget = new SharedPointsBudget( 100 );

        // Act
        int result = budget.RemainingPoints;

        // Assert
        Assert.Equal( 100, result );
    }

    [Fact]
    public void TrySpend_EnoughPoints_ReturnsTrue()
    {
        // Arrange
        var budget = new SharedPointsBudget( 100 );

        // Act
        bool result = budget.TrySpend( 50 );

        // Assert
        Assert.True( result );
    }

    [Fact]
    public void TrySpend_NotEnoughPoints_ReturnsFalse()
    {
        // Arrange
        var budget = new SharedPointsBudget( 10 );

        // Act
        bool result = budget.TrySpend( 50 );

        // Assert
        Assert.False( result );
    }

    [Fact]
    public void TrySpend_EnoughPoints_DeductsRemaining()
    {
        // Arrange
        var budget = new SharedPointsBudget( 100 );

        // Act
        budget.TrySpend( 30 );

        // Assert
        Assert.Equal( 70, budget.RemainingPoints );
    }
}

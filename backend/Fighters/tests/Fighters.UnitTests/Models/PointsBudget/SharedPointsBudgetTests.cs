using Fighters.Models.PointsBudget;

namespace Fighters.UnitTests.Models.PointsBudget;

public class SharedPointsBudgetTests
{
    [Fact]
    public void SharedPointsBudget_InitialPoints()
    {
        var budget = new SharedPointsBudget( 100 );
        Assert.Equal( 100, budget.RemainingPoints );
    }

    [Fact]
    public void SharedPointsBudget_TrySpend_EnoughPoints_ReturnsTrue()
    {
        var budget = new SharedPointsBudget( 100 );
        Assert.True( budget.TrySpend( 50 ) );
    }

    [Fact]
    public void SharedPointsBudget_TrySpend_NotEnough_ReturnsFalse()
    {
        var budget = new SharedPointsBudget( 10 );
        Assert.False( budget.TrySpend( 50 ) );
    }

    [Fact]
    public void SharedPointsBudget_TrySpend_DeductsPoints()
    {
        var budget = new SharedPointsBudget( 100 );
        budget.TrySpend( 30 );
        Assert.Equal( 70, budget.RemainingPoints );
    }
}

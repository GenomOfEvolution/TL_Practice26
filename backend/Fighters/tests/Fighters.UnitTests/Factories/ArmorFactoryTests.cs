using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.ItemCatalog;
using Fighters.Models.PointsBudget;

namespace Fighters.UnitTests.Factories;

public class ArmorFactoryTests
{
    [Fact]
    public void ArmorFactory_TryCreate_EnoughPoints_CreatesAndDeducts()
    {
        // Arrange
        var budget = new SharedPointsBudget( 100 );
        var catalog = new ArmorCatalog();
        var factory = new ArmorFactory( catalog );
        factory.SetBudget( budget );

        // Act
        bool result = factory.TryCreate( 0, out IArmor? armor );

        // Assert
        Assert.True( result );
        Assert.IsType<NoArmor>( armor );
        Assert.Equal( 100, budget.RemainingPoints );
    }

    [Fact]
    public void ArmorFactory_TryCreate_NotEnoughPoints_ReturnsFalse()
    {
        // Arrange
        var budget = new SharedPointsBudget( 0 );
        var catalog = new ArmorCatalog();
        var factory = new ArmorFactory( catalog );
        factory.SetBudget( budget );

        // Act
        bool result = factory.TryCreate( 1, out IArmor? armor );

        // Assert
        Assert.False( result );
    }
}

using Fighters.Factories;
using Fighters.Models.ItemCatalog;
using Fighters.Models.PointsBudget;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Factories;

public class WeaponFactoryTests
{
    [Fact]
    public void TryCreate_EnoughPoints_CreatesAndDeducts()
    {
        // Arrange
        var budget = new SharedPointsBudget( 100 );
        var catalog = new WeaponCatalog();
        var factory = new WeaponFactory( catalog );
        factory.SetBudget( budget );

        // Act
        bool result = factory.TryCreate( 0, out IWeapon? weapon );

        // Assert
        Assert.True( result );
        Assert.IsType<Fists>( weapon );
        Assert.Equal( 100, budget.RemainingPoints );
    }

    [Fact]
    public void TryCreate_NotEnoughPoints_ReturnsFalse()
    {
        // Arrange
        var budget = new SharedPointsBudget( 0 );
        var catalog = new WeaponCatalog();
        var factory = new WeaponFactory( catalog );
        factory.SetBudget( budget );

        // Act
        bool result = factory.TryCreate( 1, out IWeapon? weapon );

        // Assert
        Assert.False( result );
    }
}

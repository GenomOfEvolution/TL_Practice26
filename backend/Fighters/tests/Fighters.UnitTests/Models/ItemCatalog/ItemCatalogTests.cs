using Fighters.Models.Item;
using Fighters.Models.ItemCatalog;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.ItemCatalog;

public class ItemCatalogTests
{
    private static readonly TestItem _sword = new( "Sword", "A sharp blade", ItemRarity.Common );
    private static readonly TestItem _shield = new( "Shield", "A sturdy shield", ItemRarity.Uncommon );
    private static readonly TestItem _staff = new( "Staff", "A magical staff", ItemRarity.Rare );

    private static readonly TestCatalog<TestItem> _catalog = new(
        new CatalogEntry<TestItem>( _sword, 10 ),
        new CatalogEntry<TestItem>( _shield, 20 ),
        new CatalogEntry<TestItem>( _staff, 30 )
    );

    [Fact]
    public void GetAll_ThreeEntries_ReturnsAllEntries()
    {
        // Act
        var result = _catalog.GetAll();

        // Assert
        Assert.Equal( 3, result.Count );
    }

    [Fact]
    public void GetAvailable_WithBudget20_ReturnsItemsUnder20()
    {
        // Act
        var result = _catalog.GetAvailable( 20 );

        // Assert
        Assert.Equal( 2, result.Count );
        Assert.Contains( result, e => e.Item.Name == "Sword" );
        Assert.Contains( result, e => e.Item.Name == "Shield" );
    }

    [Fact]
    public void GetAvailable_ZeroPoints_ReturnsFreeItems()
    {
        // Arrange
        var catalog = new TestCatalog<TestItem>(
            new CatalogEntry<TestItem>( _sword, 0 ),
            new CatalogEntry<TestItem>( _shield, 5 )
        );

        // Act
        var result = catalog.GetAvailable( 0 );

        // Assert
        Assert.Single( result );
        Assert.Equal( "Sword", result[ 0 ].Item.Name );
    }

    [Fact]
    public void GetByIndex_ValidIndex_ReturnsCorrectEntry()
    {
        // Act
        var first = _catalog.GetByIndex( 0 );
        var last = _catalog.GetByIndex( 2 );

        // Assert
        Assert.Equal( "Sword", first.Item.Name );
        Assert.Equal( 10, first.Price );
        Assert.Equal( "Staff", last.Item.Name );
        Assert.Equal( 30, last.Price );
    }

    [Fact]
    public void GetByIndex_InvalidIndex_ThrowsArgumentOutOfRangeException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>( () => _catalog.GetByIndex( -1 ) );
        Assert.Throws<ArgumentOutOfRangeException>( () => _catalog.GetByIndex( 3 ) );
    }

    [Fact]
    public void CatalogEntry_Create_WithItemAndPrice_SetsProperties()
    {
        // Arrange
        var item = new TestItem( "Bow", "Ranged weapon", ItemRarity.Uncommon );

        // Act
        var entry = new CatalogEntry<TestItem>( item, 15 );

        // Assert
        Assert.Same( item, entry.Item );
        Assert.Equal( 15, entry.Price );
    }
}

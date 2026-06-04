using Fighters.Extensions;
using Fighters.Models.Damage;

namespace Fighters.UnitTests.Extensions;

public class DamageTypeExtensionsTests
{
    [Fact]
    public void GetTypeNameRu_AllDamageTypes_ReturnsRussianName()
    {
        // Act & Assert
        Assert.Equal( "физический", DamageType.Physic.GetTypeNameRu() );
        Assert.Equal( "магический", DamageType.Magic.GetTypeNameRu() );
        Assert.Equal( "чистый", DamageType.Pure.GetTypeNameRu() );
        Assert.Equal( "токсичный", DamageType.Poison.GetTypeNameRu() );
    }
}

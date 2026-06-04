using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Armors;

public class GlassArmorTests
{
    [Fact]
    public void ModifyWeaponDamage_BaseDamage_DoublesOutgoingDamage()
    {
        // Arrange
        var armor = new GlassArmor();
        IFighter fighter = FighterBuilder.CreateDefault();
        var baseDamage = new DamageStats
        {
            MinDamage = 50,
            MaxDamage = 100,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 100, result.MinDamage );
        Assert.Equal( 200, result.MaxDamage );
        Assert.Equal( baseDamage.Type, result.Type );
        Assert.Equal( baseDamage.CritChance, result.CritChance );
    }

    [Fact]
    public void ModifyIncomingDamage_BaseDamage_DoublesIncomingDamage()
    {
        // Arrange
        var armor = new GlassArmor();
        IFighter fighter = FighterBuilder.CreateDefault();
        var baseDamage = new DamageStats
        {
            MinDamage = 50,
            MaxDamage = 100,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyIncomingDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 100, result.MinDamage );
        Assert.Equal( 200, result.MaxDamage );
        Assert.Equal( baseDamage.Type, result.Type );
        Assert.Equal( baseDamage.CritChance, result.CritChance );
    }
}

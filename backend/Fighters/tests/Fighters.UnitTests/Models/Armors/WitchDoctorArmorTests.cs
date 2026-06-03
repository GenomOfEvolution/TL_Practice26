using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Armors;

public class WitchDoctorArmorTests
{
    private static IFighter Fighter => FighterBuilder.CreateDefault();

    [Fact]
    public void ModifyIncomingDamage_PhysicDamage_ConvertsToMagicAndReduces()
    {
        // Arrange
        var armor = new WitchDoctorArmor();
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyIncomingDamage( baseDamage, Fighter );

        // Assert
        Assert.Equal( 80, result.MinDamage );
        Assert.Equal( 160, result.MaxDamage );
        Assert.Equal( DamageType.Magic, result.Type );
        Assert.Equal( 0.425f, result.CritChance, 3 );
    }

    [Fact]
    public void ModifyIncomingDamage_PureDamage_ConvertsToMagicAndReduces()
    {
        // Arrange
        var armor = new WitchDoctorArmor();
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Pure,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyIncomingDamage( baseDamage, Fighter );

        // Assert
        Assert.Equal( 80, result.MinDamage );
        Assert.Equal( 160, result.MaxDamage );
        Assert.Equal( DamageType.Magic, result.Type );
    }

    [Fact]
    public void ModifyWeaponDamage_MagicDamage_BoostsByTwentyPercent()
    {
        // Arrange
        var armor = new WitchDoctorArmor();
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Magic,
            CritChance = 0.3f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, Fighter );

        // Assert
        Assert.Equal( 120, result.MinDamage );
        Assert.Equal( 240, result.MaxDamage );
        Assert.Equal( DamageType.Magic, result.Type );
        Assert.Equal( 0.345f, result.CritChance, 3 );
        Assert.Equal( 2.20f, result.CritDamage, 2 );
    }

    [Fact]
    public void ModifyWeaponDamage_PhysicDamage_ConvertsToMagicAndBoosts()
    {
        // Arrange
        var armor = new WitchDoctorArmor();
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, Fighter );

        // Assert
        Assert.Equal( 115, result.MinDamage );
        Assert.Equal( 230, result.MaxDamage );
        Assert.Equal( DamageType.Magic, result.Type );
    }
}

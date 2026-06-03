using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Models.Armors;

public class BerserkArmorTests
{
    private const int _maxHp = 780;

    private static SingleFighter CreateFighter( int currentHp )
    {
        var fighter = new SingleFighter(
            "Berserker",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new BerserkArmor(),
            new Fists()
        );

        int damageToTake = _maxHp - currentHp;
        if ( damageToTake > 0 )
        {
            fighter.TakeDamage( damageToTake );
        }

        return fighter;
    }

    [Fact]
    public void ModifyWeaponDamage_FullHp_NoDamageBoost()
    {
        // Arrange
        var armor = new BerserkArmor();
        var fighter = CreateFighter( _maxHp );
        var baseDamage = new DamageStats
        {
            MinDamage = 50,
            MaxDamage = 100,
            Type = DamageType.Physic,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 50, result.MinDamage );
        Assert.Equal( 100, result.MaxDamage );
    }

    [Fact]
    public void ModifyWeaponDamage_HalfHp_OneAndHalfMultiplier()
    {
        // Arrange
        var armor = new BerserkArmor();
        var fighter = CreateFighter( _maxHp / 2 );
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 150, result.MinDamage );
        Assert.Equal( 300, result.MaxDamage );
    }

    [Fact]
    public void ModifyWeaponDamage_NearDeath_DoubleDamage()
    {
        // Arrange
        var armor = new BerserkArmor();
        var fighter = CreateFighter( 1 );
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = armor.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 200, result.MinDamage );
        Assert.Equal( 400, result.MaxDamage );
    }
}

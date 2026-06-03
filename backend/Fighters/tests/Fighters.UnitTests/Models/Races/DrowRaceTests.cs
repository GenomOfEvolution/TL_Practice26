using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Models.Weapons.RangedWeapons;

namespace Fighters.UnitTests.Models.Races;

public class DrowRaceTests
{
    [Fact]
    public void ModifyWeaponDamage_RangedWeapon_AddsFiftyDamage()
    {
        // Arrange
        var race = new DrowRace();
        var fighter = new SingleFighter(
            "Drow",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            race,
            new NoSpeciality(),
            new NoArmor(),
            new Brick()
        );
        var baseDamage = new DamageStats
        {
            MinDamage = 85,
            MaxDamage = 175,
            Type = DamageType.Physic,
            CritChance = 0.3f,
            CritDamage = 1.4f,
        };

        // Act
        var result = race.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 135, result.MinDamage );
        Assert.Equal( 225, result.MaxDamage );
    }

    [Fact]
    public void ModifyWeaponDamage_MeleeWeapon_DoesNotAddBonus()
    {
        // Arrange
        var race = new DrowRace();
        var fighter = new SingleFighter(
            "Drow",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            race,
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var baseDamage = new DamageStats
        {
            MinDamage = 10,
            MaxDamage = 20,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = race.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 10, result.MinDamage );
        Assert.Equal( 20, result.MaxDamage );
    }
}

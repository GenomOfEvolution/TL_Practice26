using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Models.Races;

public class GnomeRaceTests
{
    [Fact]
    public void ModifyIncomingDamage_PoisonDamage_ReducesByPercent()
    {
        // Arrange
        var race = new GnomeRace();
        var fighter = new SingleFighter(
            "Gnome",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            race,
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var baseDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Poison,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = race.ModifyIncomingDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 85, result.MinDamage );
        Assert.Equal( 170, result.MaxDamage );
    }

    [Fact]
    public void ModifyWeaponDamage_MeleeWeapon_BoostsDamage()
    {
        // Arrange
        var race = new GnomeRace();
        var fighter = new SingleFighter(
            "Gnome",
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
        Assert.Equal( 11, result.MinDamage );
        Assert.Equal( 23, result.MaxDamage );
    }
}

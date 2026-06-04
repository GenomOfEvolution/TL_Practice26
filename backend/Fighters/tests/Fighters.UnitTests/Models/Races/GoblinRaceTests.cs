using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Models.Races;

public class GoblinRaceTests
{
    [Fact]
    public void ModifyWeaponDamage_CommonItems_AppliesNegativeModifier()
    {
        // Arrange
        var race = new GoblinRace();
        var fighter = new SingleFighter(
            "Goblin",
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
        Assert.Equal( 1, result.MinDamage );
        Assert.Equal( 10, result.MaxDamage );
    }

    [Fact]
    public void ModifyWeaponDamage_LegendaryGear_BoostsDamage()
    {
        // Arrange
        var race = new GoblinRace();
        var fighter = new SingleFighter(
            "Goblin",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            race,
            new NoSpeciality(),
            new BerserkArmor(),
            new MonoBlade()
        );
        var baseDamage = new DamageStats
        {
            MinDamage = 250,
            MaxDamage = 500,
            Type = DamageType.Pure,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = race.ModifyWeaponDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 280, result.MinDamage );
        Assert.Equal( 530, result.MaxDamage );
    }
}

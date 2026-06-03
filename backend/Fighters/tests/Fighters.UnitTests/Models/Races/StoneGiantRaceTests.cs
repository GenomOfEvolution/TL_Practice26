using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Models.Races;

public class StoneGiantRaceTests
{
    [Fact]
    public void ModifyIncomingDamage_PhysicDamage_ReducesByPercent()
    {
        // Arrange
        var race = new StoneGiantRace();
        var fighter = new SingleFighter(
            "StoneGiant",
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
            Type = DamageType.Physic,
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
    public void ModifyIncomingDamage_NonPhysicDamage_DoesNotReduce()
    {
        // Arrange
        var race = new StoneGiantRace();
        var fighter = new SingleFighter(
            "StoneGiant",
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
            Type = DamageType.Magic,
            CritChance = 0.0f,
            CritDamage = 1.0f,
        };

        // Act
        var result = race.ModifyIncomingDamage( baseDamage, fighter );

        // Assert
        Assert.Equal( 100, result.MinDamage );
        Assert.Equal( 200, result.MaxDamage );
    }
}

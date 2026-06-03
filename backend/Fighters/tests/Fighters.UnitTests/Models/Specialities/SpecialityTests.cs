using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Specialities;

public class SpecialityTests
{
    private readonly List<IFighter> _candidates;

    public SpecialityTests()
    {
        var damaged = FighterBuilder.CreateDefault( "DamagedFighter" );
        damaged.TakeDamage( 100 );

        _candidates =
        [
            FighterBuilder.CreateDefault( "FirstFighter" ),
            damaged,
            new SingleFighter(
                "HealthiestFighter",
                new FighterStats { Strength = 50, Dexterity = 0, Intelligence = 0 },
                new HumanRace(),
                new NoSpeciality(),
                new NoArmor(),
                new Fists()
            ),
            FighterBuilder.CreateDefault( "LastFighter" )
        ];
    }

    [Fact]
    public void Speciality_NoSpeciality_SelectsFirst()
    {
        // Arrange
        var speciality = new NoSpeciality();

        // Act
        var target = speciality.SelectTarget( _candidates );

        // Assert
        Assert.Equal( "FirstFighter", target.Name );
    }

    [Fact]
    public void Speciality_Warrior_SelectsHighestMaxHp()
    {
        // Arrange
        var speciality = new Warrior();

        // Act
        var target = speciality.SelectTarget( _candidates );

        // Assert
        Assert.Equal( "HealthiestFighter", target.Name );
    }

    [Fact]
    public void Speciality_Ranger_SelectsLast()
    {
        // Arrange
        var speciality = new Ranger();

        // Act
        var target = speciality.SelectTarget( _candidates );

        // Assert
        Assert.Equal( "LastFighter", target.Name );
    }

    [Fact]
    public void Speciality_Assassin_SelectsLowestCurrentHp()
    {
        // Arrange
        var speciality = new Assassin();

        // Act
        var target = speciality.SelectTarget( _candidates );

        // Assert
        Assert.Equal( "DamagedFighter", target.Name );
    }
}

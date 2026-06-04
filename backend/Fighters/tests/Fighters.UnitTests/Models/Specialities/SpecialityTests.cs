using Fighters.Models.Fighters;
using Fighters.Models.Specialities;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Specialities;

public class SpecialityTests
{
    private readonly List<IFighter> _candidates;

    public SpecialityTests()
    {
        _candidates = SetupCandidates();
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

    private static List<IFighter> SetupCandidates()
    {
        var defaultFighterStats = new FighterStats { Dexterity = 10, Intelligence = 10, Strength = 10 };
        var healthiestStats = new FighterStats { Dexterity = 0, Intelligence = 0, Strength = 50 };

        var damagedFighter = FighterBuilder.CreateWithStats( defaultFighterStats, "DamagedFighter" );
        damagedFighter.TakeDamage( 100 );

        return
        [
            FighterBuilder.CreateWithStats( defaultFighterStats, "FirstFighter" ),
            damagedFighter,
            FighterBuilder.CreateWithStats( healthiestStats, "HealthiestFighter" ),
            FighterBuilder.CreateWithStats( defaultFighterStats, "LastFighter" )
        ];
    }
}

using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.ItemCatalog;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Factories;

[Collection( "Console tests" )]
public class FighterFactoryTests
{
    [Fact]
    public void FighterFactory_CreateFighter_ReturnsValidFighter()
    {
        // Arrange
        var raceFactory = new RaceFactory();
        var specialityFactory = new SpecialityFactory();
        var statFactory = new FighterStatFactory();
        var weaponFactory = new WeaponFactory( new WeaponCatalog() );
        var armorFactory = new ArmorFactory( new ArmorCatalog() );
        var fighterFactory = new FighterFactory( raceFactory, specialityFactory, statFactory, weaponFactory, armorFactory, 100 );

        Console.SetIn( new StringReader(
            "TestFighter\n" +
            "0\n" +
            "0\n" +
            "10\n" +
            "10\n" +
            "10\n" +
            "0\n" +
            "0\n"
        ) );

        // Act
        IFighter fighter = fighterFactory.CreateFighter();

        // Assert
        Assert.NotNull( fighter );
        Assert.Equal( "TestFighter", fighter.Name );
        Assert.IsType<HumanRace>( fighter.Race );
        Assert.IsType<NoSpeciality>( fighter.Speciality );
        Assert.IsType<Fists>( fighter.EquippedWeapon );
        Assert.IsType<NoArmor>( fighter.EquippedArmor );
    }

    [Fact]
    public void FighterFactory_CreateFighterTeam_ReturnsTeamWithCorrectCount()
    {
        // Arrange
        var raceFactory = new RaceFactory();
        var specialityFactory = new SpecialityFactory();
        var statFactory = new FighterStatFactory();
        var weaponFactory = new WeaponFactory( new WeaponCatalog() );
        var armorFactory = new ArmorFactory( new ArmorCatalog() );
        var fighterFactory = new FighterFactory( raceFactory, specialityFactory, statFactory, weaponFactory, armorFactory, 100 );

        Console.SetIn( new StringReader(
            "F1\n0\n0\n10\n10\n10\n0\n0\n" +
            "F2\n0\n0\n10\n10\n10\n0\n0\n"
        ) );

        // Act
        IFighterTeam team = fighterFactory.CreateFighterTeam( 2 );

        // Assert
        Assert.NotNull( team );
        Assert.Equal( 2, team.GetMembers().Count() );
    }
}

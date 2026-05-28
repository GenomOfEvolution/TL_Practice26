using Fighters.Factories;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.ItemCatalog;
using Fighters.Models.PointsBudget;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.UnitTests.Factories;

public class FactoryTests
{
    [Fact]
    public void RaceFactory_Create_ReturnsCorrectRace()
    {
        var factory = new RaceFactory();

        Assert.IsType<HumanRace>( factory.Create( 0 ) );
        Assert.IsType<DrowRace>( factory.Create( 1 ) );
        Assert.IsType<GnomeRace>( factory.Create( 2 ) );
        Assert.IsType<GoblinRace>( factory.Create( 3 ) );
        Assert.IsType<StoneGiantRace>( factory.Create( 4 ) );
    }

    [Fact]
    public void RaceFactory_Create_InvalidChoice_Throws()
    {
        var factory = new RaceFactory();
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( -1 ) );
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( 5 ) );
    }

    [Fact]
    public void SpecialityFactory_Create_ReturnsCorrectSpeciality()
    {
        var factory = new SpecialityFactory();

        Assert.IsType<NoSpeciality>( factory.Create( 0 ) );
        Assert.IsType<Warrior>( factory.Create( 1 ) );
        Assert.IsType<Ranger>( factory.Create( 2 ) );
        Assert.IsType<Assassin>( factory.Create( 3 ) );
    }

    [Fact]
    public void SpecialityFactory_Create_InvalidChoice_Throws()
    {
        var factory = new SpecialityFactory();
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( -1 ) );
        Assert.Throws<ArgumentOutOfRangeException>( () => factory.Create( 4 ) );
    }

    [Fact]
    public void WeaponFactory_TryCreate_EnoughPoints_CreatesAndDeducts()
    {
        var budget = new SharedPointsBudget( 100 );
        var catalog = new WeaponCatalog();
        var factory = new WeaponFactory( catalog );
        factory.SetBudget( budget );

        bool result = factory.TryCreate( 0, out IWeapon? weapon );

        Assert.True( result );
        Assert.IsType<Fists>( weapon );
        Assert.Equal( 100, budget.RemainingPoints );
    }

    [Fact]
    public void WeaponFactory_TryCreate_NotEnoughPoints_ReturnsFalse()
    {
        var budget = new SharedPointsBudget( 0 );
        var catalog = new WeaponCatalog();
        var factory = new WeaponFactory( catalog );
        factory.SetBudget( budget );

        bool result = factory.TryCreate( 1, out IWeapon? weapon );

        Assert.False( result );
    }

    [Fact]
    public void ArmorFactory_TryCreate_EnoughPoints_CreatesAndDeducts()
    {
        var budget = new SharedPointsBudget( 100 );
        var catalog = new ArmorCatalog();
        var factory = new ArmorFactory( catalog );
        factory.SetBudget( budget );

        bool result = factory.TryCreate( 0, out IArmor? armor );

        Assert.True( result );
        Assert.IsType<NoArmor>( armor );
        Assert.Equal( 100, budget.RemainingPoints );
    }

    [Fact]
    public void ArmorFactory_TryCreate_NotEnoughPoints_ReturnsFalse()
    {
        var budget = new SharedPointsBudget( 0 );
        var catalog = new ArmorCatalog();
        var factory = new ArmorFactory( catalog );
        factory.SetBudget( budget );

        bool result = factory.TryCreate( 1, out IArmor? armor );

        Assert.False( result );
    }

    [Fact]
    public void FighterStatFactory_TryCreate_CreatesStats()
    {
        var budget = new SharedPointsBudget( 30 );
        var factory = new FighterStatFactory();
        factory.SetBudget( budget );

        var input = new StringReader( "10\n10\n10\n" );
        var originalInput = Console.In;
        Console.SetIn( input );

        try
        {
            bool result = factory.TryCreate( 0, out FighterStats? stats );

            Assert.True( result );
            Assert.NotNull( stats );
            Assert.Equal( 10, stats.Strength );
            Assert.Equal( 10, stats.Dexterity );
            Assert.Equal( 10, stats.Intelligence );
            Assert.Equal( 0, budget.RemainingPoints );
        }
        finally
        {
            Console.SetIn( originalInput );
        }
    }

    [Fact]
    public void FighterFactory_CreateFighter_ReturnsValidFighter()
    {
        var raceFactory = new RaceFactory();
        var specialityFactory = new SpecialityFactory();
        var statFactory = new FighterStatFactory();
        var weaponFactory = new WeaponFactory( new WeaponCatalog() );
        var armorFactory = new ArmorFactory( new ArmorCatalog() );
        var fighterFactory = new FighterFactory( raceFactory, specialityFactory, statFactory, weaponFactory, armorFactory, 100 );

        var input = new StringReader(
            "TestFighter\n" +
            "0\n" +
            "0\n" +
            "10\n" +
            "10\n" +
            "10\n" +
            "0\n" +
            "0\n"
        );
        var originalInput = Console.In;
        Console.SetIn( input );

        try
        {
            IFighter fighter = fighterFactory.CreateFighter();

            Assert.NotNull( fighter );
            Assert.Equal( "TestFighter", fighter.Name );
            Assert.IsType<HumanRace>( fighter.Race );
            Assert.IsType<NoSpeciality>( fighter.Speciality );
            Assert.IsType<Fists>( fighter.EquippedWeapon );
            Assert.IsType<NoArmor>( fighter.EquippedArmor );
        }
        finally
        {
            Console.SetIn( originalInput );
        }
    }

    [Fact]
    public void FighterFactory_CreateFighterTeam_ReturnsTeamWithCorrectCount()
    {
        var raceFactory = new RaceFactory();
        var specialityFactory = new SpecialityFactory();
        var statFactory = new FighterStatFactory();
        var weaponFactory = new WeaponFactory( new WeaponCatalog() );
        var armorFactory = new ArmorFactory( new ArmorCatalog() );
        var fighterFactory = new FighterFactory( raceFactory, specialityFactory, statFactory, weaponFactory, armorFactory, 100 );

        var input = new StringReader(
            "F1\n0\n0\n10\n10\n10\n0\n0\n" +
            "F2\n0\n0\n10\n10\n10\n0\n0\n"
        );
        var originalInput = Console.In;
        Console.SetIn( input );

        try
        {
            IFighterTeam team = fighterFactory.CreateFighterTeam( 2 );

            Assert.NotNull( team );
            Assert.Equal( 2, team.GetMembers().Count() );
        }
        finally
        {
            Console.SetIn( originalInput );
        }
    }
}

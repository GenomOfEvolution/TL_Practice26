using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using TestLibrary;

namespace Fighters.UnitTests.Models.Fighters;

public class SingleFighterTests
{
    private const int _strHealthMult = 25;
    private const int _dexHealthMult = 20;
    private const int _intHealthMult = 15;

    [Fact]
    public void SingleFighter_Create_PropertiesInitialized()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault( "Aragorn" );

        // Assert
        Assert.Equal( "Aragorn", fighter.Name );
        Assert.Equal( 10, fighter.Stats.Strength );
        Assert.Equal( 10, fighter.Stats.Dexterity );
        Assert.Equal( 10, fighter.Stats.Intelligence );
        Assert.IsType<HumanRace>( fighter.Race );
        Assert.IsType<NoSpeciality>( fighter.Speciality );
        Assert.IsType<NoArmor>( fighter.EquippedArmor );
        Assert.IsType<Fists>( fighter.EquippedWeapon );
    }

    [Fact]
    public void SingleFighter_EmptyName_ReturnsDefault()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault( "" );

        // Assert
        Assert.Equal( "Безымянный боец", fighter.Name );
    }

    [Theory]
    [InlineData( 10, 10, 10 )]
    [InlineData( 0, 0, 0 )]
    [InlineData( 5, 15, 20 )]
    public void SingleFighter_GetMaxHealth_CalculatesCorrectly( int str, int dex, int intelligence )
    {
        // Arrange
        var humanBonus = new HumanRace().GetStatBonus();
        int strPart = Math.Max( 0, str + humanBonus.Strength ) * _strHealthMult;
        int dexPart = Math.Max( 0, dex + humanBonus.Dexterity ) * _dexHealthMult;
        int intPart = Math.Max( 0, intelligence + humanBonus.Intelligence ) * _intHealthMult;
        int expected = strPart + dexPart + intPart;

        var fighter = new SingleFighter(
            "Test",
            new FighterStats { Strength = str, Dexterity = dex, Intelligence = intelligence },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );

        // Act
        int actual = fighter.GetMaxHealth();

        // Assert
        Assert.Equal( expected, actual );
    }

    [Fact]
    public void SingleFighter_GetCurrentHealth_EqualsMaxHealthOnCreation()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();

        // Act
        int current = fighter.GetCurrentHealth();
        int max = fighter.GetMaxHealth();

        // Assert
        Assert.Equal( max, current );
    }

    [Fact]
    public void SingleFighter_TakeDamage_ReducesHealth()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();
        int initialHealth = fighter.GetCurrentHealth();

        // Act
        fighter.TakeDamage( 50 );

        // Assert
        Assert.Equal( initialHealth - 50, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void SingleFighter_TakeDamage_ClampsAtZero()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();

        // Act
        fighter.TakeDamage( 999999 );

        // Assert
        Assert.Equal( 0, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void SingleFighter_SetArmor_UpdatesArmor()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();
        var newArmor = new NoArmor();

        // Act
        fighter.SetArmor( newArmor );

        // Assert
        Assert.Same( newArmor, fighter.EquippedArmor );
    }

    [Fact]
    public void SingleFighter_SetWeapon_UpdatesWeapon()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();
        var newWeapon = new Fists();

        // Act
        fighter.SetWeapon( newWeapon );

        // Assert
        Assert.Same( newWeapon, fighter.EquippedWeapon );
    }
}

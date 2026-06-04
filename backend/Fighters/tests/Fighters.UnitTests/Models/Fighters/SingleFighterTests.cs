using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.UnitTests.TestHelpers;

namespace Fighters.UnitTests.Models.Fighters;

public class SingleFighterTests
{
    private const int StrenghtHealthMult = 25;
    private const int DexterityHealthMult = 20;
    private const int IntelligenceHealthMult = 15;

    [Fact]
    public void SingleFighter_Create_PropertiesInitialized()
    {
        // Arrange
        var stats = new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 };
        SingleFighter fighter = FighterBuilder.CreateWithStats( stats, "Aragorn" );

        // Assert
        Assert.Equal( "Aragorn", fighter.Name );
        Assert.Equal( stats, fighter.Stats );
        Assert.IsType<HumanRace>( fighter.Race );
        Assert.IsType<NoSpeciality>( fighter.Speciality );
        Assert.IsType<NoArmor>( fighter.EquippedArmor );
        Assert.IsType<Fists>( fighter.EquippedWeapon );
    }

    [Fact]
    public void SingleFighter_EmptyName_ReturnsDefault()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault( String.Empty );

        // Assert
        Assert.Equal( "Безымянный боец", fighter.Name );
    }

    [Theory]
    [InlineData( 10, 10, 10 )]
    [InlineData( 0, 0, 0 )]
    [InlineData( 5, 15, 20 )]
    public void SingleFighter_GetMaxHealth_CalculatesCorrectly( int strength, int dexterity, int intelligence )
    {
        // Arrange
        var humanBonus = new HumanRace().GetStatBonus();
        int strPart = Math.Max( 0, strength + humanBonus.Strength ) * StrenghtHealthMult;
        int dexPart = Math.Max( 0, dexterity + humanBonus.Dexterity ) * DexterityHealthMult;
        int intPart = Math.Max( 0, intelligence + humanBonus.Intelligence ) * IntelligenceHealthMult;
        int expected = strPart + dexPart + intPart;

        var fighter = new SingleFighter(
            "Test",
            new FighterStats { Strength = strength, Dexterity = dexterity, Intelligence = intelligence },
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
        int damageToTake = ( int )( fighter.GetMaxHealth() * 0.5 );
        int initialHealth = fighter.GetCurrentHealth();

        // Act
        fighter.TakeDamage( damageToTake );

        // Assert
        Assert.Equal( initialHealth - damageToTake, fighter.GetCurrentHealth() );
    }

    [Fact]
    public void SingleFighter_TakeDamage_ClampsAtZero()
    {
        // Arrange
        SingleFighter fighter = FighterBuilder.CreateDefault();

        // Act
        fighter.TakeDamage( fighter.GetMaxHealth() + 1 );

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
        IWeapon newWeapon = new WoodenSword();
        IWeapon oldWeapon = fighter.EquippedWeapon;

        // Act
        fighter.SetWeapon( newWeapon );

        // Assert
        Assert.Same( newWeapon, fighter.EquippedWeapon );
        Assert.NotSame( oldWeapon, fighter.EquippedWeapon );
    }
}

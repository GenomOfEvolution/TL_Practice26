using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.Models.Weapons.RangedWeapons;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Models.Races;

public class RaceTests
{
    [Fact]
    public void Race_Goblin_ModifyWeaponDamage_RarityScaling()
    {
        // Arrange
        var goblin = new GoblinRace();
        var fighter = FighterBuilder.CreateDefault( "Goblin" );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = goblin.ModifyWeaponDamage( damage, fighter );

        // Assert
        Assert.Equal( 90, result.MinDamage );
        Assert.Equal( 190, result.MaxDamage );
    }

    [Fact]
    public void Race_Goblin_ModifyWeaponDamage_LegendaryGear_BoostsDamage()
    {
        // Arrange
        var goblin = new GoblinRace();
        var fighter = FighterBuilder.CreateDefault( "Goblin" );
        fighter.SetWeapon( new MonoBlade() );
        fighter.SetArmor( new BerserkArmor() );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = goblin.ModifyWeaponDamage( damage, fighter );

        // Assert
        Assert.Equal( 130, result.MinDamage );
        Assert.Equal( 230, result.MaxDamage );
    }

    [Fact]
    public void Race_Drow_ModifyWeaponDamage_RangedBonus()
    {
        // Arrange
        var drow = new DrowRace();
        var fighter = FighterBuilder.CreateDefault( "Drow" );
        fighter.SetWeapon( new Brick() );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = drow.ModifyWeaponDamage( damage, fighter );

        // Assert
        Assert.Equal( 150, result.MinDamage );
        Assert.Equal( 250, result.MaxDamage );
    }

    [Fact]
    public void Race_Gnome_ModifyIncomingDamage_PoisonResist()
    {
        // Arrange
        var gnome = new GnomeRace();
        var fighter = FighterBuilder.CreateDefault( "Gnome" );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Poison,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = gnome.ModifyIncomingDamage( damage, fighter );

        // Assert
        Assert.Equal( 85, result.MinDamage );
        Assert.Equal( 170, result.MaxDamage );
    }

    [Fact]
    public void Race_Gnome_ModifyWeaponDamage_MeleeBoost()
    {
        // Arrange
        var gnome = new GnomeRace();
        var fighter = FighterBuilder.CreateDefault( "Gnome" );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = gnome.ModifyWeaponDamage( damage, fighter );

        // Assert
        Assert.Equal( 115, result.MinDamage );
        Assert.Equal( 230, result.MaxDamage );
    }

    [Fact]
    public void Race_StoneGiant_ModifyIncomingDamage_PhysicResist()
    {
        // Arrange
        var stoneGiant = new StoneGiantRace();
        var fighter = FighterBuilder.CreateDefault( "StoneGiant" );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        // Act
        var result = stoneGiant.ModifyIncomingDamage( damage, fighter );

        // Assert
        Assert.Equal( 85, result.MinDamage );
        Assert.Equal( 170, result.MaxDamage );
    }
}

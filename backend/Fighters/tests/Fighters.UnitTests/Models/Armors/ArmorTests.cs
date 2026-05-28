using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons.MeleeWeapons;
using Fighters.TestLibrary;

namespace Fighters.UnitTests.Models.Armors;

public class ArmorTests
{
    [Fact]
    public void Armor_NoArmor_ResistancesEmpty()
    {
        var armor = new NoArmor();
        Assert.Empty( armor.Resistances );
    }

    [Fact]
    public void Armor_NoArmor_ModifyMethodsReturnSame()
    {
        var armor = new NoArmor();
        var fighter = FighterBuilder.CreateDefault();
        var damage = new DamageStats { MinDamage = 50, MaxDamage = 100 };

        var weaponResult = armor.ModifyWeaponDamage( damage, fighter );
        var incomingResult = armor.ModifyIncomingDamage( damage, fighter );

        Assert.Same( damage, weaponResult );
        Assert.Same( damage, incomingResult );
    }

    [Fact]
    public void Armor_LeatherArmor_Resistances_Physic10()
    {
        var armor = new LeatherArmor();
        Assert.Equal( 0.10f, armor.Resistances[ DamageType.Physic ] );
    }

    [Fact]
    public void Armor_KnightArmor_Resistances()
    {
        var armor = new KnightArmor();
        Assert.Equal( 0.40f, armor.Resistances[ DamageType.Physic ] );
        Assert.Equal( 0.10f, armor.Resistances[ DamageType.Poison ] );
        Assert.Equal( 0.05f, armor.Resistances[ DamageType.Magic ] );
    }

    [Fact]
    public void Armor_GlassArmor_DoublesDamage()
    {
        var armor = new GlassArmor();
        var fighter = FighterBuilder.CreateDefault();
        var damage = new DamageStats
        {
            MinDamage = 50,
            MaxDamage = 100,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        var weaponResult = armor.ModifyWeaponDamage( damage, fighter );
        Assert.Equal( 100, weaponResult.MinDamage );
        Assert.Equal( 200, weaponResult.MaxDamage );

        var incomingResult = armor.ModifyIncomingDamage( damage, fighter );
        Assert.Equal( 100, incomingResult.MinDamage );
        Assert.Equal( 200, incomingResult.MaxDamage );
    }

    [Fact]
    public void Armor_BerserkArmor_ScalesWithMissingHp()
    {
        var armor = new BerserkArmor();
        var fighter = new SingleFighter(
            "Berserker",
            new FighterStats { Strength = 10, Dexterity = 10, Intelligence = 10 },
            new HumanRace(),
            new NoSpeciality(),
            new NoArmor(),
            new Fists()
        );
        var damage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.1f,
            CritDamage = 2.0f,
        };

        var fullHpResult = armor.ModifyWeaponDamage( damage, fighter );
        Assert.Equal( 100, fullHpResult.MinDamage );
        Assert.Equal( 200, fullHpResult.MaxDamage );

        fighter.TakeDamage( ( int )( fighter.GetMaxHealth() * 0.5 ) );

        var halfHpResult = armor.ModifyWeaponDamage( damage, fighter );
        Assert.Equal( 150, halfHpResult.MinDamage );
        Assert.Equal( 300, halfHpResult.MaxDamage );
    }

    [Fact]
    public void Armor_WitchDoctorArmor_ModifyIncomingDamage()
    {
        var armor = new WitchDoctorArmor();
        var fighter = FighterBuilder.CreateDefault();

        var physicDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        var result = armor.ModifyIncomingDamage( physicDamage, fighter );
        Assert.Equal( DamageType.Magic, result.Type );
        Assert.Equal( 80, result.MinDamage );
        Assert.Equal( 160, result.MaxDamage );
        Assert.Equal( 0.425f, result.CritChance );
    }

    [Fact]
    public void Armor_WitchDoctorArmor_ModifyWeaponDamage()
    {
        var armor = new WitchDoctorArmor();
        var fighter = FighterBuilder.CreateDefault();

        var magicDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Magic,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        var result = armor.ModifyWeaponDamage( magicDamage, fighter );
        Assert.Equal( DamageType.Magic, result.Type );
        Assert.Equal( 120, result.MinDamage );
        Assert.Equal( 240, result.MaxDamage );
        Assert.Equal( 0.575f, result.CritChance );
        Assert.Equal( 2.2f, result.CritDamage );
    }

    [Fact]
    public void Armor_WitchDoctorArmor_ModifyWeaponDamage_ConvertsPhysToMagic()
    {
        var armor = new WitchDoctorArmor();
        var fighter = FighterBuilder.CreateDefault();

        var physDamage = new DamageStats
        {
            MinDamage = 100,
            MaxDamage = 200,
            Type = DamageType.Physic,
            CritChance = 0.5f,
            CritDamage = 2.0f,
        };

        var result = armor.ModifyWeaponDamage( physDamage, fighter );
        Assert.Equal( DamageType.Magic, result.Type );
        Assert.Equal( 115, result.MinDamage );
        Assert.Equal( 230, result.MaxDamage );
    }
}

using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public class GnomeRace : IRace
{
    private const float _poisonDamageReduction = 0.85f;
    private const float _meleeDamageReduction = 1.15f;

    public string Name => "Гном";
    public string Description => "гномы сродни дворфам, но значительно меньше по размеру своих дальних родственников";

    public int GetInitiativeModifier()
    {
        return 1;
    }

    public FighterStats GetStatBonus() => new()
    {
        Strength = 4,
        Dexterity = 0,
        Intelligence = 2
    };

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder )
    {
        DamageStats modifiedDamage = baseDamage;
        if ( baseDamage.Type == DamageType.Poison )
        {
            modifiedDamage.MinDamage = ( int )( baseDamage.MinDamage * _poisonDamageReduction );
            modifiedDamage.MaxDamage = ( int )( baseDamage.MaxDamage * _poisonDamageReduction );
        }

        return modifiedDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        DamageStats modifiedDamage = baseDamage;
        if ( wielder.EquippedWeapon.WeaponType == Weapons.WeaponType.Melee )
        {
            modifiedDamage.MinDamage = ( int )( baseDamage.MinDamage * _meleeDamageReduction );
            modifiedDamage.MaxDamage = ( int )( baseDamage.MaxDamage * _meleeDamageReduction );
        }

        return modifiedDamage;
    }
}

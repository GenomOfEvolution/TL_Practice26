using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Weapons;
using Fighters.Services.RandomService;

namespace Fighters.Services.DamageService;

public class DamageService : IDamageService
{
    private const double _weaponStatBonus = 0.05d;
    private const double _weaponStatBonusOverCap = 0.005d;
    private const double _strengthExtraBonus = 1.01d;
    private const float _intCritChanceBonus = 0.025f;

    private readonly IRandomService _randomService;

    public DamageService( IRandomService randomService )
    {
        _randomService = randomService;
    }

    public int CalculateReceivedDamage( DamageStats incomingDamage, IFighter target )
    {
        DamageStats effectiveDamage = ApplyDamageModifiers( incomingDamage, target );
        float resistance = target.EquippedArmor.Resistances.GetValueOrDefault( effectiveDamage.Type, 0f );
        float damageMultiplier = 1f - resistance;

        int minDmg = Math.Max( 1, ( int )( effectiveDamage.MinDamage * damageMultiplier ) );
        int maxDmg = Math.Max( 1, ( int )( effectiveDamage.MaxDamage * damageMultiplier ) );

        return _randomService.Next( minDmg, maxDmg + 1 );
    }

    public DamageStats CalculateAttackDamage( IFighter attacker )
    {
        DamageStats weaponDamage = CalculateWeaponDamage( attacker.EquippedWeapon, attacker );
        return TryApplyCrit( weaponDamage, attacker );
    }

    private static DamageStats ApplyDamageModifiers( DamageStats incomingDamage, IFighter target )
    {
        DamageStats armorModified = target.EquippedArmor.ModifyIncomingDamage( incomingDamage, target );

        return target.Race.ModifyIncomingDamage( armorModified, target );
    }

    private static double CalculateStatBonus( int fighterStat, int weaponStat )
    {
        return Math.Min( fighterStat, weaponStat ) * _weaponStatBonus
            + Math.Max( 0, fighterStat - weaponStat ) * _weaponStatBonusOverCap;
    }

    private DamageStats CalculateWeaponDamage( IWeapon weapon, IFighter itemHolder )
    {
        FighterStats raceBonus = itemHolder.Race.GetStatBonus();

        double strengthBonus = CalculateStatBonus( itemHolder.Stats.Strength + raceBonus.Strength, weapon.Stats.Strength );
        double dexterityBonus = CalculateStatBonus( itemHolder.Stats.Dexterity + raceBonus.Dexterity, weapon.Stats.Dexterity );
        double intelligenceBonus = CalculateStatBonus( itemHolder.Stats.Intelligence + raceBonus.Intelligence, weapon.Stats.Intelligence );

        double totalMultiplier = strengthBonus + dexterityBonus + intelligenceBonus;
        double extraStrengthMult = totalMultiplier + strengthBonus * _strengthExtraBonus;

        var baseDamage = new DamageStats
        {
            MinDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MinDamage * totalMultiplier ) ),
            MaxDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MaxDamage * extraStrengthMult ) ),
            Type = weapon.Damage.Type,
            CritChance = weapon.Damage.CritChance,
            CritDamage = weapon.Damage.CritDamage,
        };

        DamageStats raceModified = itemHolder.Race.ModifyWeaponDamage( baseDamage, itemHolder );
        return itemHolder.EquippedArmor.ModifyWeaponDamage( raceModified, itemHolder );
    }

    private DamageStats TryApplyCrit( DamageStats damage, IFighter attacker )
    {
        float critChanceTotal = damage.CritChance + attacker.Stats.Intelligence * _intCritChanceBonus;
        if ( critChanceTotal <= 0 )
        {
            return damage;
        }

        if ( _randomService.NextDouble() <= critChanceTotal )
        {
            return new DamageStats
            {
                MinDamage = ( int )( damage.MinDamage * damage.CritDamage ),
                MaxDamage = ( int )( damage.MaxDamage * damage.CritDamage ),
                Type = damage.Type,
                CritChance = damage.CritChance,
                CritDamage = damage.CritDamage
            };
        }

        return damage;
    }
}

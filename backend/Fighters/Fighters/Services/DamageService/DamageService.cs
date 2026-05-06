using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Weapons;
using Fighters.Services.RandomService;

namespace Fighters.Services.DamageService;

public class DamageService : IDamageService
{
    private const double WEAPON_STAT_BONUS = 0.05d;
    private const double WEAPON_STAT_BONUS_OVER_CAP = 0.005d;
    private IRandomService _randomService;

    public DamageService( IRandomService randomService )
    {
        _randomService = randomService;
    }

    public int CalculateReceivedDamage( DamageStats incomingDamage, IFighter target )
    {
        var modifiedDamage = target.EquippedArmor.ModifyIncomingDamage( incomingDamage, target );
        modifiedDamage = target.Race.ModifyIncomingDamage( modifiedDamage, target );

        float resistance = target.EquippedArmor.Resistances
            .GetValueOrDefault( modifiedDamage.Type, 0f );

        float damageMultiplier = 1f - resistance;

        int minDmg = ( int )( modifiedDamage.MinDamage * damageMultiplier );
        int maxDmg = ( int )( modifiedDamage.MaxDamage * damageMultiplier );

        minDmg = Math.Max( 1, minDmg );
        maxDmg = Math.Max( 1, maxDmg );

        return _randomService.Next( minDmg, maxDmg + 1 );
    }

    public DamageStats CalculateAttackDamage( IFighter attacker )
    {
        IWeapon specialityWeapon = attacker.Speciality.ModifyWeaponStats( attacker.EquippedWeapon, attacker );

        DamageStats weaponDamage = CalculateWeaponDamage( specialityWeapon, attacker );
        weaponDamage = attacker.Race.ModifyWeaponDamage( weaponDamage, attacker );
        weaponDamage = attacker.EquippedArmor.ModifyWeaponDamage( weaponDamage, attacker );

        return TryApplyCrit( weaponDamage );
    }

    private DamageStats TryApplyCrit( DamageStats damage )
    {
        if ( damage.CritChance <= 0 )
        {
            return damage;
        }

        if ( _randomService.NextDouble() <= damage.CritChance )
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

    private DamageStats CalculateWeaponDamage( IWeapon weapon, IFighter itemHolder )
    {
        FighterStats raceBonus = itemHolder.Race.GetStatBonus();

        double strengthBonus = CalculateStatBonus( itemHolder.Stats.Strength + raceBonus.Strength, weapon.Stats.Strength );
        double dexterityBonus = CalculateStatBonus( itemHolder.Stats.Dexterity + raceBonus.Dexterity, weapon.Stats.Dexterity );
        double intelligenceBonus = CalculateStatBonus( itemHolder.Stats.Intelligence + raceBonus.Intelligence, weapon.Stats.Intelligence );

        double totalMultiplier = strengthBonus + dexterityBonus + intelligenceBonus;

        return new DamageStats
        {
            MinDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MinDamage * totalMultiplier ) ),
            MaxDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MaxDamage * totalMultiplier ) ),
            Type = weapon.Damage.Type,
            CritChance = weapon.Damage.CritChance,
            CritDamage = weapon.Damage.CritDamage,
        };
    }

    private static double CalculateStatBonus( int fighterStat, int weaponStat )
    {
        return Math.Min( fighterStat, weaponStat ) * WEAPON_STAT_BONUS
            + Math.Max( 0, fighterStat - weaponStat ) * WEAPON_STAT_BONUS_OVER_CAP;
    }
}

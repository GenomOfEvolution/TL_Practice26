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
        DamageStats recievedDamage = incomingDamage;
        recievedDamage = target.EquippedArmor.ModifyIncomingDamage( recievedDamage, target );
        recievedDamage = target.Race.ModifyIncomingDamage( recievedDamage, target );

        return _randomService.Next( recievedDamage.MinDamage, recievedDamage.MaxDamage );
    }

    public DamageStats CalculateAttackDamage( IFighter attacker )
    {
        DamageStats weaponDamage = CalculateWeaponDamage( attacker.EquippedWeapon, attacker );
        weaponDamage = attacker.Race.ModifyWeaponDamage( weaponDamage, attacker );
        weaponDamage = attacker.EquippedArmor.ModifyWeaponDamage( weaponDamage, attacker );

        return weaponDamage;
    }

    private DamageStats CalculateWeaponDamage( IWeapon weapon, IFighter itemHolder )
    {
        double strengthBonus = CalculateStatBonus( itemHolder.Stats.Strength, weapon.Stats.Strength );
        double dexterityBonus = CalculateStatBonus( itemHolder.Stats.Dexterity, weapon.Stats.Dexterity );
        double intelligenceBonus = CalculateStatBonus( itemHolder.Stats.Intelligence, weapon.Stats.Intelligence );

        double totalMultiplier = strengthBonus + dexterityBonus + intelligenceBonus;

        return new DamageStats
        {
            MinDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MinDamage * totalMultiplier ) ),
            MaxDamage = ( int )Math.Max( 1, Math.Round( weapon.Damage.MaxDamage * totalMultiplier ) ),
            Type = weapon.Damage.Type
        };
    }

    private static double CalculateStatBonus( int fighterStat, int weaponStat )
    {
        return Math.Min( fighterStat, weaponStat ) * WEAPON_STAT_BONUS
            + Math.Max( 0, fighterStat - weaponStat ) * WEAPON_STAT_BONUS_OVER_CAP;
    }
}

using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Services.WeaponService;

public class WeaponService : IWeaponService
{
    private const double STAT_BONUS = 0.05d;
    private const double STAT_BONUS_OVER_CAP = 0.005d;
    public DamageStats CalculateWeaponDamage( IWeapon weapon, IFighter itemHolder )
    {
        double strengthBonus = CalculateStatBonus( itemHolder.Strength, weapon.Stats.Strength );
        double dexterityBonus = CalculateStatBonus( itemHolder.Dexterity, weapon.Stats.Dexterity );
        double intelligenceBonus = CalculateStatBonus( itemHolder.Intelligence, weapon.Stats.Intelligence );

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
        int overCapStat = fighterStat - weaponStat;

        return Math.Min( fighterStat, weaponStat ) * STAT_BONUS
            + Math.Max( 0, overCapStat ) * STAT_BONUS_OVER_CAP;
    }
}
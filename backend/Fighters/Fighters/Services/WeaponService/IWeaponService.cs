using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Services.WeaponService;

public interface IWeaponService
{
    DamageStats CalculateWeaponDamage( IWeapon weapon, IFighter itemHolder );
}

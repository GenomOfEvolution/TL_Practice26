using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Models.Specialities;

public class NoSpeciality : ISpeciality
{
    public IWeapon ModifyWeaponStats( IWeapon weapon, IFighter weaponHolder )
    {
        IWeapon modifiedWeapon = weapon;

        modifiedWeapon.Damage.MinDamage += 1;
        modifiedWeapon.Damage.MaxDamage += 1;

        return modifiedWeapon;
    }
}

using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Models.Specialities;

public interface ISpeciality
{
    /// <summary>
    /// Модифицирует базовые статы оружия
    /// </summary>
    IWeapon ModifyWeaponStats( IWeapon weapon, IFighter weaponHolder );
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons;

public interface IWeapon : IItem
{
    /// <summary>
    /// Софт кап статов оружия - чем больше, тем больше статов можно в него вложить
    /// </summary>
    ItemStats Stats { get; }

    /// <summary>
    /// Базовый урон оружия и его тип
    /// </summary>
    DamageStats Damage { get; }

    /// <summary>
    /// Тип оружия, может использоваться в расчете урона
    /// </summary>
    WeaponType WeaponType { get; }
}
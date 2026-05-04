using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons;

public enum WeaponType
{
    Melee,
    Ranged,
    Magic,
}

public interface IWeapon : IItem
{
    ItemStats Stats { get; set; }
    DamageStats Damage { get; set; }
    WeaponType WeaponType { get; set; }
}
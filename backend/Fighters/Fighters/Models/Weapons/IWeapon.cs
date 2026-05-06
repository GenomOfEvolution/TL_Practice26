using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons;

public interface IWeapon : IItem
{
    ItemStats Stats { get; }
    DamageStats Damage { get; }
    WeaponType WeaponType { get; }
}
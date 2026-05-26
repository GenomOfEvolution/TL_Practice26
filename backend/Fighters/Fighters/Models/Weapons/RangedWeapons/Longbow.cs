using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class Longbow : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 10,
        Dexterity = 10,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 115,
        MaxDamage = 240,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Длинный лук";

    public string Description => "Лук, размером с человека";

    public ItemRarity Rarity => ItemRarity.Common;
}

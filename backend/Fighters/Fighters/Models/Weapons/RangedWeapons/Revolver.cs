using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class Revolver : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 10,
        Intelligence = 5,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 85,
        MaxDamage = 175,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Револьвер";

    public string Description => "Джордж, ты ковбой!";

    public ItemRarity Rarity => ItemRarity.Common;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class RocketLauncher : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 40,
        Dexterity = 0,
        Intelligence = 40,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 120,
        MaxDamage = 315,
        Type = DamageType.Physic,
        CritChance = 0.0f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Ракетная установка";

    public string Description => "Стреляет залпом ракет";

    public ItemRarity Rarity => ItemRarity.Epic;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class WoodenSword : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 10,
        Dexterity = 0,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 65,
        MaxDamage = 140,
        Type = DamageType.Physic,
        CritChance = 0.15f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Дервянный меч";

    public string Description => "Такие мечи используют для тренировок";

    public ItemRarity Rarity => ItemRarity.Common;
}

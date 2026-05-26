using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class CrudeBow : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 10,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 100,
        MaxDamage = 200,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 1.4f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Грубый лук";

    public string Description => "Этот лук не идеален, но стрелять можно";

    public ItemRarity Rarity => ItemRarity.Common;
}

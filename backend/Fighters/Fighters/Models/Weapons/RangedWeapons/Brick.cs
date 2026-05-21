using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class Brick : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 10,
        Dexterity = 10,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 85,
        MaxDamage = 175,
        Type = DamageType.Physic,
        CritChance = 0.3f,
        CritDamage = 1.4f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Кирпич";

    public string Description => "Догони меня кирпич!";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

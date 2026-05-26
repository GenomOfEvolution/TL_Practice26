using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class GhostRifle : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 50,
        Intelligence = 30,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 335,
        MaxDamage = 735,
        Type = DamageType.Physic,
        CritChance = 0.2f,
        CritDamage = 5.0f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Винтовка \"Призрак\"";

    public string Description => "No Scope 360";

    public ItemRarity Rarity => ItemRarity.Legendary;
}

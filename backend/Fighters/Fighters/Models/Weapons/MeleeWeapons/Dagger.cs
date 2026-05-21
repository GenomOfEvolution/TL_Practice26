using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class Dagger : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 20,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 90,
        MaxDamage = 185,
        Type = DamageType.Physic,
        CritChance = 0.20f,
        CritDamage = 2.5f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Кинжал";

    public string Description => "Короткий кинжал, удобно лежит в руке";

    public ItemRarity Rarity => ItemRarity.Common;
}

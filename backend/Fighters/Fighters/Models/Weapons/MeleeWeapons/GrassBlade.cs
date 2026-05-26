using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class GrassBlade : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 30,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 60,
        MaxDamage = 120,
        Type = DamageType.Poison,
        CritChance = 0.1f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Травяной клинок";

    public string Description => "Клинок, полностью состоящий из травы";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

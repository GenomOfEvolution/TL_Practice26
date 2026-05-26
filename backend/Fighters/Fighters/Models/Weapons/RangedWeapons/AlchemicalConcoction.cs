using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class AlchemicalConcoction : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 5,
        Intelligence = 10,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 135,
        MaxDamage = 150,
        Type = DamageType.Poison,
        CritChance = 0.0f,
        CritDamage = 1.1f
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Алхимическая смесь";

    public string Description => "Небольшая баночка, наполненная кислотой";

    public ItemRarity Rarity => ItemRarity.Common;
}

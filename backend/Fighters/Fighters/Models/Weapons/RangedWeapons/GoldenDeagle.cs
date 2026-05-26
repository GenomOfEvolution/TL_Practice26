using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class GoldenDeagle : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 20,
        Intelligence = 10,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 100,
        MaxDamage = 215,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Золотой Desert Eagle";

    public string Description => "Получен от одного очень привлекательного человека";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

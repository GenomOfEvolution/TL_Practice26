using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class HuntingKnife : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 10,
        Dexterity = 20,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 110,
        MaxDamage = 235,
        Type = DamageType.Physic,
        CritChance = 0.25f,
        CritDamage = 3.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Охотничий нож";

    public string Description => "Нож, изготовленный Уральскими мастерами";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

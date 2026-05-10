using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class SlitherWand : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 5,
        Intelligence = 30,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 110,
        MaxDamage = 235,
        Type = DamageType.Poison,
        CritChance = 0.1f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Посох змей";

    public string Description => "Выпускает извивающийся снаряд в виде змеи";

    public ItemRarity Rarity => ItemRarity.Rare;
}

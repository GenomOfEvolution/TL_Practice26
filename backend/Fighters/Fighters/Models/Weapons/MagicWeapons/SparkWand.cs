using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class SparkWand : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 5,
        Intelligence = 20,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 5,
        MaxDamage = 150,
        Type = DamageType.Magic,
        CritChance = 0.2f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Жезл искр";

    public string Description => "Выпускает поток обжигающих искр";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class LeafWand : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 5,
        Intelligence = 10,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 55,
        MaxDamage = 110,
        Type = DamageType.Magic,
        CritChance = 0.3f,
        CritDamage = 1.2f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Лиственный жезл";

    public string Description => "Выпускает поток острых, как бритва, листьев";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

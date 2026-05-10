using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class ApprenticeWand : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 0,
        Intelligence = 10,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 55,
        MaxDamage = 115,
        Type = DamageType.Magic,
        CritChance = 0.05f,
        CritDamage = 1.2f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Палочка ученика";

    public string Description => "Ты волшебник, Гарри!";

    public ItemRarity Rarity => ItemRarity.Common;
}

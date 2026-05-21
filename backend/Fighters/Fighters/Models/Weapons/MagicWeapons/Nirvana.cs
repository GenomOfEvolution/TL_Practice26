using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class Nirvana : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 50,
        Intelligence = 50,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 100,
        MaxDamage = 170,
        Type = DamageType.Pure,
        CritChance = 0.25f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Нирвана";

    public string Description => "Мага, сияй! И он сиял...";

    public ItemRarity Rarity => ItemRarity.Legendary;
}

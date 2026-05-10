using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class MonoBlade : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 10,
        Dexterity = 50,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 250,
        MaxDamage = 500,
        Type = DamageType.Pure,
        CritChance = 0.0f,
        CritDamage = 1.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Катана атомного самурая";

    public string Description => "Заточка клинка до 1 атома, пробивающая любую броню";

    public ItemRarity Rarity => ItemRarity.Legendary;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class DragonBreath : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 40,
        Dexterity = 0,
        Intelligence = 40,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 100,
        MaxDamage = 100,
        Type = DamageType.Magic,
        CritChance = 0,
        CritDamage = 1.0f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Дыхание дракона";

    public string Description => "Вся мощь дракона сосредоточена в этой палочке";

    public ItemRarity Rarity => ItemRarity.Epic;
}

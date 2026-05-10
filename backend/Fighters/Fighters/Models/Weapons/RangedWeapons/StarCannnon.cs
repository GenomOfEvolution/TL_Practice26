using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class StarCannnon : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 20,
        Dexterity = 0,
        Intelligence = 30,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 105,
        MaxDamage = 280,
        Type = DamageType.Magic,
        CritChance = 0.0f,
        CritDamage = 1.1f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Звездная пушка";

    public string Description => "Стреляет маленькими звездами";

    public ItemRarity Rarity => ItemRarity.Rare;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MagicWeapons;

public class OakStaff : IWeapon
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
        MaxDamage = 215,
        Type = DamageType.Magic,
        CritChance = 0.05f,
        CritDamage = 1.2f,
    };

    public WeaponType WeaponType => WeaponType.Magic;

    public string Name => "Дубовый посох";

    public string Description => "Суковатая, слабо обработанная палка, в верхней части которой имеется специальная выемка для курительной трубки";

    public ItemRarity Rarity => ItemRarity.Common;
}

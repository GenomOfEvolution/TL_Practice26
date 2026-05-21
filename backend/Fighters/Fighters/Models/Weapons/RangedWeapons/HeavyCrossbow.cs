using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.RangedWeapons;

public class HeavyCrossbow : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 20,
        Dexterity = 10,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 135,
        MaxDamage = 285,
        Type = DamageType.Physic,
        CritChance = 0.03f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Ranged;

    public string Name => "Тяжелый арбалет";

    public string Description => "Стреляет увесистыми болтами";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

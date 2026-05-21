using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class BroadSword : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 20,
        Dexterity = 5,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 80,
        MaxDamage = 165,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Тесак";

    public string Description => "Меч с довольно широким лезвием";

    public ItemRarity Rarity => ItemRarity.Common;
}

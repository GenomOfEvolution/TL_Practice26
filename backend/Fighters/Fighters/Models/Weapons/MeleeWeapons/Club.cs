using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class Club : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 20,
        Dexterity = 0,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 105,
        MaxDamage = 215,
        Type = DamageType.Physic,
        CritChance = 0.05f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Дубина";

    public string Description => "Увесистая дубина, больше похожая на ветвь";

    public ItemRarity Rarity => ItemRarity.Common;
}

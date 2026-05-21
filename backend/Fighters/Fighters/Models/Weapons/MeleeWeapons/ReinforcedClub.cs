using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class ReinforcedClub : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 20,
        Dexterity = 0,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 145,
        MaxDamage = 285,
        Type = DamageType.Physic,
        CritChance = 0.05f,
        CritDamage = 2.5f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Усиленная дубина";

    public string Description => "Обычная ветвь усеянная гвоздями";

    public ItemRarity Rarity => ItemRarity.Uncommon;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class Fists : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 50,
        Dexterity = 50,
        Intelligence = 50
    };

    public DamageStats Damage => new()
    {
        MinDamage = 50,
        MaxDamage = 60,
        Type = DamageType.Physic,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Кулаки";

    public string Description => "Да я тебя одной левой!";

    public ItemRarity Rarity => ItemRarity.Common;
}

using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class TheSeparator : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 30,
        Dexterity = 10,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 75,
        MaxDamage = 165,
        Type = DamageType.Physic,
        CritChance = 0.8f,
        CritDamage = 2.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Расчленитель";

    public string Description => "Разделяй и властвуй";

    public ItemRarity Rarity => ItemRarity.Rare;
}

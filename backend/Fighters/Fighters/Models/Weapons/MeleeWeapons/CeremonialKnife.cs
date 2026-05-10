using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class CeremonialKnife : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 0,
        Dexterity = 20,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 205,
        MaxDamage = 230,
        Type = DamageType.Poison,
        CritChance = 0.3f,
        CritDamage = 1.5f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Церемониальный нож";

    public string Description => "Культ Акток использовал их в своих ритуалах...";

    public ItemRarity Rarity => ItemRarity.Rare;
}

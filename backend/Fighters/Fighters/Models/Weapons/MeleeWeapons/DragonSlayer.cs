using Fighters.Models.Damage;
using Fighters.Models.Item;

namespace Fighters.Models.Weapons.MeleeWeapons;

public class DragonSlayer : IWeapon
{
    public ItemStats Stats => new()
    {
        Strength = 50,
        Dexterity = 0,
        Intelligence = 0,
    };

    public DamageStats Damage => new()
    {
        MinDamage = 150,
        MaxDamage = 295,
        Type = DamageType.Physic,
        CritChance = 0.1f,
        CritDamage = 3.0f,
    };

    public WeaponType WeaponType => WeaponType.Melee;

    public string Name => "Убица драконов";

    public string Description => "Меч, больше похожий на кусок металла, принадлежавший легендарному мечнику";

    public ItemRarity Rarity => ItemRarity.Epic;
}

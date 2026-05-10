using System.Collections.ObjectModel;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class KnightArmor : IArmor
{
    private static readonly IReadOnlyDictionary<DamageType, float> _resistances =
        new ReadOnlyDictionary<DamageType, float>( new Dictionary<DamageType, float>
        {
            { DamageType.Physic, 0.60f },
            { DamageType.Magic,  0.95f },
            { DamageType.Pure,   1.00f },
            { DamageType.Poison, 0.90f }
        } );

    public IReadOnlyDictionary<DamageType, float> Resistances => _resistances;

    public string Name => "Рыцарский латный доспех";

    public string Description => "Тяжёлые стальные латы, выкованные для защиты от клинков и стрел. Отлично гасит физический урон, но сковывает движения.";

    public ItemRarity Rarity => ItemRarity.Uncommon;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }
}

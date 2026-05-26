using System.Collections.ObjectModel;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class LeatherArmor : IArmor
{
    private static readonly IReadOnlyDictionary<DamageType, float> _resistances =
        new ReadOnlyDictionary<DamageType, float>( new Dictionary<DamageType, float>
        {
            { DamageType.Physic, 0.10f },
            { DamageType.Magic,  0.00f },
            { DamageType.Pure,   0.00f },
            { DamageType.Poison, 0.00f }
        } );

    public IReadOnlyDictionary<DamageType, float> Resistances => _resistances;

    public string Name => "Кожаная броня";

    public string Description => "Простая защита из выделанной кожи";

    public ItemRarity Rarity => ItemRarity.Common;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }
}

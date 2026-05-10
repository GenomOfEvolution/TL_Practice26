using System.Collections.ObjectModel;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class GlassArmor : IArmor
{
    private static readonly IReadOnlyDictionary<DamageType, float> _resistances =
        new ReadOnlyDictionary<DamageType, float>( new Dictionary<DamageType, float>
        {
            { DamageType.Physic, 1.00f },
            { DamageType.Magic,  1.00f },
            { DamageType.Pure,   1.00f },
            { DamageType.Poison, 1.00f }
        } );

    public IReadOnlyDictionary<DamageType, float> Resistances => _resistances;

    public string Name => "Стеклянный Доспех";

    public string Description =>
        """
        Хрупкая конструкция из закалённого магического стекла. 
        Дарует невероятную пробивную силу, но делает владельца крайне уязвимым к любым атакам
        """;

    public ItemRarity Rarity => ItemRarity.Rare;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        return new DamageStats
        {
            MinDamage = ( int )Math.Round( baseDamage.MinDamage * 2.0f ),
            MaxDamage = ( int )Math.Round( baseDamage.MaxDamage * 2.0f ),
            Type = baseDamage.Type,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        return new DamageStats
        {
            MinDamage = ( int )Math.Round( baseDamage.MinDamage * 2.0f ),
            MaxDamage = ( int )Math.Round( baseDamage.MaxDamage * 2.0f ),
            Type = baseDamage.Type,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };
    }
}

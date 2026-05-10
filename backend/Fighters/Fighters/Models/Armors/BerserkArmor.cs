using System.Collections.ObjectModel;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class BerserkArmor : IArmor
{
    private static readonly IReadOnlyDictionary<DamageType, float> _resistances =
        new ReadOnlyDictionary<DamageType, float>( new Dictionary<DamageType, float>
        {
            { DamageType.Physic, 0.50f },
            { DamageType.Magic,  0.50f },
            { DamageType.Pure,   0.00f },
            { DamageType.Poison, 0.50f }
        } );

    public IReadOnlyDictionary<DamageType, float> Resistances => _resistances;

    public string Name => "Доспех Берсерка";

    public string Description =>
        """
        Выкованный из чёрного металла и скреплённый древней кровью.
        Пробуждает первобытную ярость: боль превращается в силу.
        Чем ближе владелец к смерти, тем сокрушительнее становятся его удары.
        """;

    public ItemRarity Rarity => ItemRarity.Legendary;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        int currentHp = wielder.GetCurrentHealth();
        int maxHp = wielder.GetMaxHealth();

        float missingHpRatio = maxHp > 0 ? 1f - ( ( float )currentHp / maxHp ) : 0f;
        float multiplier = 1f + missingHpRatio;

        multiplier = Math.Max( 1.0f, Math.Min( 2.0f, multiplier ) );

        return new DamageStats
        {
            MinDamage = ( int )Math.Round( baseDamage.MinDamage * multiplier ),
            MaxDamage = ( int )Math.Round( baseDamage.MaxDamage * multiplier ),
            Type = baseDamage.Type,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };
    }
}
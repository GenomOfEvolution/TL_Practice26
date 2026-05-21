using System.Collections.ObjectModel;
using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public class WitchDoctorArmor : IArmor
{
    private static readonly IReadOnlyDictionary<DamageType, float> _neutralResistances =
        new ReadOnlyDictionary<DamageType, float>( new Dictionary<DamageType, float>
        {
            { DamageType.Physic, 0.00f },
            { DamageType.Magic,  0.00f },
            { DamageType.Pure,   0.00f },
            { DamageType.Poison, 0.00f }
        } );

    public IReadOnlyDictionary<DamageType, float> Resistances => _neutralResistances;

    public string Name => "Ритуальные Одежды Ведьмака";

    public string Description =>
        """
        Обрядовые ткани, пропитанные травами и кровью духов. 
        Поглощают грубую силу атак, перенаправляя её в чистую магию, 
        а удары владельца наполняются ядовитой энергией Актока.
        """;

    public ItemRarity Rarity => ItemRarity.Epic;

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        var result = new DamageStats
        {
            Type = baseDamage.Type,
            MinDamage = baseDamage.MinDamage,
            MaxDamage = baseDamage.MaxDamage,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };

        const float damageReduction = 0.80f;
        const float critReduction = 0.85f;

        result.MinDamage = ( int )Math.Round( result.MinDamage * damageReduction );
        result.MaxDamage = ( int )Math.Round( result.MaxDamage * damageReduction );

        if ( result.Type == DamageType.Physic || result.Type == DamageType.Pure )
        {
            result.Type = DamageType.Magic;
        }

        result.CritChance = Math.Max( 0f, result.CritChance * critReduction );

        return result;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        var result = new DamageStats
        {
            Type = baseDamage.Type,
            MinDamage = baseDamage.MinDamage,
            MaxDamage = baseDamage.MaxDamage,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };

        const float magicPoisonMultiplier = 1.20f;
        const float physConvertMultiplier = 1.15f;

        if ( result.Type == DamageType.Magic || result.Type == DamageType.Poison )
        {
            result.MinDamage = ( int )Math.Round( result.MinDamage * magicPoisonMultiplier );
            result.MaxDamage = ( int )Math.Round( result.MaxDamage * magicPoisonMultiplier );
        }

        if ( result.Type == DamageType.Physic || result.Type == DamageType.Pure )
        {
            result.Type = DamageType.Magic;
            result.MinDamage = ( int )Math.Round( result.MinDamage * physConvertMultiplier );
            result.MaxDamage = ( int )Math.Round( result.MaxDamage * physConvertMultiplier );
        }

        result.CritChance = Math.Min( 1.0f, result.CritChance * 1.15f );
        result.CritDamage = Math.Max( 1.5f, result.CritDamage * 1.10f );

        return result;
    }
}

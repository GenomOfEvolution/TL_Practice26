using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public interface IArmor : IItem
{
    /// <summary>
    /// Сопротивления какому-либу типу урона
    /// </summary>
    IReadOnlyDictionary<DamageType, float> Resistances { get; }

    /// <summary>
    /// Применяет уникальные бонусы брони по какому-либо принципу/условию
    /// </summary>
    DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder );

    /// <summary>
    /// Применяет уникальные эффекты брони при получении урона
    /// </summary>
    DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder );
}
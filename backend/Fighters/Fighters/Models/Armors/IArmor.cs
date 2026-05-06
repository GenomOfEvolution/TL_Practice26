using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public interface IArmor : IItem
{
    IReadOnlyDictionary<DamageType, float> Resistances { get; }
    DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder );
    DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder );
}
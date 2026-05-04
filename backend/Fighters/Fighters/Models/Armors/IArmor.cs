using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Armors;

public interface IArmor : IItem
{
    DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder ) => baseDamage;
    DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder ) => baseDamage;
}
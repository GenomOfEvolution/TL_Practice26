using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public interface IRace
{
    string Name { get; }

    int GetStrengthBonus() => 0;
    int GetIntelegenceBonus() => 0;
    int GetDexterityBonus() => 0;

    DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder ) => baseDamage;
    DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder ) => baseDamage;

    int GetInitiativeModifier() => 0;
}
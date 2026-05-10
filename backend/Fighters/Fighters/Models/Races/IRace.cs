using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public interface IRace
{
    string Name { get; }
    string Description { get; }

    FighterStats GetStatBonus();

    DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder );
    DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder );

    int GetInitiativeModifier();
}
using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public class StoneGiantRace : IRace
{
    private const float PHYS_DAMAGE_REDUCTION = 0.85f;

    public string Name => "Каменный гигант";

    public string Description => "медлительные но крепкие каменные великаны";

    public int GetInitiativeModifier()
    {
        return -3;
    }

    public FighterStats GetStatBonus() => new()
    {
        Strength = 5,
        Dexterity = -5,
        Intelligence = 0,
    };

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder )
    {
        if ( baseDamage.Type == DamageType.Physic )
        {
            return new DamageStats
            {
                MinDamage = ( int )( PHYS_DAMAGE_REDUCTION * baseDamage.MinDamage ),
                MaxDamage = ( int )( PHYS_DAMAGE_REDUCTION * baseDamage.MaxDamage ),
                Type = baseDamage.Type,
                CritChance = baseDamage.CritChance,
                CritDamage = baseDamage.CritDamage,
            };
        }

        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }
}

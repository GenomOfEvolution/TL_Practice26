using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public class DrowRace : IRace
{
    private const int RANGED_DAMAGE_BONUS = 50;

    public string Name => "Дроу (Тёмный эльф)";
    public string Description => "обучены воевать с оружием дальнего боя";

    public int GetInitiativeModifier()
    {
        return 4;
    }

    public FighterStats GetStatBonus() => new()
    {
        Strength = -1,
        Dexterity = 5,
        Intelligence = 1
    };

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        DamageStats modfiedDamage = baseDamage;
        if ( wielder.EquippedWeapon.WeaponType == Weapons.WeaponType.Ranged )
        {
            modfiedDamage.MinDamage += RANGED_DAMAGE_BONUS;
            modfiedDamage.MaxDamage += RANGED_DAMAGE_BONUS;
        }

        return modfiedDamage;
    }
}

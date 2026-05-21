using Fighters.Models.Damage;
using Fighters.Models.Fighters;
using Fighters.Models.Item;

namespace Fighters.Models.Races;

public class GoblinRace : IRace
{
    private const int BaselineRarity = ( int )ItemRarity.Uncommon * 2;
    private const int DamageModifierPerRarityTier = 5;

    public string Name => "Гоблин";
    public string Description => "увеличивает/уменьшает урон от редкости своих предметов";

    public int GetInitiativeModifier() => 1;

    public FighterStats GetStatBonus() => new()
    {
        Strength = -2,
        Dexterity = 1,
        Intelligence = 3,
    };

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter wielder )
    {
        return baseDamage;
    }

    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder )
    {
        int totalRarity = CalculateTotalRarityScore( wielder );
        int rarityDiff = totalRarity - BaselineRarity;

        int damageModifier = rarityDiff * DamageModifierPerRarityTier;

        int newMin = Math.Max( 1, baseDamage.MinDamage + damageModifier );
        int newMax = Math.Max( 1, baseDamage.MaxDamage + damageModifier );

        if ( newMax < newMin )
        {
            newMax = newMin;
        }

        return new DamageStats
        {
            MinDamage = newMin,
            MaxDamage = newMax,
            Type = baseDamage.Type,
            CritChance = baseDamage.CritChance,
            CritDamage = baseDamage.CritDamage
        };
    }

    private static int CalculateTotalRarityScore( IFighter fighter )
    {
        int armorRarity = ( int )( fighter.EquippedArmor.Rarity );
        int weaponRarity = ( int )( fighter.EquippedWeapon.Rarity );

        return armorRarity + weaponRarity;
    }
}

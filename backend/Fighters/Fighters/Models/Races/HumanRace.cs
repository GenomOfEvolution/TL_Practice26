using Fighters.Models.Damage;
using Fighters.Models.Fighters;

namespace Fighters.Models.Races;

public class HumanRace : IRace
{
    public string Name => "Человек";
    public string Description => "получает небольшой бонус ко всем атрибутам";

    public int GetInitiativeModifier()
    {
        return 3;
    }

    public FighterStats GetStatBonus() => new()
    {
        Strength = 3,
        Dexterity = 3,
        Intelligence = 3,
    };

    public DamageStats ModifyIncomingDamage( DamageStats baseDamage, IFighter weilder ) => baseDamage;
    public DamageStats ModifyWeaponDamage( DamageStats baseDamage, IFighter wielder ) => baseDamage;
}

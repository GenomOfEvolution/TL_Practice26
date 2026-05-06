using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }

    FighterStats Stats { get; }
    IArmor EquippedArmor { get; }
    IWeapon EquippedWeapon { get; }
    IRace Race { get; }

    int GetCurrentHealth();
    int GetMaxHealth();

    void SetArmor( IArmor armor );
    void SetWeapon( IWeapon weapon );

    void TakeDamage( int damage );
}
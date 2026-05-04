using Fighters.Models.Armors;
using Fighters.Models.Damage;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }

    int Strength { get; }
    int Dexterity { get; }
    int Intelligence { get; }
    IArmor? EquippedArmor { get; }
    IWeapon? EquippedWeapon { get; }

    int GetCurrentHealth();
    int GetMaxHealth();
    int CalculateDamage();

    int CalculateArmor();

    void SetArmor( IArmor armor );
    void SetWeapon( IWeapon weapon );

    void Act();
    void TakeDamage( DamageStats damage );
}
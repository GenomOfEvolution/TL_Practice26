using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.Models.Fighters;

public class SingleFighter : IFighter
{
    const int STR_HEALTH_MULT = 25;
    const int DEX_HEALTH_MULT = 15;
    const int INT_HEALTH_MULT = 7;

    private readonly IRace _race = new HumanRace();
    private IArmor _armor = new NoArmor();
    private IWeapon _weapon = new Fists();
    private readonly string _name;

    private FighterStats _stats;

    private int _currentHealth;

    public SingleFighter(
        string name,
        FighterStats fighterStats,
        IRace race,
        IArmor armor,
        IWeapon weapon )
    {
        _name = name;
        _stats = fighterStats;
        _race = race;
        _armor = armor;
        _weapon = weapon;

        _currentHealth = GetMaxHealth();
    }

    public string Name => String.IsNullOrEmpty( _name ) ? "Безымянный боец" : _name;
    public FighterStats Stats => _stats;

    public IArmor EquippedArmor => _armor;
    public IWeapon EquippedWeapon => _weapon;
    public IRace Race => _race;

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _stats.Strength * STR_HEALTH_MULT
            + _stats.Dexterity * DEX_HEALTH_MULT
            + _stats.Intelligence * INT_HEALTH_MULT;
    }

    public void SetArmor( IArmor armor )
    {
        _armor = armor;
    }

    public void SetWeapon( IWeapon weapon )
    {
        _weapon = weapon;
    }

    public void TakeDamage( int damage )
    {
        int newHealth = _currentHealth - damage;
        if ( newHealth < 0 )
        {
            newHealth = 0;
        }

        _currentHealth = newHealth;
    }
}

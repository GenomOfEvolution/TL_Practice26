using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.Models.Fighters;

public class SingleFighter : IFighter
{
    private const int _strHealthMult = 25;
    private const int _dexHealthMult = 20;
    private const int _intHealthMult = 15;

    private readonly IRace _race = new HumanRace();
    private IArmor _armor = new NoArmor();
    private IWeapon _weapon = new Fists();
    private readonly ISpeciality _speciality = new NoSpeciality();
    private readonly string _name;

    private FighterStats _stats;

    private int _currentHealth;

    public SingleFighter(
        string name,
        FighterStats fighterStats,
        IRace race,
        ISpeciality speciality,
        IArmor armor,
        IWeapon weapon )
    {
        _name = name;
        _stats = fighterStats;
        _race = race;
        _speciality = speciality;
        _armor = armor;
        _weapon = weapon;

        _currentHealth = GetMaxHealth();
    }

    public string Name => String.IsNullOrEmpty( _name ) ? "Безымянный боец" : _name;
    public FighterStats Stats => _stats;

    public IArmor EquippedArmor => _armor;
    public IWeapon EquippedWeapon => _weapon;
    public IRace Race => _race;

    public ISpeciality Speciality => _speciality;

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        FighterStats raceBonusStats = _race.GetStatBonus();

        return ( Math.Max( 0, _stats.Strength + raceBonusStats.Strength ) ) * _strHealthMult
             + ( Math.Max( 0, _stats.Dexterity + raceBonusStats.Dexterity ) ) * _dexHealthMult
             + ( Math.Max( 0, _stats.Intelligence + raceBonusStats.Intelligence ) ) * _intHealthMult;
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

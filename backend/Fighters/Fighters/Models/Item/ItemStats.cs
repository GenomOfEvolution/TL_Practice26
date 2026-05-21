namespace Fighters.Models.Item;

/// <summary>
/// Ограничения для статов оружия - [0; 50]
/// </summary>
public class ItemStats
{
    private const int _minStatValue = 0;
    private const int _maxStatValue = 50;

    private int _strength;
    private int _dexterity;
    private int _intelligence;

    public int Strength
    {
        get => _strength;
        set
        {
            ValidateStat( value, nameof( Strength ) );
            _strength = value;
        }
    }

    public int Dexterity
    {
        get => _dexterity;
        set
        {
            ValidateStat( value, nameof( Dexterity ) );
            _dexterity = value;
        }
    }

    public int Intelligence
    {
        get => _intelligence;
        set
        {
            ValidateStat( value, nameof( Intelligence ) );
            _intelligence = value;
        }
    }

    private static void ValidateStat( int value, string statName )
    {
        if ( value < _minStatValue || value > _maxStatValue )
        {
            throw new ArgumentOutOfRangeException(
                statName,
                $"ItemStat must be in range {_minStatValue} && {_maxStatValue}"
            );
        }
    }
}
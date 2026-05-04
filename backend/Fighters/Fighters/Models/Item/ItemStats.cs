namespace Fighters.Models.Item;

public class ItemStats
{
    private const int MIN_STAT_VALUE = 0;
    private const int MAX_STAT_VALUE = 50;

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
        if ( value < MIN_STAT_VALUE || value > MAX_STAT_VALUE )
        {
            throw new ArgumentOutOfRangeException(
                statName,
                $"ItemStat must be in range {MIN_STAT_VALUE} && {MAX_STAT_VALUE}"
            );
        }
    }
}
namespace Fighters.Models.PointsBudget;

public class SharedPointsBudget : IPointsBudget
{
    private int _points;
    public SharedPointsBudget( int initialPoints = 0 ) => _points = initialPoints;

    public int RemainingPoints => _points;

    public bool TrySpend( int amount )
    {
        if ( _points < amount )
        {
            return false;
        }

        _points -= amount;

        return true;
    }
}
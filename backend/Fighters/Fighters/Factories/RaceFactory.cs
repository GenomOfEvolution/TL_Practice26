using Fighters.Factories.Contracts;
using Fighters.Models.Races;

namespace Fighters.Factories;

public class RaceFactory : IFighterComponentFactory<IRace>
{
    private readonly List<IRace> _races =
    [
        new HumanRace(),
        new DrowRace(),
        new GnomeRace(),
        new GoblinRace(),
        new StoneGiantRace(),
    ];

    public IRace Create( int choice )
    {
        if ( choice < 0 || choice > _races.Count )
        {
            throw new ArgumentOutOfRangeException( "Такой расы в списке нет" );
        }

        return _races[ choice ];
    }

    public void PrintMenu()
    {
        Console.WriteLine( "Список доступных рас:" );
        for ( int i = 0; i < _races.Count; i++ )
        {
            Console.WriteLine( $"[{i}] {_races[ i ].Name} - {_races[ i ].Description}" );
        }
    }
}

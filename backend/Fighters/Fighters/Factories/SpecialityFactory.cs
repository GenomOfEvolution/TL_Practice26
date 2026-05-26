using Fighters.Factories.Contracts;
using Fighters.Models.Specialities;

namespace Fighters.Factories;

public class SpecialityFactory : IFighterComponentFactory<ISpeciality>
{
    private readonly List<ISpeciality> _specialities =
    [
        new NoSpeciality(),
        new Warrior(),
        new Ranger(),
        new Assassin()
    ];

    public ISpeciality Create( int choice )
    {
        if ( choice < 0 || choice > _specialities.Count )
        {
            throw new ArgumentOutOfRangeException( "Такого класса в списке нет" );
        }

        return _specialities[ choice ];
    }

    public void PrintMenu()
    {
        Console.WriteLine( "Список доступных классов:" );
        for ( int i = 0; i < _specialities.Count; i++ )
        {
            Console.WriteLine( $"[{i}] {_specialities[ i ].Name} - {_specialities[ i ].Description}" );
        }
    }
}

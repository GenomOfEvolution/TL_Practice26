using Fighters.Models.Fighters;

namespace Fighters.Models.Specialities;

public class Assassin : ISpeciality
{
    public string Name => "Ассасин";

    public string Description => "Выбирает цель с наименьшим текущим здоровьем";

    public IFighter SelectTarget( IList<IFighter> candidates )
    {
        IFighter target = candidates[ 0 ];
        int minHp = candidates[ 0 ].GetCurrentHealth();

        for ( int i = 1; i < candidates.Count; i++ )
        {
            int currentHp = candidates[ i ].GetCurrentHealth();
            if ( currentHp < minHp )
            {
                minHp = currentHp;
                target = candidates[ i ];
            }
        }

        return target;
    }
}
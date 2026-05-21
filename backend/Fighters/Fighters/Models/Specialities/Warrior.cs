using Fighters.Models.Fighters;

namespace Fighters.Models.Specialities;

public class Warrior : ISpeciality
{
    public string Name => "Воин";

    public string Description => "Выбирает самого \"толстого\" противника — с наибольшим запасом здоровья";

    public IFighter SelectTarget( IList<IFighter> candidates )
    {
        IFighter target = candidates[ 0 ];
        int maxHp = candidates[ 0 ].GetMaxHealth();

        for ( int i = 1; i < candidates.Count; i++ )
        {
            int currentMaxHp = candidates[ i ].GetMaxHealth();
            if ( currentMaxHp > maxHp )
            {
                maxHp = currentMaxHp;
                target = candidates[ i ];
            }
        }

        return target;
    }
}

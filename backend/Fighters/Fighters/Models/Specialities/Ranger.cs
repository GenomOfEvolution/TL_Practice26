using Fighters.Models.Fighters;

namespace Fighters.Models.Specialities;

public class Ranger : ISpeciality
{
    public string Name => "Рейнджер";

    public string Description => "Атакует последнего в списке";

    public IFighter SelectTarget( IList<IFighter> candidates )
    {
        return candidates[ candidates.Count - 1 ];
    }
}

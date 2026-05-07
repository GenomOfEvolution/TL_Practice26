using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Models.Specialities;

public class NoSpeciality : ISpeciality
{
    public IFighter SelectTarget( IList<IFighter> candidates )
    {
        return candidates.First();
    }
}

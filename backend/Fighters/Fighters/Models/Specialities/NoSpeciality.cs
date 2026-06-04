using Fighters.Models.Fighters;

namespace Fighters.Models.Specialities;

public class NoSpeciality : ISpeciality
{
    public string Name => "Нет класса";

    public string Description => "Выбирает первую попавшуюся цель";

    public IFighter SelectTarget( IList<IFighter> candidates )
    {
        return candidates.First();
    }
}

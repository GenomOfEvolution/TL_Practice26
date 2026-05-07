using Fighters.Models.Fighters;
using Fighters.Models.Weapons;

namespace Fighters.Models.Specialities;

public interface ISpeciality
{
    /// <summary>
    /// Выбирает свою стратегию атаки
    /// </summary>
    IFighter SelectTarget( IList<IFighter> candidates );
}

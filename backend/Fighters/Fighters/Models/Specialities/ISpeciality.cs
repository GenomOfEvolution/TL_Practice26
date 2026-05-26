using Fighters.Models.Fighters;

namespace Fighters.Models.Specialities;

public interface ISpeciality
{
    string Name { get; }
    string Description { get; }

    /// <summary>
    /// Выбирает свою стратегию атаки
    /// </summary>
    IFighter SelectTarget( IList<IFighter> candidates );
}

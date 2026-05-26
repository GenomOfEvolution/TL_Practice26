using Fighters.Models.Fighters;

namespace Fighters.Services.InitiativeService;

public interface IInitiativeService
{
    IReadOnlyList<IFighter> DetermineTurnOrder( IEnumerable<IFighter> participants );
}

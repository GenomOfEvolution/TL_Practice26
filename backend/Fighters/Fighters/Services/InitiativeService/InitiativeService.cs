using Fighters.Models.Fighters;
using Fighters.Services.RandomService;

namespace Fighters.Services.InitiativeService;

public class InitiativeService : IInitiativeService
{
    private const int DexterityReducer = 5;
    private readonly IRandomService _randomService;

    public InitiativeService( IRandomService randomService )
    {
        _randomService = randomService;
    }

    public IReadOnlyList<IFighter> DetermineTurnOrder( IEnumerable<IFighter> participants )
    {
        ArgumentNullException.ThrowIfNull( participants );

        List<IFighter> sorted = [ .. participants
            .Select( f => new FighterInitiative( f, CalculateInitiative( f ) ) )
            .OrderByDescending( entry => entry.InitiativeScore )
            .ThenByDescending( entry => entry.Fighter.Stats.Dexterity )
            .Select( entry => entry.Fighter ) ];

        return sorted.AsReadOnly();
    }

    private int CalculateInitiative( IFighter fighter )
    {
        int dex = fighter.Stats.Dexterity;
        int randomPart = _randomService.Next( 0, ( dex % DexterityReducer ) + 1 );
        int raceModifier = fighter.Race.GetInitiativeModifier();

        return randomPart + raceModifier;
    }
}
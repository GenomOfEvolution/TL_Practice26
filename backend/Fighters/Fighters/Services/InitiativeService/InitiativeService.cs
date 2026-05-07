using Fighters.Models.Fighters;
using Fighters.Services.RandomService;

namespace Fighters.Services.InitiativeService;

public class InitiativeService : IInitiativeService
{
    private readonly IRandomService _randomService;
    private const int DEXTERITY_REDUCER = 5;

    public InitiativeService( IRandomService randomService )
    {
        _randomService = randomService;
    }

    public IReadOnlyList<IFighter> DetermineTurnOrder( IEnumerable<IFighter> participants )
    {
        ArgumentNullException.ThrowIfNull( participants );

        IEnumerable<FighterInitiatvie> fightersWithInitiative = participants
            .Select( f => new FighterInitiatvie( f, CalculateInitiative( f ) ) );

        List<IFighter> sorted = [ ..
            fightersWithInitiative
                .OrderByDescending(entry => entry.InitiativeScore)
                .ThenByDescending(entry => entry.Fighter.Stats.Dexterity)
                .Select(entry => entry.Fighter)
        ];

        return sorted.AsReadOnly();
    }

    private int CalculateInitiative( IFighter fighter )
    {
        int dex = fighter.Stats.Dexterity;
        int randomPart = _randomService.Next( 0, ( dex % DEXTERITY_REDUCER ) + 1 );
        int raceModifier = fighter.Race.GetInitiativeModifier();

        return randomPart + raceModifier;
    }
}
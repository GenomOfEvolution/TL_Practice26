using Fighters.Models.Fighters;

namespace Fighters.Services.InitiativeService;

public class FighterInitiative
{
    public IFighter Fighter { get; }
    public int InitiativeScore { get; }

    public FighterInitiative( IFighter fighter, int initiative )
    {
        Fighter = fighter;
        InitiativeScore = initiative;
    }
}

using Fighters.Models.Fighters;

namespace Fighters.Services.InitiativeService;

public record FighterInitiatvie
(
    IFighter Fighter,
    int InitiativeScore
);

using Fighters.Models.Fighters;

namespace Fighters.Factories.Contracts;

public interface IFighterFactory
{
    IFighter CreateFighter();
    IFighterTeam CreateFighterTeam( int fightersAmount );
}

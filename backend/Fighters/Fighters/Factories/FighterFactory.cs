using Fighters.Factories.Contracts;
using Fighters.Models.Fighters;

namespace Fighters.Factories;

public class FighterFactory : IFighterFactory
{
    public FighterFactory()
    {

    }

    public IFighter CreateFighter()
    {
        throw new NotImplementedException();
    }

    public IFighterTeam CreateFighterTeam( int fightersAmount )
    {
        var fighterTeam = new FighterTeam();

        for ( int i = 0; i < fightersAmount; i++ )
        {
            fighterTeam.AddFighter( CreateFighter() );
        }

        return fighterTeam;
    }
}
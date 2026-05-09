namespace Fighters.Models.Fighters;

public class FighterTeam : IFighterTeam
{
    private List<IFighter> _fighters = [];

    public void AddFighter( IFighter fighter )
    {
        _fighters.Add( fighter );
    }

    public IEnumerable<IFighter> GetMembers()
    {
        return _fighters;
    }
}

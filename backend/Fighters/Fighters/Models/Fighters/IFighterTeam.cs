namespace Fighters.Models.Fighters;

public interface IFighterTeam
{
    IEnumerable<IFighter> GetMembers();
    void AddFighter( IFighter fighter );
}

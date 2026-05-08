using Fighters.Factories.Contracts;
using Fighters.Models.Races;

namespace Fighters.Factories;

public class RaceFactory : IFighterComponentFactory<IRace>
{
    public IRace Create( int choice ) => choice switch
    {
        1 => new HumanRace(),
        _ => throw new ArgumentOutOfRangeException( nameof( choice ), "Такой рассы нет в списке" )
    };

    public void PrintMenu()
    {
        Console.WriteLine(
        $"""
        Список доступных рас:
        1. Человек - получает небольшой бонус ко всем атрибутам
        """
        );
    }
}

using Fighters.Factories.Contracts;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.PointsBudget;
using Fighters.Models.Races;
using Fighters.Models.Specialities;
using Fighters.Models.Weapons;

namespace Fighters.Factories;

public class FighterFactory : IFighterFactory
{
    private readonly IFighterComponentFactory<IRace> _raceFactory;
    private readonly IFighterComponentFactory<ISpeciality> _specialityFactory;
    private readonly IPointRestrictedFactory<FighterStats> _statsFactory;
    private readonly IPointRestrictedFactory<IWeapon> _weaponFactory;
    private readonly IPointRestrictedFactory<IArmor> _armorFactory;

    private readonly int _pointsPerFighter;

    public FighterFactory(
        IFighterComponentFactory<IRace> raceFactory,
        IFighterComponentFactory<ISpeciality> specialityFactory,
        IPointRestrictedFactory<FighterStats> statsFactory,
        IPointRestrictedFactory<IWeapon> weaponFactory,
        IPointRestrictedFactory<IArmor> armorFactory,
        int pointsPerFighter )
    {
        _raceFactory = raceFactory;
        _statsFactory = statsFactory;
        _weaponFactory = weaponFactory;
        _armorFactory = armorFactory;
        _pointsPerFighter = pointsPerFighter;
        _specialityFactory = specialityFactory;
    }

    public IFighter CreateFighter()
    {
        string name = ReadString( "Введите имя бойца: " );
        Console.WriteLine( $"Боец: {name}\n" );

        IPointsBudget budget = new SharedPointsBudget( _pointsPerFighter );
        UpdateFactoriesBudget( budget );

        IRace race = SelectComponent( "Выберите расу (номер): ", _raceFactory, new HumanRace() );
        Console.WriteLine( $"Раса: {race.Name}\n" );

        ISpeciality speciality = SelectComponent( "Выберите класс (номер): ", _specialityFactory, new NoSpeciality() );
        Console.WriteLine( $"Класс: {speciality.Name}\n" );

        FighterStats stats = _statsFactory.Create();
        Console.WriteLine( $"Статы распределены. Осталось очков: {budget.RemainingPoints}\n" );

        IWeapon weapon = SelectPointRestricted( "оружие", _weaponFactory );
        Console.WriteLine( $"Оружие: {weapon.Name}. Осталось очков: {budget.RemainingPoints}\n" );

        IArmor armor = SelectPointRestricted( "броню", _armorFactory );
        Console.WriteLine( $"Броня: {armor.Name}. Осталось очков: {budget.RemainingPoints}\n" );

        return new SingleFighter(
            name: name,
            fighterStats: stats,
            race: race,
            speciality: speciality,
            armor: armor,
            weapon: weapon
        );
    }

    public IFighterTeam CreateFighterTeam( int amount )
    {
        var team = new FighterTeam();
        for ( int i = 0; i < amount; i++ )
        {
            Console.WriteLine( $"\n--- Боец {i + 1}/{amount} ---" );
            team.AddFighter( CreateFighter() );
        }

        return team;
    }

    private void UpdateFactoriesBudget( IPointsBudget budget )
    {
        _armorFactory.SetBudget( budget );
        _weaponFactory.SetBudget( budget );
        _statsFactory.SetBudget( budget );
    }

    private T SelectComponent<T>(
        string selectOption,
        IFighterComponentFactory<T> factory,
        T defaultValue
    ) where T : class
    {
        bool isValid = false;
        T? result = defaultValue;

        factory.PrintMenu();
        while ( !isValid )
        {
            int choice = ReadInt( selectOption );

            try
            {
                result = factory.Create( choice );
                isValid = true;
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Ошибка: {ex.Message}\n" );
            }
        }

        return result!;
    }

    private T SelectPointRestricted<T>( string name, IPointRestrictedFactory<T> factory )
    {
        bool isValid = false;
        T? item = default;

        factory.PrintMenu();
        while ( !isValid )
        {
            int choice = ReadInt( $"Выберите {name} (номер): " );

            if ( factory.TryCreate( choice, out item ) )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( $"Не хватило очков или неверный номер. (Доступно: {factory.RemainingPoints})\n" );
            }
        }

        return item!;
    }

    private static int ReadInt( string prompt )
    {
        bool isValid = false;
        int result = 0;

        while ( !isValid )
        {
            Console.Write( prompt );
            string? input = Console.ReadLine();

            if ( int.TryParse( input, out result ) )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( "Введите корректное целое число.\n" );
            }
        }

        return result;
    }

    private static string ReadString( string prompt )
    {
        bool isValid = false;
        string result = String.Empty;

        while ( !isValid )
        {
            Console.Write( prompt );
            result = Console.ReadLine()!.Trim();

            if ( !String.IsNullOrWhiteSpace( result ) )
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine( "Имя не может быть пустым.\n" );
            }
        }

        return result;
    }
}
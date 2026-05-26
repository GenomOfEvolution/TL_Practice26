using Fighters.Factories.Contracts;
using Fighters.Models.ItemCatalog;
using Fighters.Models.PointsBudget;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.Factories;

public class WeaponFactory : IPointRestrictedFactory<IWeapon>
{
    private readonly IItemCatalog<IWeapon> _catalog;
    private IPointsBudget _pointsBudget = new SharedPointsBudget();
    private IReadOnlyList<CatalogEntry<IWeapon>>? _cachedAvailable;

    public int RemainingPoints => _pointsBudget.RemainingPoints;

    public WeaponFactory( IItemCatalog<IWeapon> catalog )
    {
        _catalog = catalog;
    }

    public IWeapon Create( int choice ) =>
        TryCreate( choice, out IWeapon? weapon )
            ? weapon
            : throw new InvalidOperationException( $"Недостаточно очков или неверный выбор (Осталось: {RemainingPoints})" );


    public void PrintMenu() => _catalog.PrintAvailable( _pointsBudget.RemainingPoints );

    public void SetBudget( IPointsBudget budget )
    {
        _pointsBudget = budget;
    }

    public bool TryCreate( int choice, out IWeapon item )
    {
        _cachedAvailable = _catalog.GetAvailable( _pointsBudget.RemainingPoints );

        if ( choice < 0 || choice >= _cachedAvailable.Count )
        {
            item = new Fists();

            return false;
        }

        CatalogEntry<IWeapon> selectedEntry = _cachedAvailable[ choice ];

        if ( !_pointsBudget.TrySpend( selectedEntry.Price ) )
        {
            item = new Fists();

            return false;
        }

        item = selectedEntry.Item;

        return true;
    }
}

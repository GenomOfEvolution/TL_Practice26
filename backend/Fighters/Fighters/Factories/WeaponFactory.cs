using Fighters.Factories.Contracts;
using Fighters.Models.ItemCatalog;
using Fighters.Models.Weapons;
using Fighters.Models.Weapons.MeleeWeapons;

namespace Fighters.Factories;

public class WeaponFactory : IPointRestrictedFactory<IWeapon>
{
    private readonly IItemCatalog<IWeapon> _catalog;
    private int _pointsToSpend;
    private IReadOnlyList<CatalogEntry<IWeapon>>? _cachedAvailable;

    public WeaponFactory( IItemCatalog<IWeapon> catalog, int startingPoints = 0 )
    {
        _catalog = catalog;
        _pointsToSpend = startingPoints;
    }

    public int RemainingPoints => throw new NotImplementedException();

    public IWeapon Create( int choice ) =>
        TryCreate( choice, out IWeapon? weapon )
            ? weapon
            : throw new InvalidOperationException( $"Недостаточно очков или неверный выбор (Осталось: {RemainingPoints})" );


    public void PrintMenu() => _catalog.PrintAvailable( _pointsToSpend );

    public void SetStartingPoints( int startingPoints )
    {
        _pointsToSpend = startingPoints;
        _cachedAvailable = null;
    }

    public bool TryCreate( int choice, out IWeapon item )
    {
        _cachedAvailable ??= _catalog.GetAvailable( _pointsToSpend );

        if ( choice < 0 || choice >= _cachedAvailable.Count )
        {
            item = new Fists();
            return false;
        }

        CatalogEntry<IWeapon> selectedEntry = _cachedAvailable[ choice ];
        _pointsToSpend -= selectedEntry.Price;
        item = selectedEntry.Item;

        return true;
    }
}

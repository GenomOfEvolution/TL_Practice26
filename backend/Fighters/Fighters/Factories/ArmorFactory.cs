using Fighters.Factories.Contracts;
using Fighters.Models.Armors;
using Fighters.Models.ItemCatalog;
using Fighters.Models.PointsBudget;

namespace Fighters.Factories;

public class ArmorFactory : IPointRestrictedFactory<IArmor>
{
    private readonly IItemCatalog<IArmor> _catalog;
    private readonly IPointsBudget _budget;
    private IReadOnlyList<CatalogEntry<IArmor>>? _cachedAvailable;

    public ArmorFactory( IItemCatalog<IArmor> catalog, IPointsBudget budget )
    {
        _catalog = catalog;
        _budget = budget;
    }

    public IArmor Create( int choice ) =>
        TryCreate( choice, out IArmor? armor )
            ? armor
            : throw new InvalidOperationException( $"Недостаточно очков или неверный выбор (Осталось: {RemainingPoints})" );

    public int RemainingPoints => _budget.RemainingPoints;

    public void PrintMenu() => _catalog.PrintAvailable( _budget.RemainingPoints );

    public bool TryCreate( int choice, out IArmor item )
    {
        _cachedAvailable ??= _catalog.GetAvailable( _budget.RemainingPoints );

        if ( choice < 0 || choice >= _cachedAvailable.Count )
        {
            item = new NoArmor();
            return false;
        }

        CatalogEntry<IArmor> selectedEntry = _cachedAvailable[ choice ];
        if ( !_budget.TrySpend( selectedEntry.Price ) )
        {
            item = new NoArmor();
            return false;
        }

        item = selectedEntry.Item;

        return true;
    }
}

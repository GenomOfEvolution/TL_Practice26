using Fighters.Models.Item;

namespace Fighters.TestLibrary;

public class TestItem : IItem
{
    public string Name { get; }
    public string Description { get; }
    public ItemRarity Rarity { get; }

    public TestItem( string name, string description, ItemRarity rarity )
    {
        Name = name;
        Description = description;
        Rarity = rarity;
    }
}

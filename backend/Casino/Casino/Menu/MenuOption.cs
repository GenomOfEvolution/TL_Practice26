using Casino.Models;

namespace Casino.Menu;

public class MenuOption( string description, Func<OptionHandleResult> handler )
{
    public string Description { get; } = description;
    public Func<OptionHandleResult> Handler { get; } = handler;
}
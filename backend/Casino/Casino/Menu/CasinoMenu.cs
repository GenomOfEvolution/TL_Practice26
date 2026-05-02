using Casino.Models;

namespace Casino.Menu;

public class CasinoMenu
{
    private readonly CasinoGame _casino;
    private readonly Dictionary<string, MenuOption> _optionHandlers;

    public CasinoMenu( CasinoGame casino )
    {
        _casino = casino;
        _optionHandlers = new Dictionary<string, MenuOption>
        {
            [ "1" ] = new MenuOption( "Пополнить баланс ", _casino.MakeDeposit ),
            [ "2" ] = new MenuOption( "Показать баланс ", _casino.ShowBalance ),
            [ "3" ] = new MenuOption( "Сыграть ", _casino.Play ),
            [ "4" ] = new MenuOption( "Выйти ", _casino.Exit )
        };
    }

    public void Run()
    {
        PrintHeader();
        while ( !_casino.IsFinished )
        {
            PrintMenu();
            string option = Console.ReadLine() ?? "";
            OptionHandleResult result = HandleOption( option );
            Console.WriteLine( result );
        }
    }

    private OptionHandleResult HandleOption( string option )
    {
        if ( _optionHandlers.TryGetValue( option, out var menuOption ) )
        {
            return menuOption.Handler();
        }

        return OptionHandleResult.InvalidOption;
    }

    private void PrintMenu()
    {
        foreach ( KeyValuePair<string, MenuOption> kvp in _optionHandlers )
        {
            Console.WriteLine( $"{kvp.Key} - {kvp.Value.Description}" );
        }
    }

    private static void PrintHeader()
    {
        const string header =
        """
        #####    ##    ####  #####  ##   #  ####
        #        ##   #        #    # #  # #    #
        #       #  #   ####    #    # ## # #    #
        #       ####       #   #    #  # # #    #
        #####  #    #  ####  #####  #   ##  ####
        """;
        Console.WriteLine( header );
    }
}
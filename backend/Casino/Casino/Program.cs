using System.Globalization;

double balance = 0;
bool isGameFinished = false;

var optionHandlers = new Dictionary<string, MenuOption>
{
    [ "1" ] = new MenuOption( "Пополнить баланс", MakeDeposit ),
    [ "2" ] = new MenuOption( "Показать баланс", ShowBalance ),
    [ "3" ] = new MenuOption( "Сыграть", Play ),
    [ "4" ] = new MenuOption( "Выйти", Exit )
};

PrintHeader();
while ( !isGameFinished )
{
    PrintMenu();
    string option = Console.ReadLine() ?? "";
    OptionHandleResult res = HandleOption( option );
    Console.WriteLine( res );
}

OptionHandleResult HandleOption( string option )
{
    if ( optionHandlers.TryGetValue( option, out var menuOption ) )
    {
        return menuOption.Handler();
    }

    return OptionHandleResult.InvalidOption;
}

OptionHandleResult Exit()
{
    isGameFinished = true;
    return OptionHandleResult.Succes;
}

OptionHandleResult Play()
{
    Console.Write( "Введите ставку: " );
    string strBet = Console.ReadLine();

    if ( !TryParseDouble( strBet, out double bet ) || bet <= 0d || bet > balance )
    {
        return OptionHandleResult.InvalidBet;
    }

    int seed = Random.Shared.Next( 1, 21 );
    Console.WriteLine( $"Выпало число: {seed}" );

    if ( seed >= 18 && seed <= 20 )
    {
        double winAmount = CalculateWinAmount( bet, seed );
        balance += winAmount;
        Console.WriteLine( $"Вы выиграли: {winAmount}!" );
    }
    else
    {
        Console.WriteLine( "Увы, ставка не зашла!" );
        balance -= bet;
    }

    return OptionHandleResult.Succes;
}

double CalculateWinAmount( double bet, int seed )
{
    const int multiplicator = 25;
    double winPercent = multiplicator * ( seed % 17 );

    if ( winPercent <= 0 ) return 0;
    return bet * ( winPercent / 100 );
}

OptionHandleResult ShowBalance()
{
    Console.WriteLine( $"Ваш текущий баланс: {balance}" );
    return OptionHandleResult.Succes;
}

OptionHandleResult MakeDeposit()
{
    Console.Write( "Введите депозит: " );
    string depositStr = Console.ReadLine();

    if ( !TryParseDouble( depositStr, out double deposit ) || deposit <= 0 )
    {
        return OptionHandleResult.InvlaidDepositValue;
    }

    if ( double.MaxValue - deposit < balance )
    {
        return OptionHandleResult.InvlaidDepositValue;
    }

    balance += deposit;
    return OptionHandleResult.Succes;
}

void PrintMenu()
{
    foreach ( var kvp in optionHandlers )
    {
        Console.WriteLine( $"\"{kvp.Key}\" - {kvp.Value.Description}" );
    }
}

void PrintHeader()
{
    const string header =
        """
         #####   ##    ####  #####  ##   #  ####        
        #        ##   #        #    # #  # #    #       
        #       #  #   ####    #    # ## # #    #       
        #       ####       #   #    #  # # #    #       
         ##### #    #  ####  #####  #   ##  ####        
        """;
    Console.WriteLine( header );
}

bool TryParseDouble( string input, out double value )
{
    if ( string.IsNullOrWhiteSpace( input ) )
    {
        value = 0;
        return false;
    }

    string normalized = input.Trim().Replace( ',', '.' );
    return double.TryParse( normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out value );
}

enum OptionHandleResult
{
    Succes = 0,
    InvalidOption,
    InvlaidDepositValue,
    InvalidBet,
}

readonly struct MenuOption
{
    public string Description { get; }
    public Func<OptionHandleResult> Handler { get; }

    public MenuOption( string description, Func<OptionHandleResult> handler )
    {
        Description = description;
        Handler = handler;
    }
}
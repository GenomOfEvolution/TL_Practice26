using System.Globalization;

namespace Casino.Models;

public class CasinoGame
{
    private double _balance = 0;
    private bool _isGameFinished = false;

    public bool IsFinished => _isGameFinished;

    public OptionHandleResult Play()
    {
        Console.Write( "Введите ставку: " );
        string userInput = Console.ReadLine();

        if ( !IsValidBet( userInput, out double bet ) )
        {
            return OptionHandleResult.InvalidBet;
        }

        int seed = Random.Shared.Next( 1, 21 );
        Console.WriteLine( $"Выпало число: {seed}" );

        if ( seed >= 18 && seed <= 20 )
        {
            double winAmount = CalculateWinAmount( bet, seed );
            _balance += winAmount;
            Console.WriteLine( $"Вы выиграли: {winAmount}!" );
        }
        else
        {
            Console.WriteLine( "Увы, ставка не зашла!" );
            _balance -= bet;
        }

        return OptionHandleResult.Success;
    }

    public OptionHandleResult ShowBalance()
    {
        Console.WriteLine( $"Ваш текущий баланс: {_balance}" );

        return OptionHandleResult.Success;
    }

    public OptionHandleResult MakeDeposit()
    {
        Console.Write( "Введите депозит: " );
        string depositStr = Console.ReadLine();
        if ( !TryParseDouble( depositStr, out double deposit ) || deposit <= 0 )
        {
            return OptionHandleResult.InvlaidDepositValue;
        }

        if ( double.MaxValue - deposit < _balance )
        {
            return OptionHandleResult.InvlaidDepositValue;
        }

        _balance += deposit;

        return OptionHandleResult.Success;
    }

    public OptionHandleResult Exit()
    {
        _isGameFinished = true;

        return OptionHandleResult.Success;
    }

    private bool IsValidBet( string rawInput, out double parsedBet )
    {
        parsedBet = 0d;

        return TryParseDouble( rawInput, out parsedBet ) && parsedBet > 0d && parsedBet <= _balance;
    }

    private static double CalculateWinAmount( double bet, int seed )
    {
        const int multiplicator = 25;
        const int normalizer = 17;
        double winPercent = multiplicator * ( seed % normalizer );
        if ( winPercent <= 0 )
        {
            return 0;
        }

        return bet * ( winPercent / 100 );
    }

    private static bool TryParseDouble( string input, out double value )
    {
        if ( string.IsNullOrWhiteSpace( input ) )
        {
            value = 0;

            return false;
        }
        string normalized = input.Trim().Replace( ',', '.' );

        return double.TryParse( normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out value );
    }
}
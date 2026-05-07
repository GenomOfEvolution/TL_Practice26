namespace OrderManager.Models;

public class OrderManager
{
    private const int _daysToDeliver = 3;

    private string _productName;
    private string _deliveryAddress;
    private string _userName;
    private int _productAmount;
    private DateTime _orderDate;

    public void ProcessOrder()
    {
        _orderDate = DateTime.Now;

        try
        {
            ReadOrderInfo();
            bool confirmed = ConfirmOrder();

            if ( confirmed )
                PrintSuccessMessage();
            else
                PrintCancelMessage();
        }
        catch ( Exception ex )
        {
            Console.WriteLine( ex.Message );
        }
    }

    private void ReadOrderInfo()
    {
        _productName = ReadProductName();
        _productAmount = ReadProductAmount();
        _deliveryAddress = ReadAddress();
        _userName = ReadUserName();
    }

    private bool ConfirmOrder()
    {
        const string confirmPhrase = "y";
        Console.Write( "Подтвердите заказ (Y/N): " );

        string input = Console.ReadLine();

        return String.Equals( input, confirmPhrase, StringComparison.CurrentCultureIgnoreCase );
    }

    private void PrintSuccessMessage()
    {
        Console.WriteLine( $"{_userName}! Ваш заказ {_productName} в количестве {_productAmount} оформлен!" );
        Console.WriteLine( $"Ожидайте доставку по адресу {_deliveryAddress} к {_orderDate.AddDays( _daysToDeliver ):d}" );
    }

    private static void PrintCancelMessage()
    {
        Console.WriteLine( "Ваш заказ отменен" );
    }

    private static string ReadProductName()
    {
        Console.Write( "Введите название товара: " );
        string productName = Console.ReadLine();

        if ( String.IsNullOrEmpty( productName ) )
        {
            throw new Exception( "Имя товара не должно быть пустым!" );
        }

        return productName;
    }

    private static string ReadAddress()
    {
        Console.Write( "Введите адрес доставки: " );
        string address = Console.ReadLine();

        if ( String.IsNullOrEmpty( address ) )
        {
            throw new Exception( "Адрес доставки не должен быть пустым!" );
        }

        return address;
    }

    private static int ReadProductAmount()
    {
        Console.Write( "Введите количество товара: " );
        if ( !int.TryParse( Console.ReadLine(), out int amount ) || amount <= 0 )
        {
            throw new Exception( "Количество товара должно быть целым числом > 0!" );
        }

        return amount;
    }

    private static string ReadUserName()
    {
        Console.Write( "Введите ваше имя: " );
        string name = Console.ReadLine();

        if ( String.IsNullOrEmpty( name ) )
        {
            throw new Exception( "Имя не должно быть пустым!" );
        }

        return name;
    }
}
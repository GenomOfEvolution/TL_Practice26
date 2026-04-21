const int DAYS_TO_DELIVER = 3;
DateTime todaysDate = DateTime.Now;

string productName;
string deliveryAddress;
string userName;
int productAmount;

try
{
    ReadOrderInfo();
    bool confirmed = ConfirmOrder();
    if ( confirmed )
    {
        PrintSuccessMessage();
    }
    else
    {
        PrintCancelMessage();
    }
}
catch ( Exception ex )
{
    Console.WriteLine( ex.Message );
}

void PrintCancelMessage()
{
    Console.WriteLine( "Ваш заказ отменен" );
}

void PrintSuccessMessage()
{
    Console.WriteLine( $"{userName}! Ваш заказ {productName} в количестве {productAmount} оформлен!" );
    Console.WriteLine( $"Ожидайте доставку по адресу {deliveryAddress} к {todaysDate.AddDays( DAYS_TO_DELIVER ):d}" );
}

bool ConfirmOrder()
{
    const string CONFIRM_PHRASE = "y";
    Console.Write( "Подтвердите заказ (Y/N):" );
    return Console.ReadLine().Equals( CONFIRM_PHRASE, StringComparison.CurrentCultureIgnoreCase );
}

void ReadOrderInfo()
{
    productName = ReadProductName();
    productAmount = ReadProductAmount();
    deliveryAddress = ReadAddress();
    userName = ReadUserName();
}

string ReadProductName()
{
    Console.Write( "Введите название товара: " );
    string productName = Console.ReadLine();

    if ( string.IsNullOrEmpty( productName ) )
    {
        throw new Exception( "Имя товара не должно быть пустым!" );
    }

    return productName;
}

string ReadAddress()
{
    Console.Write( "Введите адрес доставки: " );
    string address = Console.ReadLine();

    if ( string.IsNullOrEmpty( address ) )
    {
        throw new Exception( "Адрес доставки не должен быть пустым!" );
    }

    return address;
}

int ReadProductAmount()
{
    Console.Write( "Введите количество товара: " );
    if ( !int.TryParse( Console.ReadLine(), out int amount ) || amount <= 0 )
    {
        throw new Exception( "Количество товара должно быть целым числом > 0!" );
    }

    return amount;
}

string ReadUserName()
{
    Console.Write( "Введите ваше имя: " );
    string name = Console.ReadLine();

    if ( string.IsNullOrEmpty( name ) )
    {
        throw new Exception( "Имя не должно быть пустым!" );
    }

    return name;
}
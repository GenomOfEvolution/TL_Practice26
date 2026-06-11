using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Shared;

public class EnumListToStringConverter<T> : ValueConverter<List<T>, string> where T : Enum
{
    public EnumListToStringConverter()
        : base(
            list => ConvertToDb( list ),
            str => ConvertFromDb( str ) )
    {
    }

    private static string ConvertToDb( List<T> list )
    {
        if ( list is null || list.Count == 0 )
        {
            return string.Empty;
        }

        return string.Join( ',', list.Select( e => Convert.ToInt32( e ).ToString() ) );
    }

    private static List<T> ConvertFromDb( string str )
    {
        if ( string.IsNullOrEmpty( str ) )
        {
            return [];
        }

        return str
            .Split( ',', StringSplitOptions.RemoveEmptyEntries )
            .Select( s => ( T )Enum.ToObject( typeof( T ), int.Parse( s ) ) )
            .ToList();
    }
}

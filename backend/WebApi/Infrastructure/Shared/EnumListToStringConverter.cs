using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Shared;

public class EnumListToStringConverter<T> : ValueConverter<List<T>, string> where T : Enum
{
    public EnumListToStringConverter() : base(
        list => list == null || list.Count == 0 ? string.Empty : string.Join( ',', list.Select( e => Convert.ToInt32( e ) ) ),
        str => string.IsNullOrEmpty( str )
            ? new List<T>()
            : str.Split( ',', StringSplitOptions.RemoveEmptyEntries ).Select( s => ( T )Enum.ToObject( typeof( T ), int.Parse( s ) ) ).ToList() )
    { }
}
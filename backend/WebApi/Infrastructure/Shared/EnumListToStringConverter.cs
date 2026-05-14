using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Shared;

public class EnumListToStringConverter<T> : ValueConverter<List<T>, string> where T : Enum
{
    public EnumListToStringConverter() : base(
        list => list == null || list.Count == 0 ? string.Empty : string.Join( ',', list ),
        str => string.IsNullOrEmpty( str )
            ? new List<T>()
            : str.Split( ',' ).Select( s => ( T )Enum.Parse( typeof( T ), s ) ).ToList() )
    { }
}
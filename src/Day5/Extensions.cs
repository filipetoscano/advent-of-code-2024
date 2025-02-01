using System.Text.Json;

public class Point<T> where T : struct
{
    public int X { get; set; }

    public int Y { get; set; }

    public T Value { get; set; }
}


public static class Extensions
{
    public static string AsJson( this object obj )
    {
        return JsonSerializer.Serialize( obj );
    }


    public static void Dump( this object obj )
    {
        Console.WriteLine( obj.AsJson() );
    }


    public static Point<T>? At<T>( this List<Point<T>> list, int x, int y )
        where T : struct
    {
        var point = list.SingleOrDefault( pt => pt.X == x && pt.Y == y );

        if ( point == null )
            return null;

        return point;
    }


    public static IEnumerable<(int Index, T Value)> WithIndex<T>( this IEnumerable<T> source )
    {
        int index = 0;

        foreach ( T value in source )
        {
            yield return (index, value);
            index++;
        }
    }


    public static List<T> Copy<T>( this List<T> source )
    {
        var list = new List<T>();
        list.AddRange( source );

        return list;
    }
}

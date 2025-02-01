
public class Point<T> where T : struct
{
    public int X { get; set; }

    public int Y { get; set; }

    public T Value { get; set; }
}


public static class Extensions
{
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
}

using System.Xml;

var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );

var map = new List<Point<char>>();

foreach ( var row in input.Split( "\n", StringSplitOptions.RemoveEmptyEntries ).Select( x => x.Trim() ).WithIndex() )
{
    foreach ( var col in row.Value.WithIndex() )
    {
        map.Add( new Point<char>()
        {
            X = col.Index,
            Y = row.Index,
            Value = col.Value,
        } );
    }
}

var sum = 0;

foreach ( var p in map )
{
    if ( p.Value != 'X' )
        continue;

    var up = IsXmas( p, x => x + 0, y => y + 1, 1 );
    var dn = IsXmas( p, x => x + 0, y => y - 1, 1 );
    var rr = IsXmas( p, x => x + 1, y => y + 0, 1 );
    var ll = IsXmas( p, x => x - 1, y => y + 0, 1 );

    var ne = IsXmas( p, x => x + 1, y => y + 1, 1 );
    var se = IsXmas( p, x => x + 1, y => y - 1, 1 );
    var sw = IsXmas( p, x => x - 1, y => y - 1, 1 );
    var nw = IsXmas( p, x => x - 1, y => y + 1, 1 );

    var here = up + dn + rr + ll + ne + se + sw + nw;

    if ( here > 0 )
        Console.WriteLine( "{0} {1}: {2}", p.X, p.Y, here );
    else
        Console.WriteLine( "{0} {1}: none", p.X, p.Y );

    sum += here;
}

Console.WriteLine( "Sum={0}", sum );


int IsXmas( Point<char> point, Func<int, int> nextX, Func<int, int> nextY, int ix )
{
    var nx = nextX( point.X );
    var ny = nextY( point.Y );

    var next = map.At( nx, ny );

    if ( next == null )
        return 0;

    //Console.WriteLine( "{0}: {1} {2} {3}", ix, nx, ny, next.Value );

    if ( next.Value != "XMAS"[ ix ] )
        return 0;

    if ( ix == 3 )
        return 1;

    return IsXmas( next, nextX, nextY, ++ix );
}


public class Point<T>
    where T : struct
{
    public int X { get; set; }
    public int Y { get; set; }

    public T Value { get; set; }
}

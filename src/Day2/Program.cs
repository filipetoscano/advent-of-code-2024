var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );

var safe = 0;

foreach ( var report in input.Split( "\n" ) )
{
    if ( report.Trim().Length == 0 )
        continue;

    var r = report.Split( " ", StringSplitOptions.RemoveEmptyEntries ).Select( x => int.Parse( x ) ).ToList();

    var l1 = r.First();
    var ln = r.Last();

    var min = ( l1 > ln ) ? -3 : 1;
    var max = ( l1 > ln ) ? -1 : 3;

    if ( IsSafe( r, min, max, 1 ) == true )
    { 
        safe++;
    }
    else
    {
        Console.WriteLine( "unsafe: {0}", report );
    }
}

Console.WriteLine( "Nr Safe={0}", safe );


/*
 * 
 */
bool IsSafe( List<int> r, int min, int max, int tolerance )
{
    for ( int i = 0; i < r.Count - 1; i++ )
    {
        var d = r[ i + 1 ] - r[ i ];

        if ( d == 0 || d < min || d > max )
        {
            if ( tolerance == 0 )
                return false;

            // Left
            var r1 = CopyWithout( r, i );
            var is1 = IsSafe( r1, min, max, tolerance - 1 );

            if ( is1 == true )
                return true;

            // Right
            var r2 = CopyWithout( r, i + 1 );
            var is2 = IsSafe( r2, min, max, tolerance - 1 );

            if ( is2 == true )
                return true;

            return false;
        }
    }

    Console.WriteLine( "safe {0}: {1}", tolerance, string.Join( " ", r ) );
    return true;
}


List<int> CopyWithout( List<int> r, int ix )
{
    var rd = new List<int>();
    rd.AddRange( r );
    rd.RemoveAt( ix );

    return rd;
}
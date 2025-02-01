var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );

var safe = 0;

foreach ( var report in input.Split( "\n" ) )
{
    if ( report.Trim().Length == 0 )
        continue;

    var r = report.Split( " ", StringSplitOptions.RemoveEmptyEntries ).Select( x => int.Parse( x ) ).ToList();

    var l1 = r[ 0 ];
    var l2 = r[ 1 ];

    if ( l1 == l2 )
        continue;

    var min = ( l1 > l2 ) ? -3 : 1;
    var max = ( l1 > l2 ) ? -1 : 3;

    if ( IsSafe( r, min, max ) == true )
    {
        Console.WriteLine( "safe: {0}", report );
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
bool IsSafe( List<int> r, int min, int max )
{
    for ( int i = 0; i < r.Count - 1; i++ )
    {
        var d = r[ i + 1 ] - r[ i ];

        if ( d == 0 )
            return false;

        if ( d < min )
            return false;

        if ( d > max )
            return false;
    }

    return true;
}
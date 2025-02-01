using System.Reflection;
using System.Reflection.Metadata.Ecma335;

var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );
var lines = input.Split( Environment.NewLine ).Select( x => x.Trim() ).ToList();

var ix = lines.IndexOf( "" );
var part1 = lines.Take( ix );
var part2 = lines.Skip( ix + 1 );


/*
 * Rules
 */
var rules = new Dictionary<int, List<int>>();

foreach ( var l in part1 )
{
    var lv = l.Split( "|" ).Select( x => int.Parse( x ) ).ToList();

    var page = lv[ 0 ];
    var before = lv[ 1 ];

    if ( rules.ContainsKey( page ) == false )
        rules.Add( page, new List<int>() );

    rules[ page ].Add( before );
}

rules.Dump();


/*
 * Page
 */
var mids = new List<int>();
var mods = new List<int>();

foreach ( var l in part2 )
{
    Console.WriteLine();
    Console.WriteLine( "------" );
    Console.WriteLine( l );

    var pages = l.Split( "," ).Select( x => int.Parse( x ) ).ToList();

    var outcome = "invalid";


    /*
     * 
     */
    if ( Validate( pages ) == true )
        outcome = "valid:1";
    else
    {
        //pages = ReorderPages( pages );
        //outcome = "valid:2";
    }


    /*
     * 
     */
    if ( outcome == "invalid" )
        continue;


    /*
     * 
     */
    var skip = (int) pages.Count / 2;
    var mid = pages.Skip( skip ).First();

    if ( outcome == "valid:1" )
        mids.Add( mid );
    else
        mods.Add( mid );
}


var midSum = mids.Sum();
var modSum = mods.Sum();

Console.WriteLine( "M={0}", string.Join( " ", mids ) );
Console.WriteLine( "MS={0}", midSum );

Console.WriteLine( "O={0}", string.Join( " ", mods ) );
Console.WriteLine( "OS={0}", modSum );


/*
 * 
 */
bool Validate( List<int> pages )
{
    var pageIx = pages.WithIndex().ToDictionary( x => x.Value, x => x.Index );

    foreach ( var p in pageIx )
    {
        if ( rules.ContainsKey( p.Key ) == false )
            continue;

        var ruleset = rules[ p.Key ];

        foreach ( var nextPage in ruleset )
        {
            if ( pageIx.TryGetValue( nextPage, out var nextPageIx ) == false )
                continue;

            if ( p.Value > nextPageIx )
            {
                Console.WriteLine( "err: page {0}@{1} is before {2}@{3}", nextPage, nextPageIx, p.Key, p.Value );
                return false;
            }
        }
    }

    return true;
}

var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );
var lines = input.Split( Environment.NewLine ).Select( x => x.Trim() ).ToList();

var ix = lines.IndexOf( "" );
var part1 = lines.Take( ix );
var part2 = lines.Skip( ix + 1 );


/*
 * Rules
 */
var rules = new List<Rule>();

foreach ( var l in part1 )
{
    var ba = l.Split( "|" );

    rules.Add( new Rule()
    {
        Before = int.Parse( ba[ 0 ] ),
        After = int.Parse( ba[ 1 ] ),
    } );
}


/*
 * Page
 */
var mids = new List<int>();

foreach ( var l in part2 )
{
    //Console.WriteLine();
    //Console.WriteLine( "------" );
    //Console.WriteLine( l );

    var isValid = true;

    var pages = l.Split( "," ).Select( x => int.Parse( x ) ).ToList();

    foreach ( var p in pages.WithIndex() )
    {
        var pr = rules.Where( x => x.Before == p.Value ).Select( x => x.After ).ToList();

        foreach ( var rule in pr )
        {
            //Console.WriteLine( "Page {0} must appear before {1}", p.Value, rule );

            var rx = pages.IndexOf( rule );

            if ( rx < 0 )
            {
                // Console.WriteLine( "wrn: Page {0} not found", rule );
                continue;
            }

            if ( rx < p.Index )
            {
                //Console.WriteLine( "err: Page {0} appears at {1} < {2}", rule, rx, p.Index );
                isValid = false;
            }
        }
    }

    if ( isValid == false )
    {
        //Console.WriteLine( "invalid sequence" );
        continue;
    }

    var skip = (int) pages.Count / 2;
    var mid = pages.Skip( skip ).First();

    mids.Add( mid );
}


var midSum = mids.Sum();

Console.WriteLine( "M={0}", string.Join( " ", mids ) );
Console.WriteLine( "MS={0}", midSum );



public class Rule
{
    public int Before { get; set; }
    public int After { get; set; }
}
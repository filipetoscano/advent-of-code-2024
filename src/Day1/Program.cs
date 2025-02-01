var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );

var left = new List<int>();
var right = new List<int>();
var dist = new List<int>();
var sim = new List<int>();


// Parse
foreach ( var l in input.Split( Environment.NewLine ) )
{
    if ( l.Trim().Length == 0 )
        continue;

    var v = l.Split( " ", StringSplitOptions.RemoveEmptyEntries );

    var lv = int.Parse( v[ 0 ] );
    var rv = int.Parse( v[ 1 ] );

    left.Add( lv );
    right.Add( rv );
}

left.Sort();
right.Sort();


// Distance
for ( int i = 0; i < left.Count; i++ )
{
    var lv = left[ i ];
    var rv = right[ i ];

    var pd = lv - rv;

    if ( pd < 0 )
        pd = -pd;

    dist.Add( pd );
}


// Similarity
var rightAgg = right.GroupBy( x => x ).ToDictionary( x => x.Key, x => x.Count() );

for ( int i = 0; i < left.Count; i++ )
{
    var lv = left[ i ];
    var rv = 0;

    if ( rightAgg.ContainsKey( lv ) == true )
        rv = rightAgg[ lv ];

    sim.Add( lv * rv );
}


// Output
Console.WriteLine( "L={0}", string.Join( " ", left ) );
Console.WriteLine( "R={0}", string.Join( " ", right ) );
Console.WriteLine( "D={0}", string.Join( " ", dist ) );
Console.WriteLine( "S={0}", string.Join( " ", sim ) );

var totalDist = dist.Sum();
var totalSim = sim.Sum();

Console.WriteLine( "TD={0}", totalDist );
Console.WriteLine( "TS={0}", totalSim );

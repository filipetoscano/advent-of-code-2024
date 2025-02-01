using System.Text.RegularExpressions;

var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );

var mulOps = new Regex( @"mul\((?<lv>\d{1,3}),(?<rv>\d{1,3})\)" );
var enableOps = new Regex( @"do(n't)?\(\)" );


/*
 * ON/OFF operations
 */
var mc2 = enableOps.Matches( input );

var onOff = new List<Segment>();
onOff.Add( new Segment()
{
    IsEnabled = true,
    MinIndex = -1,
    MaxIndex = null,
} );

foreach ( Match m in mc2 )
{
    var last = onOff.Last();
    var isEnabled = ( m.Length == 4 );

    if ( last.IsEnabled == isEnabled )
        continue;

    last.MaxIndex = m.Index;

    onOff.Add( new Segment()
    {
        IsEnabled = isEnabled,
        MinIndex = m.Index,
        MaxIndex = null,
    } );
}

onOff.Last().MaxIndex = int.MaxValue;


/*
 * MUL operations
 */
var mc = mulOps.Matches( input );
var muls = new List<int>();

foreach ( Match m in mc )
{
    var ix = m.Index;
    var seg = onOff.Single( x => x.MinIndex <= ix && ix <= x.MaxIndex );

    if ( seg.IsEnabled == false )
        continue;

    var lv = int.Parse( m.Groups[ "lv" ].Value );
    var rv = int.Parse( m.Groups[ "rv" ].Value );
    var mul = lv * rv;

    muls.Add( mul );
}


var sum = muls.Sum();

Console.WriteLine( "M={0}", string.Join( " ", muls ) );
Console.WriteLine( "OnOff={0}", string.Join( " - ", onOff ) );
Console.WriteLine( "MS={0}", sum );


/*
 * 
 */
public class Segment
{
    public bool IsEnabled { get; set; }

    public int MinIndex { get; set; }

    public int? MaxIndex { get; set; }

    public override string ToString()
    {
        return string.Format( "({0} [{1}, {2}[)", this.IsEnabled ? "On" : "Off", this.MinIndex, this.MaxIndex );
    }
}
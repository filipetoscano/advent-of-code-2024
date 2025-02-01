using System.Text.RegularExpressions;

var fname = args.Count() > 0 ? args[ 0 ] : "in.txt";
var input = File.ReadAllText( fname );


var regex = new Regex( @"mul\((?<lv>\d{1,3}),(?<rv>\d{1,3})\)" );

var mc = regex.Matches( input );
var muls = new List<int>();

foreach ( Match m in mc )
{
    var lv = int.Parse( m.Groups[ "lv" ].Value );
    var rv = int.Parse( m.Groups[ "rv" ].Value );

    var mul = lv * rv;

    muls.Add( mul );
}


var sum = muls.Sum();

Console.WriteLine( "M={0}", string.Join( " ", muls ) );
Console.WriteLine( "MS={0}", sum );

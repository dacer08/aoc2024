using AdventOfCode;
using AdventOfCode.Current.D20;
using System.Diagnostics;

var watch = new Stopwatch();
watch.Start();

var d = new D20();
d.Run();

watch.Stop();
Console.WriteLine("Time: " + watch.ElapsedMilliseconds.ToString() + " ms");
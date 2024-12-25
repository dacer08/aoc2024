using AdventOfCode;
using AdventOfCode.Current.D21;
using System.Diagnostics;

var watch = new Stopwatch();
watch.Start();

var d = new D21(watch);
d.Run();

watch.Stop();
Console.WriteLine("Time: " + watch.ElapsedMilliseconds.ToString() + " ms");
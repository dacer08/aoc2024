using AdventOfCode;
using AdventOfCode.Current.D24;
using System.Diagnostics;

var watch = new Stopwatch();
watch.Start();

var d = new D24(watch);
d.Run();

watch.Stop();
Console.WriteLine("Time: " + watch.ElapsedMilliseconds.ToString() + " ms");
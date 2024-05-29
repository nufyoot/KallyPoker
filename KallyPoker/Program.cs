using System.Diagnostics;
using BenchmarkDotNet.Running;
using KallyPoker;

#if DEBUG
var runner = new PokerBenchmarks();

var watch = Stopwatch.StartNew();
runner.RunTest();
watch.Stop();

Console.WriteLine($"The test took {watch.ElapsedMilliseconds}ms");
#else
BenchmarkRunner.Run<PokerBenchmarks>();
#endif
using System.Diagnostics;
using BenchmarkDotNet.Running;
using KallyPoker;

#if DEBUG
var runner = new PokerBenchmarks();

runner.RunTest();
#else
BenchmarkRunner.Run<PokerBenchmarks>();
#endif
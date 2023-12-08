using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

[MemoryDiagnoser]
public class Benchmarks
{

    // [Benchmark]
    // public async Task<string> Day03WithLoading()
    // {
    //     var day = new Day03();
    //     return await day.Solve_2();
    // }
    //
    // [Benchmark]
    // public async Task<string> Day04_01_WithLoading()
    // {
    //     var day = new Day04();
    //     return await day.Solve_1();
    // }
    //
    // [Benchmark]
    // public async Task<string> Day04_02_WithLoading()
    // {
    //     var day = new Day04();
    //     return await day.Solve_2();
    // }

    // [Benchmark]
    // public async Task<string> Day05_01_WithLoading()
    // {
    //     var day = new Day05();
    //     return await day.Solve_1();
    // }

    // [Benchmark]
    // public async Task<string> Day06_Part1_WithLoading()
    // {
    //     var day = new Day06();
    //     return await day.Solve_1();
    // }
    //
    // [Benchmark]
    // public async Task<string> Day06_Part2_WithLoading()
    // {
    //     var day = new Day06();
    //     return await day.Solve_2();
    // }
    //

    // [Benchmark]
    // public async Task<string> Day07_Part1_WithLoading()
    // {
    //     var day = new Day07();
    //     return await day.Solve_1();
    // }
    //
    // [Benchmark]
    // public async Task<string> Day07_Part1_WithLoading_AsSpan()
    // {
    //     var day = new Day07();
    //     return await day.Solve_1_AsSpan();
    // }

    //
    // [Benchmark]
    // public async Task<string> Day07_Part2_WithLoading()
    // {
    //     var day = new Day07();
    //     return await day.Solve_2();
    // }

    // [Benchmark]
    // public Day07.Hand CreateHand()
    // {
    //     return new Day07.Hand("32T3K 765");
    // }
    //
    // [Benchmark]
    // public Day07.Hand CreateHand_Span()
    // {
    //     return new Day07.Hand("32T3K 765".AsSpan());
    // }
    
    [Benchmark]
    public async Task<string> Day08_Part1_WithLoading()
    {
        var day = new Day08();
        return await day.Solve_1();
    }

    [Benchmark]
    public async Task<string> Day08_Part2_WithLoading()
    {
        var day = new Day08();
        return await day.Solve_2();
    }
}
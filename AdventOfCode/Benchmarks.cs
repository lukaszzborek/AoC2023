using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode;

public class Benchmarks
{
    public Benchmarks()
    {
    }
    
    
    [Benchmark]
    public async Task<string> Day03WithLoading()
    {
        var day = new Day03();
        return await day.Solve_2();
    }
    
    [Benchmark]
    public async Task<string> Day04_01_WithLoading()
    {
        var day = new Day04();
        return await day.Solve_1();
    }
    
    [Benchmark]
    public async Task<string> Day04_02_WithLoading()
    {
        var day = new Day04();
        return await day.Solve_2();
    }
}
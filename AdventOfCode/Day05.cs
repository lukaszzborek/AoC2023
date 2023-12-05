using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;
using Range=Perfolizer.Mathematics.RangeEstimators.Range;

namespace AdventOfCode;

public class Day05 : BetterBaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n\r\n");
        var seeds = lines[0]
                    .Split(':')[1]
                    .Trim()
                    .Split(' ')
                    .Select(long.Parse)
                    .ToList();

        var soil = PartToDictionary(lines[1]);
        var fertilizer = PartToDictionary(lines[2]);
        var water = PartToDictionary(lines[3]);
        var light = PartToDictionary(lines[4]);
        var temperature = PartToDictionary(lines[5]);
        var humidity = PartToDictionary(lines[6]);
        var location = PartToDictionary(lines[7]);

        for (int i = 0; i < seeds.Count; i++)
        {
            var temp = soil.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = fertilizer.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = water.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = light.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = temperature.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = humidity.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
            
            temp = location.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
            seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
        }
        
        return new ValueTask<string>(seeds.Min().ToString());
    }
    
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n\r\n");
        var seedsTemp = lines[0]
                    .Split(':')[1]
                    .Trim()
                    .Split(' ')
                    .Select(long.Parse)
                    .ToArray();

        
        var soil = PartToSortedList(lines[1]);
        var fertilizer = PartToSortedList(lines[2]);
        var water = PartToSortedList(lines[3]);
        var light = PartToSortedList(lines[4]);
        var temperature = PartToSortedList(lines[5]);
        var humidity = PartToSortedList(lines[6]);
        var location = PartToSortedList(lines[7]);
        var maps = new List<SortedList<LongRange,long>>(){soil,fertilizer,water,light,temperature,humidity,location};
        var minLocation = long.MaxValue;
        var lockObj = new object();
        
        for (var i = 0; i < seedsTemp.Length; i += 2)
        {
            Parallel.For(0, seedsTemp[i + 1], /*new ParallelOptions(){MaxDegreeOfParallelism = },*/l =>
            {
                var seeds = Calculate(seedsTemp[i] +l, maps);

                if (seeds >= minLocation)
                    return;
                
                lock (lockObj)
                {
                    if (seeds < minLocation)
                        minLocation = seeds;
                }
            });
            Console.WriteLine("Calculated");
        }
        return new ValueTask<string>(minLocation.ToString());
    }
    private static long Calculate(long seeds, List<SortedList<LongRange, long>> maps)
    {
        maps.ForEach(map =>
        {
            var temp = map.FirstOrDefault(x => x.Key.InRange(seeds));
            seeds = seeds + temp.Value - temp.Key.Left;
        });
        
        return seeds;
    }

    // public override ValueTask<string> Solve_2()
    // {
    //     var lines = InputData.Split("\r\n\r\n");
    //     var seedsTemp = lines[0]
    //                 .Split(':')[1]
    //                 .Trim()
    //                 .Split(' ')
    //                 .Select(long.Parse)
    //                 .ToArray();
    //
    //     var seedsList = new List<List<long>>();
    //     
    //     var soil = PartToDictionary(lines[1]);
    //     var fertilizer = PartToDictionary(lines[2]);
    //     var water = PartToDictionary(lines[3]);
    //     var light = PartToDictionary(lines[4]);
    //     var temperature = PartToDictionary(lines[5]);
    //     var humidity = PartToDictionary(lines[6]);
    //     var location = PartToDictionary(lines[7]);
    //     
    //     for (var i = 0; i < seedsTemp.Length; i+=2)
    //     {
    //         var temp = new List<long>();
    //         for (var j = 0; j < seedsTemp[i+1]; j++)
    //         {
    //             temp.Add(seedsTemp[i] + j);
    //         }
    //         seedsList.Add(temp);
    //     }
    //     
    //     
    //
    //
    //     Console.WriteLine("List initialized");
    //     Parallel.ForEach(seedsList, seeds =>
    //     {
    //         for (int i = 0; i < seeds.Count; i++)
    //         {
    //             var seeds1 = seeds;
    //             var temp = soil.FirstOrDefault(x => x.Key.Left <= seeds1[i] && x.Key.Right >= seeds1[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = fertilizer.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = water.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = light.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = temperature.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = humidity.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //
    //             temp = location.FirstOrDefault(x => x.Key.Left <= seeds[i] && x.Key.Right >= seeds[i]);
    //             seeds[i] = seeds[i] + temp.Value - temp.Key.Left;
    //         }
    //     });
    //         
    //
    //     return new ValueTask<string>(seedsList.Select(x=>x.Min()).Min().ToString());
    // }
    
    private static Dictionary<LongRange, long> PartToDictionary(string line)
    {
        return line.Split("\r\n")
                   .Skip(1)
                   .Select(x => x.Split(' ')
                                 .Select(long.Parse)
                                 .ToArray())
                   .ToDictionary(x => LongRange.Of(x[1], x[1] + x[2] - 1), x => x[0]);
    }
    
    private static SortedList<LongRange, long> PartToSortedList(string line)
    {
        var temp = line.Split("\r\n")
                       .Skip(1)
                       .Select(x => x.Split(' ')
                                     .Select(long.Parse)
                                     .ToArray());
        var result = new SortedList<LongRange, long>();
        foreach (var t in temp)
        {
            result.Add(LongRange.Of(t[1], t[1] + t[2] - 1), t[0]);
        }

        return result;
    }
    
    private readonly record struct LongRange(long Left, long Right) : IComparable<LongRange>
    {
        public bool InRange(long value) => Left <= value && Right >= value;

        public static LongRange Of(long left, long right) => new LongRange(left, right);

        public int CompareTo(LongRange other)
        {
            var result = Left.CompareTo(other.Left);
            if (result != 0)
                return result;
            return Right.CompareTo(other.Right);
        }
        
        public override int GetHashCode()
        {
            return System.HashCode.Combine(Left, Right);
        }
    }

}
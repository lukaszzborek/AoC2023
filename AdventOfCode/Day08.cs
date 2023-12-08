using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day08 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var steps = lines[0];

        var map = new Dictionary<string, Tuple<string, string>>();
        for (var i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            map.Add(line[..3], new Tuple<string, string>(line[7..10], line[12..15]));
        }

        var stepsCount = 0;
        var current = "AAA";
        var stop = 0;
        while (true)
        {
            stepsCount++;
            var currentMap = map[current];
            current = steps[stop] switch
            {
                'L' => currentMap.Item1,
                'R' => currentMap.Item2
            };

            if (current == "ZZZ")
                break;

            if (stop == steps.Length - 1)
                stop = 0;
            else
                stop++;
        }

        return new ValueTask<string>(stepsCount.ToString());
    }
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var steps = lines[0];

        var map = new Dictionary<string, Tuple<string, string>>();
        var currents = new List<string>();
        for (var i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var key = line[..3];
            map.Add(key, new Tuple<string, string>(line[7..10], line[12..15]));
            if (key[2] == 'A')
                currents.Add(key);
        }

        var stepsCount = 0;
        var stop = 0;
        var results = new List<int>();

        foreach (var temp in currents)
        {
            var current = temp;
            stepsCount = 0;
            stop = 0;

            while (true)
            {
                stepsCount++;

                var currentMap = map[current];
                current = steps[stop] switch
                {
                    'L' => currentMap.Item1,
                    'R' => currentMap.Item2,
                    _ => throw new Exception("Invalid step")
                };

                if (current[2] == 'Z')
                    break;

                if (stop == steps.Length - 1)
                    stop = 0;
                else
                    stop++;
            }

            results.Add(stepsCount);
        }

        return new ValueTask<string>(CalculateLCM(results)
            .ToString());
    }

    public static long CalculateLCM(List<int> numbers)
    {
        long result = numbers[0];

        for (var i = 1; i < numbers.Count; i++)
            result = CalculateLCMOfTwo(result, numbers[i]);

        return result;
    }

    public static long CalculateLCMOfTwo(long a, long b)
    {
        long tempA = a, tempB = b;

        while (tempB != 0)
        {
            var temp = tempB;
            tempB = tempA % tempB;
            tempA = temp;
        }

        var gcd = tempA;
        var lcm = Math.Abs(a * b) / gcd;

        return lcm;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day01 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var sum = 0;
        var regex = new Regex(@"\d");
        foreach (var line in lines)
        {
            var numberStr = regex.Matches(line)
                                 .Select(x => x.Value)
                                 .ToArray();
            sum += int.Parse($"{numberStr[0]}{numberStr[^1]}");
        }
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var numbers3Dict = new Dictionary<string, string>
        {
            {
                "one", "1"
            },
            {
                "two", "2"
            },
            {
                "six", "6"
            }
        };

        var numbers4Dict = new Dictionary<string, string>
        {
            {
                "four", "4"
            },
            {
                "five", "5"
            },
            {
                "nine", "9"
            }
        };

        var numbers5Dict = new Dictionary<string, string>
        {
            {
                "three", "3"
            },
            {
                "seven", "7"
            },
            {
                "eight", "8"
            }
        };

        var firstChars = new HashSet<char>
        {
            'o',
            't',
            's',
            'f',
            'n',
            'e'
        };
        var sum = 0;
        var data = InputData.AsSpan();
        var newData = new StringBuilder();
        for (var i = 0; i < data.Length; i++)
        {
            if (firstChars.Contains(data[i]))
            {
                if (CheckNumber(data, newData, i, numbers3Dict, 3))
                    continue;
                if (CheckNumber(data, newData, i, numbers4Dict, 4))
                    continue;
                if (CheckNumber(data, newData, i, numbers5Dict, 5))
                    continue;
            }

            newData.Append(data[i]);
        }

        var lines = newData.ToString()
                           .Split("\r\n");
        var regex = new Regex(@"\d");
        foreach (var line in lines)
        {
            var numberStr = regex.Matches(line)
                                 .Select(x => x.Value)
                                 .ToArray();

            sum += int.Parse($"{numberStr[0]}{numberStr[^1]}");
        }

        return new ValueTask<string>(sum.ToString());
    }
    private bool CheckNumber(ReadOnlySpan<char> data, StringBuilder newData, int i,
        Dictionary<string, string> numbersDict, int length)
    {
        if (i + length > data.Length)
            return false;

        var number = data.Slice(i, length);
        if (numbersDict.ContainsKey(number.ToString()))
        {
            newData.Append(numbersDict[number.ToString()]);
            return true;
        }
        return false;
    }
}
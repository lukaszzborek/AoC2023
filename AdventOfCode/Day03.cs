using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day03 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n")
                             .ToArray();
        var sum = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (!char.IsDigit(lines[i][j]))
                    continue;

                if (CheckSymbol(lines, i, j))
                {
                    var result = GetNumber(lines, i, j);
                    j = result.Item2;
                    sum += result.Item1;
                }
            }
        }

        return new ValueTask<string>(sum.ToString());
    }
    
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n")
                             .ToArray();
        var sum = 0;
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] == '*')
                {
                    sum += GetGear(lines, i, j);
                }
            }
        }

        return new ValueTask<string>(sum.ToString());
    }
    private int GetGear(string[] lines, int i, int j)
    {
        var numbers = new HashSet<int>();
        if (i > 0 && j > 0 && char.IsDigit(lines[i - 1][j - 1]))
            numbers.Add(GetNumber(lines, i-1, j-1).Item1);
        if (i > 0 && char.IsDigit(lines[i - 1][j]))
            numbers.Add(GetNumber(lines, i-1, j).Item1);
        if (i > 0 && j < lines[i].Length - 1 && char.IsDigit(lines[i - 1][j + 1]))
            numbers.Add(GetNumber(lines, i-1, j+1).Item1);
        if (j > 0 && char.IsDigit(lines[i][j - 1]))
            numbers.Add(GetNumber(lines, i, j-1).Item1);
        if (j < lines[i].Length - 1 && char.IsDigit(lines[i][j + 1]))
            numbers.Add(GetNumber(lines, i, j+1).Item1);
        if (i < lines.Length - 1 && j > 0 && char.IsDigit(lines[i + 1][j - 1]))
            numbers.Add(GetNumber(lines, i+1, j-1).Item1);
        if (i < lines.Length - 1 && char.IsDigit(lines[i + 1][j]))
            numbers.Add(GetNumber(lines, i+1, j).Item1);
        if (i < lines.Length - 1 && j < lines[i].Length - 1 && char.IsDigit(lines[i + 1][j + 1]))
            numbers.Add(GetNumber(lines, i+1, j+1).Item1);

        return numbers.Count == 1 ? 0 : numbers.Aggregate(1, (current, num) => current * num);
    }

    private static Tuple<int, int> GetNumber(string[] lines, int i, int j)
    {

        var cursor = j;
        var forward = false;
        var start = j;
        var end = j;
        do
        {
            if (forward)
                cursor++;
            else
                cursor--;

            if (cursor < 0 || cursor >= lines[i].Length || !char.IsDigit(lines[i][cursor]))
            {
                if (forward)
                {
                    end = cursor;
                    break;
                }
                start = cursor + 1;
                forward = true;
            }
        } while (true);

        return new Tuple<int, int>(int.Parse(lines[i][start..end]), end - 1);
    }
    
    private bool CheckSymbol(string[] lines, int i, int j)
    {
        if (i > 0 && j > 0 && !char.IsDigit(lines[i - 1][j - 1]) && lines[i - 1][j - 1] != '.')
            return true;
        if (i > 0 && !char.IsDigit(lines[i - 1][j]) && lines[i - 1][j] != '.')
            return true;
        if (i > 0 && j < lines[i].Length - 1 && !char.IsDigit(lines[i - 1][j + 1]) && lines[i - 1][j + 1] != '.')
            return true;
        if (j > 0 && !char.IsDigit(lines[i][j - 1]) && lines[i][j - 1] != '.')
            return true;
        if (j < lines[i].Length - 1 && !char.IsDigit(lines[i][j + 1]) && lines[i][j + 1] != '.')
            return true;
        if (i < lines.Length - 1 && j > 0 && !char.IsDigit(lines[i + 1][j - 1]) && lines[i + 1][j - 1] != '.')
            return true;
        if (i < lines.Length - 1 && !char.IsDigit(lines[i + 1][j]) && lines[i + 1][j] != '.')
            return true;
        if (i < lines.Length - 1 && j < lines[i].Length - 1 && !char.IsDigit(lines[i + 1][j + 1]) &&
            lines[i + 1][j + 1] != '.')
            return true;

        return false;
    }
    
}
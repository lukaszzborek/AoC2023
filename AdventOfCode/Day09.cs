using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day09 : BetterBaseDay
{
    private List<History> Histories { get; set; }
    
    public override ValueTask<string> Solve_1()
    {
        PrepareDataSpanP();
        var result = 0;
        foreach (var history in Histories)
            result += history.CalculateNextValue();

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        PrepareDataSpanP();
        var result = 0;
        foreach (var history in Histories)
            result += history.CalculatePreviousValue();

        return new ValueTask<string>(result.ToString());
    }
    
    public void PrepareDataSpan()
    {
        var data = InputData.AsSpan();
        Histories = new List<History>(200);
        int index = 0;
        do
        {
            index = data.IndexOf('\n');
            if (index == -1)
            {
                Histories.Add(new History(data));
                break;
            }
            
            Histories.Add(new History(data[..(index-1)]));
            data = data[(index+1)..];
        } while (true);
    }
    
    private void PrepareDataSpanP()
    {
        var data = InputData.AsSpan();
        Histories = new List<History>(205);
        int index = 0;
        do
        {
            index = data.IndexOf('\n');
            if (index == -1)
            {
                Histories.Add(new History(data));
                break;
            }
            
            Histories.Add(new History(data[..(index-1)]));
            data = data[(index+1)..];
        } while (true);
    }
    
    public void PrepareData()
    {
        var lines = InputData.Split("\r\n");
        Histories = lines.Select(x => new History(x))
                         .ToList();
    }
    
    public ValueTask<string> Solve_1_OnlyLogic()
    {
        var result = 0;
        foreach (var history in Histories)
            result += history.CalculateNextValue();

        return new ValueTask<string>(result.ToString());
    }

    private sealed class History
    {
        public List<int> Numbers { get; }
        public History(ReadOnlySpan<char> line)
        {
            Numbers = new List<int>(20);
            do
            {
                var index = line.IndexOf(' ');
                if (index == -1)
                {
                    Numbers.Add(int.Parse(line));
                    break;
                }
                Numbers.Add(int.Parse(line[..index]));
                line = line[(index+1)..];
            } while (true); 
        }
        
        // public History(string line)
        // {
        //     Numbers = line.Split(" ")
        //                   .Select(int.Parse)
        //                   .ToList();
        // }

        public int CalculateNextValue()
        {
            var steps = new List<List<int>>();
            var currentStep = Numbers;
            var nextStep = new List<int>(10);
            steps.Add(currentStep);
            do
            {
                var allZero = true;
                for (var i = 0; i < currentStep.Count - 1; i++)
                {
                    var value = currentStep[i + 1] - currentStep[i];
                    nextStep.Add(value);

                    if (allZero && value != 0)
                        allZero = false;
                }

                if (allZero)
                    break;

                steps.Add(nextStep);
                currentStep = nextStep;
                nextStep = new List<int>(10);
            } while (true);

            for (var i = steps.Count - 2; i >= 0; i--)
            {
                var diff = steps[i + 1][^1];
                if (i == 0)
                    return steps[i][^1] + diff;
                
                steps[i]
                    .Add(steps[i][^1] + diff);
            }

            return steps[0][^1];
        }

        public int CalculatePreviousValue()
        {
            Numbers.Reverse();
            return CalculateNextValue();
        }
    }
}
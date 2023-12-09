using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day09 : BetterBaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var histories = lines.Select(x => new History(x));
        var result = 0;
        foreach (var history in histories)
            result += history.CalculateNextValue();

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var histories = lines.Select(x => new History(x));
        var result = 0;
        foreach (var history in histories)
            result += history.CalculatePreviousValue();

        return new ValueTask<string>(result.ToString());
    }

    private sealed class History
    {
        public List<int> Numbers { get; }
        public History(string line)
        {
            Numbers = line.Split(" ")
                          .Select(int.Parse)
                          .ToList();
        }

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
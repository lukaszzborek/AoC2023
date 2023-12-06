using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day06 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        int[] times = null;
        int[] distances = null;
        var split = lines[0]
                    .Split(':')
                    .Select(x => x.Trim())
                    .ToArray();

        times = split[1]
                .Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x.Trim()))
                .ToArray();

        split = lines[1]
                .Split(':')
                .Select(x => x.Trim())
                .ToArray();
        distances = split[1]
                    .Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => int.Parse(x.Trim()))
                    .ToArray();


        var result = 1;
        for (var i = 0; i < times.Length; i++)
        {
            var raceResult = 0;
            var prevWin = false;
            for (var j = 0; j < times[i]; j++)
            {
                var speed = j;
                var distance = speed * (times[i] - j);
                if (distance > distances[i])
                    raceResult++;
                else if (raceResult > 0)
                    break;
            }
            result *= raceResult;
        }
        return new ValueTask<string>(result.ToString());
    }
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        long times = 0;
        long distances = 0;
        var split = lines[0]
                    .Replace(" ", "")
                    .Split(':');

        times = long.Parse(split[1]);

        split = lines[1]
                .Replace(" ", "")
                .Split(':');
        distances = long.Parse(split[1]);

        var result = 0;
        for (var j = 0; j < times; j++)
        {
            var speed = j;
            var distance = speed * (times - j);
            if (distance > distances)
                result++;
            else if (result > 0)
                break;
        }
        return new ValueTask<string>(result.ToString());
    }
}
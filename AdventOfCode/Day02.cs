using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day02 : BetterBaseDay
{
    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var games = new List<Game>();
        foreach (var line in lines)
            games.Add(Game.Parse(line));

        games = games.Where(x => x.Rounds.SelectMany(r => r.Cubes)
                                  .Where(c => c.Color == "red")
                                  .All(c => c.Quantity <= 12))
                     .ToList();

        games = games.Where(x => x.Rounds.SelectMany(r => r.Cubes)
                                  .Where(c => c.Color == "green")
                                  .All(c => c.Quantity <= 13))
                     .ToList();

        games = games.Where(x => x.Rounds.SelectMany(r => r.Cubes)
                                  .Where(c => c.Color == "blue")
                                  .All(c => c.Quantity <= 14))
                     .ToList();

        return new ValueTask<string>(games.Sum(x => x.Number)
                                          .ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var games = new List<Game>();
        foreach (var line in lines)
            games.Add(Game.Parse(line));

        var power = games.Sum(x => x.GetPower());
        return new ValueTask<string>(power.ToString());
    }

    private record Game(int Number, List<Round> Rounds)
    {
        public static Game Parse(string line)
        {
            var splited = line.Split(':');
            var number = int.Parse(splited[0]
                .Split(' ')[1]);
            var roundsStr = splited[1]
                .Split(';');
            var rounds = new List<Round>();
            foreach (var roundStr in roundsStr)
                rounds.Add(Round.Parse(roundStr));

            return new Game(number, rounds);
        }

        public int GetPower()
        {
            var cubes = Rounds.SelectMany(x => x.Cubes)
                              .GroupBy(x => x.Color)
                              .ToList();
            var power = 1;
            foreach (var cubeColor in cubes)
                power *= cubeColor.Max(x => x.Quantity);

            return power;
        }
    }

    private record Round(List<Cube> Cubes)
    {
        public static Round Parse(string line)
        {
            var splited = line.Split(',');
            var cubes = new List<Cube>();
            foreach (var color in splited)
            {
                var temp = color.Trim()
                                .Split(' ');
                var quantity = int.Parse(temp[0]);
                cubes.Add(new Cube(temp[1], quantity));
            }

            return new Round(cubes);
        }
    }

    private record Cube(string Color, int Quantity);
}
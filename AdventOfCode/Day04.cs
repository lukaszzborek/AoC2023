using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day04 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var cards = new List<Card>();
        foreach (var line in lines)
            cards.Add(new Card(line));

        var result = 0;
        foreach (var card in cards)
        {
            card.WinningNumbers.IntersectWith(card.Numbers);
            var qty = card.WinningNumbers.Count;
            if (qty == 0)
                continue;

            result += (int)Math.Pow(2, qty - 1);
        }

        return new ValueTask<string>(result.ToString());
    }
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var cards = new List<Card>();
        foreach (var line in lines)
            cards.Add(new Card(line));

        for (var i = 0; i < cards.Count; i++)
        {
            cards[i]
                .WinningNumbers.IntersectWith(cards[i].Numbers);
            var qty = cards[i].WinningNumbers.Count;
            if (qty == 0)
                continue;

            for (var j = 1; j <= qty; j++)
                cards[i + j].Copies += 1 + cards[i].Copies;
        }

        var result = cards.Sum(x => x.Copies) + cards.Count;
        return new ValueTask<string>(result.ToString());
    }

    private sealed class Card
    {
        public int Number { get; set; }
        public HashSet<string> Numbers { get; }
        public HashSet<string> WinningNumbers { get; }
        public int Copies { get; set; }

        public Card(string line)
        {
            Numbers = new HashSet<string>();
            WinningNumbers = new HashSet<string>();
            var split = line.Split(':');
            Number = int.Parse(split[0]
                               .Trim()
                               .Split(' ')[^1]);
            var numbers = split[1]
                .Split('|');
            var splitNumbers = numbers[0]
                               .Trim()
                               .Split(' ');
            foreach (var splitNumber in splitNumbers)
            {
                if (splitNumber.Trim() == string.Empty)
                    continue;

                Numbers.Add(splitNumber.Trim());
            }

            var winningSplitNumbers = numbers[1]
                                      .Trim()
                                      .Split(' ');
            foreach (var splitNumber in winningSplitNumbers)
            {
                if (splitNumber.Trim() == string.Empty)
                    continue;

                WinningNumbers.Add(splitNumber.Trim());
            }
        }
    }
}
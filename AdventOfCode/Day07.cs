using System;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day07 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var hands = lines.Select(x => new Hand(x))
                         .ToList();

        hands.Sort();
        for (var i = 0; i < hands.Count; i++)
            hands[i].Rank = i + 1;

        return new ValueTask<string>(hands.Sum(x => x.Valud)
                                          .ToString());
    }
    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var hands = lines.Select(x => new Hand(x, true))
                         .ToList();

        hands.Sort();
        for (var i = 0; i < hands.Count; i++)
            hands[i].Rank = i + 1;


        return new ValueTask<string>(hands.Sum(x => x.Valud)
                                          .ToString());
    }

    private class Hand : IComparable<Hand>
    {

        private readonly int[] _cardsNormalize;
        public string Cards { get; }
        public int Bid { get; }
        public HandPower Power { get; }
        public int Rank { get; set; }
        public int Valud => Bid * Rank;

        public Hand(string line, bool joker = false)
        {
            var split = line.Split(' ');
            Cards = split[0];
            Bid = int.Parse(split[1]);

            if (joker)
                Power = GetPowerWithJoker(Cards);
            else
                Power = GetPower(Cards);

            var cardsChar = Cards.ToCharArray();
            _cardsNormalize = new int[cardsChar.Length];
            for (var i = 0; i < _cardsNormalize.Length; i++)
                _cardsNormalize[i] = cardsChar[i] switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' when !joker => 11,
                    'J' when joker => 1,
                    'T' => 10,
                    _ => int.Parse(cardsChar[i]
                        .ToString())
                };
        }

        public int CompareTo(Hand other)
        {
            if (Power != other.Power)
                return Power > other.Power ? 1 : -1;

            for (var i = 0; i < _cardsNormalize.Length; i++)
            {
                if (_cardsNormalize[i] == other._cardsNormalize[i])
                    continue;
                return _cardsNormalize[i] > other._cardsNormalize[i] ? 1 : -1;
            }

            return 0;
        }

        private HandPower GetPowerWithJoker(string hand)
        {
            var jokers = Cards.Count(x => x == 'J');

            var power = GetPower(hand);

            if (jokers == 0)
                return power;

            switch (power)
            {
                case HandPower.FiveOfAKind:
                case HandPower.FourOfAKind when jokers == 1:
                case HandPower.FourOfAKind when jokers == 4:
                    return HandPower.FiveOfAKind;
                case HandPower.FullHouse when jokers == 1: return HandPower.FourOfAKind;
                case HandPower.FullHouse: return HandPower.FiveOfAKind;
                case HandPower.ThreeOfAKind when jokers == 3: return HandPower.FourOfAKind;
                case HandPower.ThreeOfAKind when jokers == 2: return power;
                case HandPower.ThreeOfAKind when jokers == 1:
                case HandPower.TwoPairs when jokers == 2:
                    return HandPower.FourOfAKind;
                case HandPower.TwoPairs when jokers == 1: return HandPower.FullHouse;
                case HandPower.Pair when jokers == 2:
                case HandPower.Pair when jokers == 1:
                    return HandPower.ThreeOfAKind;
                default: return power + jokers;
            }

        }

        private HandPower GetPower(string hand)
        {
            var types = hand.GroupBy(x => x)
                            .ToList();
            var groups = types.Count;
            switch (groups)
            {
                case 1: return HandPower.FiveOfAKind;
                case 5: return HandPower.HighCard;
            }

            var max = types.Max(x => x.Count());
            return max switch
            {
                4 => HandPower.FourOfAKind,
                3 when types.Min(x => x.Count()) == 2 => HandPower.FullHouse,
                3 => HandPower.ThreeOfAKind,
                _ => groups == 3 ? HandPower.TwoPairs : HandPower.Pair
            };
        }
    }

    private enum HandPower
    {
        HighCard = 1,
        Pair = 2,
        TwoPairs = 3,
        ThreeOfAKind = 4,
        FullHouse = 5,
        FourOfAKind = 6,
        FiveOfAKind = 7
    }
}
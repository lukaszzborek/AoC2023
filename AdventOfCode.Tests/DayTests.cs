using Shouldly;
using Xunit;

namespace AdventOfCode.Tests;

public class DayTests
{
    [Fact]
    public async Task Day01_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        });

        var day = new Day01();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("142");
    }

    [Fact]
    public async Task Day01_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        });

        var day = new Day01();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("281");
    }

    [Fact]
    public async Task Day02_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        });

        var day = new Day02();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("8");
    }

    [Fact]
    public async Task Day02_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        });

        var day = new Day02();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("2286");
    }

    [Fact]
    public async Task Day03_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        });

        var day = new Day03();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("4361");
    }

    [Fact]
    public async Task Day03_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        });

        var day = new Day03();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("467835");
    }

    [Fact]
    public async Task Day04_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        });

        var day = new Day04();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("13");
    }

    [Fact]
    public async Task Day04_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53",
            "Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19",
            "Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1",
            "Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83",
            "Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36",
            "Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11"
        });

        var day = new Day04();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("30");
    }

    [Fact]
    public async Task Day05_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        });

        var day = new Day05();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("35");
    }

    [Fact]
    public async Task Day05_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        });

        var day = new Day05();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("46");
    }

    [Fact]
    public async Task Day06_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        });

        var day = new Day06();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("288");
    }

    [Fact]
    public async Task Day06_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "Time:      7  15   30",
            "Distance:  9  40  200"
        });

        var day = new Day06();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("71503");
    }

    [Fact]
    public async Task Day07_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        });

        var day = new Day07();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("6440");
    }

    [Fact]
    public async Task Day07_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "32T3K 765",
            "T55J5 684",
            "KK677 28",
            "KTJJT 220",
            "QQQJA 483"
        });

        var day = new Day07();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("5905");
    }

    [Fact]
    public async Task Day08_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "RL",
            "",
            "AAA = (BBB, CCC)",
            "BBB = (DDD, EEE)",
            "CCC = (ZZZ, GGG)",
            "DDD = (DDD, DDD)",
            "EEE = (EEE, EEE)",
            "GGG = (GGG, GGG)",
            "ZZZ = (ZZZ, ZZZ)"
        });

        var day = new Day08();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("2");
    }

    [Fact]
    public async Task Day08_01_Test2()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "LLR",
            "",
            "AAA = (BBB, BBB)",
            "BBB = (AAA, ZZZ)",
            "ZZZ = (ZZZ, ZZZ)"
        });

        var day = new Day08();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("6");
    }

    [Fact]
    public async Task Day08_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "LR",
            "",
            "11A = (11B, XXX)",
            "11B = (XXX, 11Z)",
            "11Z = (11B, XXX)",
            "22A = (22B, XXX)",
            "22B = (22C, 22C)",
            "22C = (22Z, 22Z)",
            "22Z = (22B, 22B)",
            "XXX = (XXX, XXX)"
        });

        var day = new Day08();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("6");
    }

    [Fact]
    public async Task Day09_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        });

        var day = new Day09();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("114");
    }

    [Fact]
    public async Task Day09_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        });

        var day = new Day09();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("2");
    }

    [Fact]
    public async Task Day09_01_RealData()
    {
        var day = new Day09();
        var result = await day.Solve_1();
        result.ShouldBe("2043183816");
    }

    [Fact]
    public async Task Day09_02_RealData()
    {
        var day = new Day09();
        var result = await day.Solve_2();
        result.ShouldBe("1118");
    }

    [Fact]
    public async Task Day09_01_RealData_2()
    {
        var day = new Day09();
        day.PrepareData();
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
        (await day.Solve_1_OnlyLogic()).ShouldBe("2043183816");
    }

    [Fact]
    public async Task Day10_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            ".....",
            ".S-7.",
            ".|.|.",
            ".L-J.",
            "....."
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("4");
    }

    [Fact]
    public async Task Day10_01_Test2()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "..F7.F7",
            ".FJ|.LJ",
            "SJ.L7..",
            "|F--J..",
            "LJ....."
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("8");
    }

    [Fact]
    public async Task Day10_01_RealData()
    {
        var day = new Day10();
        var result = await day.Solve_1();
        result.ShouldBe("6725");
    }

    [Fact]
    public async Task Day10_02_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "................",
            ".S-------7..F-7.",
            ".|F-----7|..|.|.",
            ".||.....||..L-J.",
            ".||.....||......",
            ".|L-7.F-J|......",
            ".|..|.|..|......",
            ".L--J.L--J......",
            "................"
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("4");
    }

    [Fact]
    public async Task Day10_02_Test2()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "..........",
            ".S------7.",
            ".|F----7|.",
            ".||....||.",
            ".||....||.",
            ".|L-7F-J|.",
            ".|..||..|.",
            ".L--JL--J.",
            ".........."
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("4");
    }

    [Fact]
    public async Task Day10_02_Test3()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            ".F----7F7F7F7F-7....",
            ".|F--7||||||||FJ....",
            ".||.FJ||||||||L7....",
            "FJL7L7LJLJ||LJ.L-7..",
            "L--J.L7...LJS7F-7L7.",
            "....F-J..F7FJ|L7L7L7",
            "....L7.F7||L7|.L7L7|",
            ".....|FJLJ|FJ|F7|.LJ",
            "....FJL-7.||.||||...",
            "....L---J.LJ.LJLJ..."
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("8");
    }

    [Fact]
    public async Task Day10_02_Test4()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "FF7FSF7F7F7F7F7F---7",
            "L|LJ||||||||||||F--J",
            "FL-7LJLJ||||||LJL-77",
            "F--JF--7||LJLJ7F7FJ-",
            "L---JF-JLJ.||-FJLJJ7",
            "|F|F-JF---7F7-L7L|7|",
            "|FFJF7L7F-JF7|JL---7",
            "7-L-JL7||F7|L7F-7F7|",
            "L.L7LFJ|||||FJL7||LJ",
            "L7JLJL-JLJLJL--JLJ.L"
        });

        var day = new Day10();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_2();
        result.ShouldBe("10");
    }

    [Fact]
    public async Task Day11_01_Test1()
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        });

        var day = new Day11();
        day.IsTest = true;
        day.TestInput = strList;
        var result = await day.Solve_1();
        result.ShouldBe("374");
    }
    
    [Theory]
    [InlineData(1, 6, 5,11,9)]
    [InlineData(4, 0, 9,10,15)]
    [InlineData(0, 2, 12,7,17)]
    [InlineData(0, 11, 5,11,5)]
    [InlineData(2, 15, 26,14,25)]
    public void Day11_01_Test2(int x1, int y1, int x2, int y2, int expected)
    {
        var day = new Day11();
        day.Steps(new Day11.Point(x1, y1), new Day11.Point(x2, y2)).ShouldBe(expected);
    }
    
    [Fact]
    public async Task Day11_01_RealData()
    {
        var day = new Day11();
        var result = await day.Solve_1();
        result.ShouldBe("9521550");
    }
    
    [Theory]
    [InlineData(10, 1030)]
    [InlineData(100, 8410)]
    public async Task Day11_02_Test1(int scale, int expected)
    {
        var strList = string.Join("\r\n", new List<string>
        {
            "...#......",
            ".......#..",
            "#.........",
            "..........",
            "......#...",
            ".#........",
            ".........#",
            "..........",
            ".......#..",
            "#...#....."
        });

        var day = new Day11();
        day.IsTest = true;
        day.TestInput = strList;
        day.Scale = scale;
        var result = await day.Solve_1();
        result.ShouldBe(expected.ToString());
    }
    
    [Fact]
    public async Task Day11_02_RealData()
    {
        var day = new Day11();
        var result = await day.Solve_2();
        result.ShouldBe("298932923702");
    }
}
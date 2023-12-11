using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day11 : BetterBaseDay
{
    public long Scale { get; set; } = 2;
    public override ValueTask<string> Solve_1()
    {
        Scale -= 1;
        var result = Calculate();
        return new ValueTask<string>((result).ToString());
    }
    
    public override ValueTask<string> Solve_2()
    {
        Scale = 1000000;
        Scale -= 1;
        var result = Calculate();
        return new ValueTask<string>((result).ToString());
    }
    
    private long Calculate()
    {
        var lines = InputData.Split("\r\n");
        var galaxies = new List<Point>();
        long maxX = 0;
        long maxY = 0;
        
        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                if (lines[i][j] != '#')
                {
                    continue;
                }
                
                galaxies.Add(new Point(j, i));
                        
                maxX = Math.Max(maxX, j);
                maxY = Math.Max(maxY, i);
            }
        }

        //Print(galaxies);
        
        ExpandGalaxy(galaxies, maxX, maxY);

        //Print(galaxies);
        //var pairs = new HashSet<(Point, Point)>();
        long result = 0;
        foreach (var g1 in galaxies)
        {
            //foreach (var g2 in galaxies.Where(g2 => !pairs.Contains((g1, g2)) && !pairs.Contains((g2, g1))).Where(g2 => !g1.Equals(g2)))
            foreach (var g2 in galaxies)
            {
                //pairs.Add((g1, g2));
                result += Steps(g1, g2);
            }
        }

        return result/2;
    }
    private void ExpandGalaxy(List<Point> galaxies, long maxX, long maxY)
    {

        for (long i = 0; i < maxY; i++)
        {
            if (galaxies.Exists(x => x.Y == i))
            {
                continue;
            }

            for (var j = 0; j < galaxies.Count; j++)
            {
                if(galaxies[j].Y > i)
                    galaxies[j].Y += Scale;
            }
            // foreach (var g in galaxies.Where(x => x.Y > i))
            // {
            //     g.Y += Scale;
            // }
            i += Scale;
            maxY += Scale;
        }

        for (long i = 0; i < maxX; i++)
        {
            if (galaxies.Exists(x => x.X == i))
            {
                continue;
            }
        
            for (var j = 0; j < galaxies.Count; j++)
            {
                if(galaxies[j].X > i)
                    galaxies[j].X += Scale;
            }
            
            // foreach (var g in galaxies.Where(x => x.X > i))
            // {
            //     g.X += Scale;
            // }
            i += Scale;
            maxX += Scale;
        }
    }

    public long Steps(Point a, Point b)
    {
        return Math.Abs(b.X - a.X) + Math.Abs(b.Y - a.Y);
    }

    private void Print(List<Point> galaxies)
    {
        var maxX = galaxies.Max(g => g.X);
        var maxY = galaxies.Max(g => g.Y);
        
        Console.WriteLine("");
        
        for (var i = 0; i <= maxY; i++)
        {
            for (var j = 0; j <= maxX; j++)
            {
                if (galaxies.Contains(new Point(j, i)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("");
        Console.WriteLine("==================================");
    }

    public class Point
    {
        public long X { get; set; }
        public long Y { get; set; }

        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point point)
                return X == point.X && Y == point.Y;

            return false;
        }
    }
}
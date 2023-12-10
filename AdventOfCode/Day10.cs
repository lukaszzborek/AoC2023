using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Core;

namespace AdventOfCode;

public class Day10 : BetterBaseDay
{

    public override ValueTask<string> Solve_1()
    {
        var lines = InputData.Split("\r\n");
        var pointsDictionary = new Dictionary<Point, Point>(lines.Length * lines[0].Length);
        Point startingPoint = null;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == '.')
                    continue;

                var point = new Point(j, i, line[j]);
                pointsDictionary.Add(point, point);

                if (point.Value == Connections.Starting)
                    startingPoint = point;
            }
        }
        
        if (startingPoint == null)
            throw new InvalidDataException("Starting point not found");

        foreach (var point in pointsDictionary.Values)
        {
            if (point.Value == Connections.Starting)
                continue;
            if (pointsDictionary.TryGetValue(point.GetNext(), out var next))
            {
                point.Next = next;

                if (next == startingPoint)
                    startingPoint.Previous = point;
            }

            if (pointsDictionary.TryGetValue(point.GetPrev(), out var prev))
            {
                point.Previous = prev;

                if (prev == startingPoint)
                    startingPoint.Next = point;
            }
        }

        var currentPoint = startingPoint;
        var result = 0;
        Point prevPoint = null;
        do
        {
            if (prevPoint == null || prevPoint != currentPoint.Next)
            {
                prevPoint = currentPoint;
                currentPoint = currentPoint.Next;
            }
            else
            {
                prevPoint = currentPoint;
                currentPoint = currentPoint.Previous;
            }
            result++;
        } while (currentPoint != startingPoint);

        result /= 2;

        return new ValueTask<string>(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = InputData.Split("\r\n");
        var pointsDictionary = new Dictionary<Point, Point>(lines.Length * lines[0].Length);
        Point startingPoint = null;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                var point = new Point(j, i, line[j]);
                pointsDictionary.Add(point, point);

                if (point.Value == Connections.Starting)
                    startingPoint = point;
            }
        }

        AddMiddlePoints(pointsDictionary);
        Print(pointsDictionary);
        Console.WriteLine("------------------------------------");
        if (startingPoint == null)
            throw new InvalidDataException("Starting point not found");
        
        foreach (var point in pointsDictionary.Values)
        {
            if (point.Value == Connections.Starting || point.Value == Connections.None)
                continue;
            if (pointsDictionary.TryGetValue(point.GetNext(0.5), out var next))
            {
                point.Next = next;

                if (next == startingPoint)
                    startingPoint.Previous = point;
            }

            if (pointsDictionary.TryGetValue(point.GetPrev(0.5), out var prev))
            {
                point.Previous = prev;

                if (prev == startingPoint)
                    startingPoint.Next = point;
            }
        }

        var currentPoint = startingPoint;
        currentPoint.IsVisitedPipe = true;
        Point prevPoint = null;
        do
        {
            if (prevPoint == null || prevPoint != currentPoint.Next)
            {
                prevPoint = currentPoint;
                currentPoint = currentPoint.Next;
            }
            else
            {
                prevPoint = currentPoint;
                currentPoint = currentPoint.Previous;
            }
            currentPoint.IsVisitedPipe = true;
        } while (currentPoint != startingPoint);

        var dict = pointsDictionary.Values.OrderBy(x => x.Y)
                                   .ThenBy(x => x.X)
                                   .ToDictionary(x => x, x => x);
        var maxX = dict.Values.Max(x => x.X);
        var maxT = dict.Values.Max(x => x.Y);
        var currentX = 0.0;
        var currentY = 0.0;
        var direction = Direction.East;
        var rounds = 0.0;
        do
        {
            if(pointsDictionary.TryGetValue(new Point(currentX, currentY), out var point))
            {
                if (point.Value == Connections.None && !point.IsChecked)
                {
                    if (point.X == 0 || point.Y == 0)
                    {
                        point = pointsDictionary[point];
                        point.SetOutside();
                        point.IsChecked = true;
                    }
                    else
                        CheckPoint(pointsDictionary, point, null, 0.5);
                }
            }
            else
            {
                Console.WriteLine();
            }

            switch (direction)
            {
                case Direction.North: 
                    currentY -= 0.5;
                    if (currentY < 0)
                    {
                        currentY = rounds;
                        direction = Direction.East;
                    }
                    break;
                case Direction.East: 
                    currentX += 0.5;
                    if (currentX > maxX)
                    {
                        currentX = maxX - rounds;
                        direction = Direction.South;
                    }
                    break;
                case Direction.South: 
                    currentY += 0.5;
                    if (currentY > maxT)
                    {
                        currentY = maxT - rounds;
                        direction = Direction.West;
                    }
                    break;
                case Direction.West:
                    currentX -= 0.5;
                    if (currentX < 0)
                    {
                        currentX = rounds;
                        direction = Direction.North;
                    }
                    break;
            }
            if (currentX == rounds && currentY == rounds)
            {
                rounds += 0.5;
                currentX = rounds;
                currentY = rounds;
                //Print(pointsDictionary);
                direction = Direction.East;

                Console.WriteLine($"Round: {rounds}");
            }

            if (pointsDictionary.Values.All(x => x.IsChecked || x.IsVisitedPipe))
                break;
        } while (true);

        var result =
            pointsDictionary.Values.Count(x =>  !x.IsOutside && !x.IsVisitedPipe && !x.IsMiddle);
        
        Print(pointsDictionary);
        return new ValueTask<string>(result.ToString());
    }
    private static void Print(Dictionary<Point, Point> pointsDictionary)
    {
        var t = pointsDictionary.Values.Select(x => x.Y)
                                .Distinct()
                                .Order()
                                .ToList();
        var sb = new StringBuilder();
        foreach (var i in t)
        {
            var points = pointsDictionary.Values.Where(x => x.Y == i)
                                         .OrderBy(x => x.X)
                                         .ToList();
            foreach (var point in points)
                sb.Append(point.GetPipe());
            sb.AppendLine();
        }
        var str = sb.ToString();
        Console.WriteLine(str);
        Console.WriteLine("------------------------------------");
        Console.WriteLine();
    }
    private void AddMiddlePoints(Dictionary<Point, Point> pointsDictionary)
    {
        var points = pointsDictionary.Values.ToList();
        foreach (var point in points)
        {
            AddMiddlePoint(pointsDictionary, new Point(point.X, point.Y + 0.5), Direction.South);
            AddMiddlePoint(pointsDictionary, new Point(point.X, point.Y - 0.5), Direction.North);
            AddMiddlePoint(pointsDictionary, new Point(point.X + 0.5, point.Y), Direction.East);
            AddMiddlePoint(pointsDictionary, new Point(point.X - 0.5, point.Y), Direction.West);

            AddEmptyMiddlePoint(pointsDictionary, new Point(point.X + 0.5, point.Y + 0.5));
            AddEmptyMiddlePoint(pointsDictionary, new Point(point.X + 0.5, point.Y - 0.5));
            AddEmptyMiddlePoint(pointsDictionary, new Point(point.X - 0.5, point.Y + 0.5));
            AddEmptyMiddlePoint(pointsDictionary, new Point(point.X - 0.5, point.Y - 0.5));
        }
    }
    private void AddEmptyMiddlePoint(Dictionary<Point, Point> pointsDictionary, Point point)
    {
        if(point.X < 0 || point.Y < 0)
            return;
        
        if (pointsDictionary.ContainsKey(point))
            return;

        point.SetValue(Connections.None);
        pointsDictionary.Add(point, point);
    }
    private void AddMiddlePoint(Dictionary<Point, Point> pointsDictionary, Point point, Direction direction)
    {
        if (pointsDictionary.ContainsKey(point))
            return;

        Point nextPoint = null;
        Point prevPoint = null;

        switch (direction)
        {
            case Direction.North:
                pointsDictionary.TryGetValue(new Point(point.X, point.Y - 0.5), out nextPoint);
                pointsDictionary.TryGetValue(new Point(point.X, point.Y + 0.5), out prevPoint);
                break;
            case Direction.East:
                pointsDictionary.TryGetValue(new Point(point.X + 0.5, point.Y), out nextPoint);
                pointsDictionary.TryGetValue(new Point(point.X - 0.5, point.Y), out prevPoint);
                break;
            case Direction.South:
                pointsDictionary.TryGetValue(new Point(point.X, point.Y + 0.5), out nextPoint);
                pointsDictionary.TryGetValue(new Point(point.X, point.Y - 0.5), out prevPoint);
                break;
            case Direction.West:
                pointsDictionary.TryGetValue(new Point(point.X - 0.5, point.Y), out nextPoint);
                pointsDictionary.TryGetValue(new Point(point.X + 0.5, point.Y), out prevPoint);
                break;
        }

        if (nextPoint == null || prevPoint == null)
        {
            if(point.X < 0 || point.Y < 0)
                return;
            
            pointsDictionary.Add(point, new Point(point.X, point.Y, '.'));
            return;
        }

        if (nextPoint.Value == Connections.None || prevPoint.Value == Connections.None)
        {
            pointsDictionary.Add(point, new Point(point.X, point.Y, '.'));
            return;
        }

        switch (direction)
        {
            case Direction.North:
                point.SetValue(
                    GetConnectionBetweenPointsInDirection(prevPoint.Value, nextPoint.Value, Direction.North));
                break;
            case Direction.East:
                point.SetValue(GetConnectionBetweenPointsInDirection(prevPoint.Value, nextPoint.Value, Direction.East));
                break;
            case Direction.South:
                point.SetValue(
                    GetConnectionBetweenPointsInDirection(prevPoint.Value, nextPoint.Value, Direction.South));
                break;
            case Direction.West:
                point.SetValue(GetConnectionBetweenPointsInDirection(prevPoint.Value, nextPoint.Value, Direction.West));
                break;
        }

        pointsDictionary.Add(point, point);
    }

    private Connections GetConnectionBetweenPointsInDirection(Connections point1, Connections point2,
        Direction direction)
    {
        if (point1 == point2)
        {
            if((direction == Direction.North || direction == Direction.South) && point1 is Connections.NorthSouth)
                return point1;
            
            if((direction == Direction.East || direction == Direction.West) && point1 is Connections.EastWest)
                return point1;
        }

        if (point1 == point2)
            return Connections.None;

        switch (direction)
        {
            case Direction.North:
                if (point1 is Connections.NorthWest or Connections.NorthEast &&
                    point2 is Connections.SouthEast or Connections.SouthWest or Connections.NorthSouth ||
                    point1 is Connections.Starting && point2 is Connections.NorthSouth)
                    return Connections.NorthSouth;

                return Connections.None;
            case Direction.East:
                if ((point1 is Connections.NorthEast or Connections.EastWest or Connections.SouthEast &&
                    point2 is Connections.EastWest or Connections.NorthWest or Connections.SouthWest) ||
                    (point1 is Connections.Starting && point2 is Connections.EastWest or Connections.NorthWest or Connections.SouthWest) ||
                    (point2 is Connections.Starting && point1 is Connections.SouthEast or Connections.NorthEast or Connections.EastWest))
                    return Connections.EastWest;

                return Connections.None;
            case Direction.South:
                if (point1 is Connections.SouthEast or Connections.SouthWest or Connections.NorthSouth 
                    && point2 is Connections.NorthSouth or Connections.NorthWest or Connections.NorthEast or Connections.Starting)
                    return Connections.NorthSouth;

                if (point1 is Connections.Starting && point2 is Connections.NorthSouth or Connections.NorthEast or Connections.NorthWest)
                    return Connections.NorthSouth;
                return Connections.None;
            case Direction.West:
                if (point1 is Connections.NorthWest or Connections.SouthWest &&
                    point2 is Connections.NorthEast or Connections.SouthEast or Connections.EastWest ||
                    point1 is Connections.Starting && point2 is Connections.EastWest)
                    return Connections.EastWest;

                return Connections.None;
            default: throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

    private void CheckPoint(Dictionary<Point, Point> pointsDictionary, Point point, HashSet<Point> visited = null,
        double round = 1, int depth = 0)
    {
        if(depth > 1000)
            return;

        if (visited == null)
            visited = new HashSet<Point>();

        visited.Add(point);
        point.IsChecked = true;
        
        var isOutside = false;

        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X - round, point.Y - round), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X, point.Y - round), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X + round, point.Y - round), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X - round, point.Y), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X + round, point.Y), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X - round, point.Y + round), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X, point.Y + round), depth, round) || isOutside;
        isOutside = ValueIsOutside(pointsDictionary, visited, new Point(point.X + round, point.Y + round), depth, round) || isOutside;

        if (isOutside)
            SetOutside(visited);
    }
    
    private void SetOutside(HashSet<Point> visited)
    {
        foreach (var point in visited)
            point.SetOutside();
    }

    private bool ValueIsOutside(Dictionary<Point, Point> pointsDictionary, HashSet<Point> visited, Point point, int depth, double round = 1)
    {
        if (visited.TryGetValue(point, out var visitedPoint))
            return visitedPoint.IsOutside;

        if (point.X < 0 || point.Y < 0)
        {
            point.IsChecked = true;
            return true;
        }

        if (pointsDictionary.TryGetValue(point, out var currentPoint))
        {
            if (currentPoint.IsVisitedPipe)
            {
                currentPoint.IsChecked = true;
                return false;
            }
            
            if(currentPoint.IsOutside)
                return true;
            
            visited.Add(currentPoint);
            currentPoint.IsChecked = true;

            CheckPoint(pointsDictionary, currentPoint, visited, round, depth + 1);
        }
        else
        {
            point.SetOutside();
        }

        return point.IsChecked;
    }

    [DebuggerDisplay("x = {X}, y = {Y}, orig = {OriginalValue}, value = {Value}")]
    private sealed class Point
    {
        public double X { get; }
        public double Y { get; }
        public char OriginalValue { get; private set; }
        public Connections Value { get; private set; }
        public bool IsVisitedPipe { get; set; }
        public bool IsOutside { get; private set; }
        public bool IsChecked { get; set; }
        public Point Next { get; set; }
        public Point Previous { get; set; }
        public bool IsMiddle { get; set; }
        public Point(double x, double y, char value)
        {
            X = x;
            Y = y;
            OriginalValue = value;
            Value = GetConnection(value);
            IsMiddle = x % 1 != 0 || y % 1 != 0;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Value = Connections.None;
            OriginalValue = ' ';
            IsMiddle = x % 1 != 0 || y % 1 != 0;
        }
        
        public void SetOutside()
        {
            IsOutside = true;
            IsChecked = true;
        }
        
        public Point GetNext(double round = 1)
        {
            return Value switch
            {
                Connections.None => null,
                Connections.Starting => null,
                Connections.NorthSouth => new Point(X, Y + round),
                Connections.EastWest => new Point(X + round, Y),
                Connections.NorthEast => new Point(X, Y - round),
                Connections.NorthWest => new Point(X, Y - round),
                Connections.SouthWest => new Point(X, Y + round),
                Connections.SouthEast => new Point(X, Y + round),
            };
        }

        public Point GetPrev(double round = 1)
        {
            return Value switch
            {
                Connections.None => null,
                Connections.Starting => null,
                Connections.NorthSouth => new Point(X, Y - round),
                Connections.EastWest => new Point(X - round, Y),
                Connections.NorthEast => new Point(X + round, Y),
                Connections.NorthWest => new Point(X - round, Y),
                Connections.SouthWest => new Point(X - round, Y),
                Connections.SouthEast => new Point(X + round, Y),
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is Point point)
                return X == point.X && Y == point.Y;

            return false;
        }

        public static bool operator ==(Point left, Point right)
        {
            return left?.Equals(right) ?? right is null;
        }
        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public string GetPipe()
        {
            return Value switch
            {
                Connections.None => IsOutside ? " " : "0",
                Connections.Starting => "S",
                Connections.NorthSouth => "│",
                Connections.EastWest => "─",
                Connections.NorthEast => "└",
                Connections.NorthWest => "┘",
                Connections.SouthWest => "┐",
                Connections.SouthEast => "┌",
            };
        }

        public void SetValue(Connections value)
        {
            Value = value;
        }

        private Connections GetConnection(char value)
        {
            return value switch
            {
                '.' => Connections.None,
                'S' => Connections.Starting,
                '|' => Connections.NorthSouth,
                '-' => Connections.EastWest,
                'L' => Connections.NorthEast,
                'J' => Connections.NorthWest,
                '7' => Connections.SouthWest,
                'F' => Connections.SouthEast,
            };
        }
    }

    private enum Connections
    {
        None,
        Starting,
        NorthSouth,
        EastWest,
        NorthEast,
        NorthWest,
        SouthWest,
        SouthEast
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }
}
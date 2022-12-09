using System;

var input = File.ReadAllText("input.txt");

var pairs = input.Split("\r\n")
    .Select(x => x.Split(","))
    .Select(x => new { Left = x[0].Split("-").Select(int.Parse).ToList(), Right = x[1].Split("-").Select(int.Parse).ToList() })
    .ToList();

var ranges = pairs.Select(x => new { Left = Enumerable.Range(x.Left[0], x.Left[1] - x.Left[0] + 1).ToList(), Right = Enumerable.Range(x.Right[0], x.Right[1] - x.Right[0] + 1).ToList() })
    .ToList();

var intersections = ranges
    .Select(x => new { x.Left, x.Right, Intersection = x.Left.Intersect(x.Right) })
    .Select(x => new { x.Left, x.Right, Intersection = x.Intersection.ToList() })
    .Count(x => x.Left.Count == x.Intersection.Count || x.Right.Count == x.Intersection.Count);

var intersections2 = ranges
    .Select(x => new { x.Left, x.Right, Intersection = x.Left.Intersect(x.Right) })
    .Select(x => new { x.Left, x.Right, Intersection = x.Intersection.ToList() })
    .Count(x => x.Intersection.Count > 0);

Console.WriteLine(intersections);
Console.WriteLine(intersections2);

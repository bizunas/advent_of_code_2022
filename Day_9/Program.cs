
using System;
using System.Diagnostics;

var input = File.ReadAllLines("input.txt").Select(x => x.Split(" ")).Select(x => (ConvertToDirection(x[0]), int.Parse(x[1]))).ToList();


Dictionary<(int, int), int> tailVisitedPositions =  new Dictionary<(int, int), int>() { { (0, 0), 1 } };

var headPosition = new Position(0, 0);
var tailPositions = Enumerable.Range(1, 9).Select(x => new Position(0, 0)).ToArray();

foreach (var (direction, quantity) in input)
{
    for(var i = 0; i < quantity; i++)
    {
        headPosition.UpdatePosition(direction);
        var previousFolowingPosition = headPosition;

        for (var tailIndex = 0; tailIndex < tailPositions.Length; tailIndex++)
        {
            var tailPosition = tailPositions[tailIndex];
            var tailSeparatedX = previousFolowingPosition.X - tailPosition.X;
            var tailSeparatedY = previousFolowingPosition.Y - tailPosition.Y;
            var diffX = Math.Abs(tailSeparatedX);
            var diffY = Math.Abs(tailSeparatedY);

            if (diffX + diffY > 2)
            {
                tailPosition.X += Math.Sign(tailSeparatedX);
                tailPosition.Y += Math.Sign(tailSeparatedY);
            }
            else if(diffX > 1 || diffY > 1)
            {
                tailPosition.X += (diffX - 1) * Math.Sign(tailSeparatedX);
                tailPosition.Y += (diffY - 1) * Math.Sign(tailSeparatedY);
            }

            if (tailIndex == 8)
            {
                AddVisitedTailPosition(tailPosition);
            }

            previousFolowingPosition = tailPosition;
        }
    }
}

Console.WriteLine(tailVisitedPositions.Keys.Count);

void AddVisitedTailPosition(Position position)
{
    var currentTailPosition = (position.X, position.Y);
    if (tailVisitedPositions.ContainsKey(currentTailPosition))
    {
        tailVisitedPositions[currentTailPosition] += 1;
    }
    else
    {
        tailVisitedPositions.Add(currentTailPosition, 1);
    }
}

static Direction ConvertToDirection(string input)
{
    return input switch
    {
        "L" => Direction.Left,
        "R" => Direction.Right,
        "U" => Direction.Up,
        "D" => Direction.Down,
        _ => throw new NotImplementedException(),
    };
}


[DebuggerDisplay("X: {X}, Y: {Y}")]
public class Position
{
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }

    public (int X, int Y) UpdatePosition(Direction direction)
    {
        X += direction switch
        {
            Direction.Left => -1,
            Direction.Right => 1,
            _ => 0,
        };
        Y += direction switch
        {
            Direction.Up => -1,
            Direction.Down => 1,
            _ => 0,
        };

        return (X, Y);
    }
}
public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}
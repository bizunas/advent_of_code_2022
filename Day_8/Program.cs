using System;

var input = File.ReadAllLines("input.txt");

short[][] grid = input.Select(x => x.Select(y => short.Parse(y.ToString())).ToArray()).ToArray();
var columnCount = grid[0].Length;
int[,] scenicScore = new int[grid.Length, columnCount];

int visibleTreeCount = grid.Length * 2 + (grid[0].Length - 2) * 2;
int highestScenicScore = 0;

for(short rowIndex = 1; rowIndex < grid.Length - 1; rowIndex++)
{
    for (short columnIndex = 1; columnIndex < grid[rowIndex].Length - 1; columnIndex++)
    {
        //Console.WriteLine($"({rowIndex}, {columnIndex}) => {grid[rowIndex][columnIndex]}");
        var isVisibleAnydirection = IsVisibleOutside(rowIndex, columnIndex, grid);
        visibleTreeCount += isVisibleAnydirection ? 1 : 0;
        scenicScore[rowIndex, columnIndex] = VisibleTrees(rowIndex, columnIndex, grid);
        if (scenicScore[rowIndex, columnIndex] > highestScenicScore)
        {
            highestScenicScore = scenicScore[rowIndex, columnIndex];
        }
    }
}

Console.WriteLine(visibleTreeCount);
Console.WriteLine(highestScenicScore);

static bool IsVisibleOutside(short rowIndex, short columnIndex, short[][] grid)
{
    var directions = IsVisibleOutsideTop(rowIndex, columnIndex, grid)
        | IsVisibleOutsideBottom(rowIndex, columnIndex, grid)
        | IsVisibleOutsideLeft(rowIndex, columnIndex, grid)
        | IsVisibleOutsideRight(rowIndex, columnIndex, grid);

    return directions > 0;
}

static int VisibleTrees(short rowIndex, short columnIndex, short[][] grid)
{
    var top = VisibleOutsideTop(rowIndex, columnIndex, grid);
    var bottom = VisibleOutsideBottom(rowIndex, columnIndex, grid);
    var left = VisibleOutsideLeft(rowIndex, columnIndex, grid);
    var right = VisibleOutsideRight(rowIndex, columnIndex, grid);
    return top * bottom * left * right;
}

static Direction IsVisibleOutsideTop(short rowIndex, short columnIndex, short[][] grid)
{
    return grid[0..rowIndex].Select(row => row[columnIndex]).All(x => x < grid[rowIndex][columnIndex]) ? Direction.Top : Direction.None;
}

static Direction IsVisibleOutsideBottom(short rowIndex, short columnIndex, short[][] grid)
{
    return grid[(rowIndex + 1)..grid.Length].Select(row => row[columnIndex]).All(x => x < grid[rowIndex][columnIndex]) ? Direction.Bottom : Direction.None;
}

static Direction IsVisibleOutsideLeft(short rowIndex, short columnIndex, short[][] grid)
{
    return grid[rowIndex][0..columnIndex].All(x => x < grid[rowIndex][columnIndex]) ? Direction.Left : Direction.None;
}

static Direction IsVisibleOutsideRight(short rowIndex, short columnIndex, short[][] grid)
{
    return grid[rowIndex][(columnIndex + 1)..grid[rowIndex].Length].All(x => x < grid[rowIndex][columnIndex]) ? Direction.Right : Direction.None;
}

static int VisibleOutsideTop(short rowIndex, short columnIndex, short[][] grid)
{
    var treesInLine = grid[0..rowIndex].Select(row => row[columnIndex]).Reverse();
    var treesVisibleCount = treesInLine.TakeWhile(x => x < grid[rowIndex][columnIndex]).Count();
    return (treesInLine.Count() - treesVisibleCount > 0) ? treesVisibleCount + 1 : treesVisibleCount;
}

static int VisibleOutsideBottom(short rowIndex, short columnIndex, short[][] grid)
{
    var treesInLine = grid[(rowIndex + 1)..grid.Length].Select(row => row[columnIndex]);
    var treesVisibleCount = treesInLine.TakeWhile(x => x < grid[rowIndex][columnIndex]).Count();
    return (treesInLine.Count() - treesVisibleCount > 0) ? treesVisibleCount + 1 : treesVisibleCount;
}

static int VisibleOutsideLeft(short rowIndex, short columnIndex, short[][] grid)
{
    var treesInLine = grid[rowIndex][0..columnIndex].Reverse();
    var treesVisibleCount = treesInLine.TakeWhile(x => x < grid[rowIndex][columnIndex]).Count();
    return (treesInLine.Count() - treesVisibleCount > 0) ? treesVisibleCount + 1 : treesVisibleCount;
}

static int VisibleOutsideRight(short rowIndex, short columnIndex, short[][] grid)
{
    var treesInLine = grid[rowIndex][(columnIndex + 1)..grid[rowIndex].Length];
    var treesVisibleCount = treesInLine.TakeWhile(x => x < grid[rowIndex][columnIndex]).Count();
    return (treesInLine.Count() - treesVisibleCount > 0) ? treesVisibleCount + 1 : treesVisibleCount;
}

[Flags]
public enum Direction
{
    None = 0,
    Top = 1,
    Bottom = 2,
    Left = 4,
    Right = 8,
}
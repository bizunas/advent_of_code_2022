using Newtonsoft.Json;
using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt").Split("\r\n");

var rawStacks = input.TakeWhile(x => x.Replace(" ", "").StartsWith("[")).Reverse().ToList();
var stackCount = int.Parse(input.Skip(rawStacks.Count).First().Split(" ").Where(x => x != "").Last());
var rawMoves = input.Skip(rawStacks.Count + 2).ToList();


var stacks = new List<Stack<char>>();
var stacks2 = new List<Stack<char>>();

for (int i = 0; i < stackCount; i++)
{
    var stack = new Stack<char>();
    var stack2 = new Stack<char>();
    foreach (var rawStack in rawStacks)
    {
        var letter = rawStack[i + 1 + (i * 3)];
        if (letter != ' ')
        {
            stack.Push(letter);
            stack2.Push(letter);
        }
    }
    stacks.Add(stack);
    stacks2.Add(stack2);
}


var moveRegex = new Regex("move (\\d+) from (\\d) to (\\d)", RegexOptions.Compiled);

var moves = rawMoves.Select(x => moveRegex.Match(x)).Select(x => new { Quantity = int.Parse(x.Groups[1].Value), FromStack = int.Parse(x.Groups[2].Value), ToStack = int.Parse(x.Groups[3].Value) }).ToList();

Console.WriteLine(stacks);
Console.WriteLine(moves);

foreach(var (move, index) in moves.Select((x,i) => (x,i)))
{
    for (int i = 0; i < move.Quantity; i++)
    {
        stacks[move.ToStack - 1].Push(stacks[move.FromStack - 1].Pop());
    }
}

Console.WriteLine(string.Join("", stacks.Select(x => x.Peek())));


foreach (var (move, index) in moves.Select((x, i) => (x, i)))
{
    var letters = new List<char>();
    for (int i = 0; i < move.Quantity; i++)
    {
        letters.Add(stacks2[move.FromStack - 1].Pop());
    }

    letters.Reverse();

    foreach(var letter in letters)
    {
        stacks2[move.ToStack - 1].Push(letter);
    }
}

Console.WriteLine(string.Join("", stacks2.Select(x => x.Peek())));

var input = File.ReadAllText("input.txt");
var inputByElves = input.Split("\r\n\r\n");
var elves = new List<int>();


foreach(var elfText in inputByElves)
{
    elves.Add(elfText.Split("\r\n").Select(int.Parse).Sum());
}

Console.WriteLine(string.Join("\r\n", elves.Select((x, index) => (x, index)).OrderByDescending(x => x.x)));
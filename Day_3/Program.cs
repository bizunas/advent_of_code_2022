var input = File.ReadAllText("input.txt");

var lines = input.Split("\r\n");

var parts = lines.Select(line => new { Left = line.Substring(0, line.Length / 2), Right = line.Substring(line.Length / 2) }).Select((x, i) => new { x.Left, x.Right, DuplicateLetter = FindDuplicate(i, x.Left, x.Right) }).ToList();
var parts2 = lines.Chunk(3).Select(x => string.Join("", x[0].Intersect(x[1]).Intersect(x[2])).First()).ToList();

Console.WriteLine(parts.Select(x => x.DuplicateLetter).Select(x => x < 97 ? x - 38 : x - 96).Sum());
Console.WriteLine(parts2.Select(x => x < 97 ? x - 38 : x - 96).Sum());


static char FindDuplicate(int i, string left, string right)
{
    foreach (var leftLetter in left)
    {
        foreach (var rightLetter in right)
        {
            if (leftLetter == rightLetter)
            {
                return leftLetter;
            }
        }
    }

    throw new NotImplementedException();
}

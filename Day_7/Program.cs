using System.Diagnostics;

var input = File.ReadAllText("input.txt");

var lines  = input.Split("\r\n");
var fileSystem = new Item { Name = "/" };
Item currentDir = fileSystem;

var systemSize = 70000000;
var minRequiredSize = 30000000;

foreach (var line in lines)
{
    if (line.StartsWith("$"))
    {
        var commandParts = line.Split(" ");
        var command = commandParts[1];
        var argument = commandParts.Length > 2 ? commandParts[2] : null;

        if (command == "cd" && argument == "/")
        {
            currentDir = fileSystem;
            continue;
        }

        if (command == "cd" && argument == "..")
        {
            currentDir = currentDir.Parent;
            continue;
        }

        if (command == "cd")
        {
            currentDir = currentDir.Items.First(x => x.Name == argument);
            continue;
        }

        if (command == "ls")
        {
            continue;
        }

        throw new NotImplementedException();
    }

    var lsOutputLine = line.Split(" ");
    var sizeOrDir = lsOutputLine[0];
    var name = lsOutputLine[1];

    if (sizeOrDir == "dir")
    {
        currentDir.Items.Add(new Item { Name = name, Parent = currentDir });
        continue;
    }

    currentDir.Items.Add(new Item { Name = name, Parent = currentDir, Size = int.Parse(sizeOrDir), Type = ItemType.File });
}

var totalSizes_1 = fileSystem.GetDescendantDirectories().Where(x => x.CalculateSize() <= 100000).Sum(x => x.CalculateSize());

Console.WriteLine(fileSystem.ToStructureString(0));
Console.WriteLine(totalSizes_1);

var allDirectories = fileSystem.GetDescendantDirectories().Select(x => new { x.Name, Size = x.CalculateSize() }).OrderBy(x => x.Size);

var allDirectoriesString = string.Join("\r\n", allDirectories.Select(x => $"Name: {x.Name}, Size: {x.Size}").ToList());

var availableSize = systemSize - fileSystem.CalculateSize();

var sizeToDelete = minRequiredSize - availableSize;

Console.WriteLine(allDirectoriesString);
Console.WriteLine(allDirectories.First(x => x.Size > sizeToDelete));

public class Item
{
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public Item Parent { get; set; }
    public List<Item> Items { get; set; } = new List<Item>();
    public long Size { get; set; }
    public long CalculateSize() { return Type == ItemType.File ? Size : Items.Sum(x => x.CalculateSize()); }

    public override string ToString()
    {
        return ToStructureString(0);
    }

    public List<Item> GetDescendantDirectories()
    {
        if (Type == ItemType.Directory && !Items.Any(x => x.Type == ItemType.Directory))
        {
            return new() { this };
        }

        if (Type == ItemType.Directory)
        {
            return Items.SelectMany(x => x.GetDescendantDirectories()).Concat(new List<Item>() { this }).ToList();
        }

        return new();
    }

    public string ToStructureString(int level)
    {
        var self = $"{string.Concat(Enumerable.Repeat("  ", level))}- {Name} ({(Type == ItemType.Directory ? "dir" : "file, size=" + Size)}) = {CalculateSize()}\r\n";
        var items = string.Concat(Items.Select(x => x.ToStructureString(level + 1)).ToList());
        return self + items;
    }
}

public enum ItemType
{
    Directory,
    File,
}
var input = File.ReadAllText("input.txt");

var inputSpan = input.AsSpan();
for(int i = 0; i < input.Length; i++)
{
    if (IsUniqueCharacters(inputSpan[i..(i + 14)]))
    {
        Console.WriteLine(i + 14);
        break;
    }
}


static bool IsUniqueCharacters(ReadOnlySpan<char> str)
{

    // If at any time we encounter 2
    // same characters, return false
    for (int i = 0; i < str.Length; i++)
        for (int j = i + 1; j < str.Length; j++)
            if (str[i] == str[j])
                return false;

    // If no duplicate characters
    // encountered, return true
    return true;
}
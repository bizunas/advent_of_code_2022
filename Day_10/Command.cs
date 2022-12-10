using System.Diagnostics;

namespace Day_10;

public class Command
{
    public Instruction Instruction { get; set; }
    public int Argument { get; set; }

    public override string ToString()
    {
        return $"{Instruction} {(Argument != 0 ? Argument : "")}";
    }
}

namespace Day_10;

public class InstructionProcessor
{
    private Dictionary<Instruction, int> InstructionCycleMap = new Dictionary<Instruction, int>() { { Instruction.None, 0 }, { Instruction.Noop, 1 }, { Instruction.Addx, 2 } };
    public Command[] Commands { get; init; }
    public int CurrentInstructionIndex { get; set; } = -1;
    public Command? CurrentInstruction => CurrentInstructionIndex < Commands.Length ? Commands[CurrentInstructionIndex] : null;
    public bool HasNext => CurrentInstructionIndex < Commands.Length;
    public int CurrentInstructionCycles => CurrentInstruction?.Instruction is not null ? InstructionCycleMap[CurrentInstruction.Instruction] : -1;

    public InstructionProcessor(string[] input)
    {
        Commands = input.Select(ParseInstruction).ToArray();
    }



    public Command? NextInstruction()
    {
        CurrentInstructionIndex++;
        return CurrentInstruction;
    }

    static Command ParseInstruction(string input)
    {
        var commandStr = input.Split(" ")[0];
        var argumentStr = input.IndexOf(" ") > -1 ? input.Split(" ")[1] : null;
        var command = commandStr switch
        {
            "addx" => Instruction.Addx,
            "noop" => Instruction.Noop,
            _ => throw new NotImplementedException()
        };
        var argument = argumentStr is null ? 0 : int.Parse(argumentStr);
        return new Command { Instruction = command, Argument = argument };
    }
}

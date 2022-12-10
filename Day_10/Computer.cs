namespace Day_10;

public class Computer
{
    public (int, string) Compute(string[] input)
    {
        var totals = new List<int>();
        var crtOutput = "";

        var instructionProcessor = new InstructionProcessor(input);
        var clockCycle = 1;
        var processingInstructionCycle = 1;
        var register = 1;
        var command = instructionProcessor.NextInstruction();

        if (register - 1 <= (clockCycle % 40) && (clockCycle % 40) <= register + 1)
        {
            crtOutput += '#';
        }
        else
        {
            crtOutput += '.';
        }

        while (command is not null)
        {
            var instructionCycle = instructionProcessor.CurrentInstructionCycles;

            if ((clockCycle - 20) % 40 == 0)
            {
                totals.Add(clockCycle * register);
            }

            if (instructionCycle == processingInstructionCycle)
            {
                register += command?.Argument ?? 0;
                command = instructionProcessor.NextInstruction();
                processingInstructionCycle = 0;
            }

            if (register - 1 <= (clockCycle % 40) && (clockCycle % 40) <= register + 1)
            {
                crtOutput += '#';
            }
            else
            {
                crtOutput += '.';
            }

            processingInstructionCycle++;
            clockCycle++;

            if (clockCycle == 240)
            {
                break;
            }

            if (clockCycle % 40 == 0)
            {
                crtOutput += "\r\n";
            }
        }

        return (totals.Sum(), crtOutput);
    }
}

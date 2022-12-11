using System.Diagnostics;

namespace Day_11
{
    public class Monkey
    {
        public List<long> Items { get; set; } = new List<long>();
        public Func<long, long> Operation { get; set; }
        public Func<long, int> Test { get; set; }
        public int DivisibleBy { get; set; }
        public int TestSuccessMonkeyIndex { get; set; }
        public int TestFailMonkeyIndex { get; set; }
        public Func<long, long> WorryLevelReliever { get; set; }
        public long ModuloReliever { get; set; }

        public Monkey(List<string> monkeyLines, Func<long, long> worryLevelReliever)
        {
            Items = monkeyLines[1].Split(": ")[1].Split(",").Select(long.Parse).ToList();
            Operation = BuildOperationFunction(monkeyLines[2]);
            Test = BuildTestFunction(monkeyLines[3]);
            TestSuccessMonkeyIndex = int.Parse(monkeyLines[4].Split("monkey ")[1]);
            TestFailMonkeyIndex = int.Parse(monkeyLines[5].Split("monkey ")[1]);
            WorryLevelReliever = worryLevelReliever;
        }

        public List<(long Item, int MonkeyIndex)> Process()
        {
            return Items.Select(ProcessItem).ToList();
        }

        private (long Item, int MonkeyIndex) ProcessItem(long item)
        {
            long observableItemValue = Operation(item);
            observableItemValue = WorryLevelReliever(observableItemValue) % ModuloReliever;

            return (observableItemValue, Test(observableItemValue));
        }

        private Func<long, int> BuildTestFunction(string testText)
        {
            DivisibleBy = int.Parse(testText.Split("by ")[1]);
            return (x) => x % DivisibleBy == 0 ? TestSuccessMonkeyIndex : TestFailMonkeyIndex;
        }

        private Func<long, long> BuildOperationFunction(string operationText)
        {
            var operationParts = operationText.Split("old ")[1].Split(" ");
            var operationType = operationParts[0];
            var operationArgument = operationParts[1] == "old" ? (int?)null : int.Parse(operationParts[1]);

            return operationType switch
            {
                "+" => (x) => x + (operationArgument is null ? x : operationArgument.Value),
                "/" => (x) => x / (operationArgument is null ? x : operationArgument.Value),
                "-" => (x) => x - (operationArgument is null ? x : operationArgument.Value),
                "*" => (x) => x * (operationArgument is null ? x : operationArgument.Value),
                _ => throw new NotImplementedException()
            };
        }

        public override string ToString()
        {
            return $"Items: {string.Join(", ", Items)}";
        }
    }
}
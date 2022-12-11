using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Day_11;

public class KeepAwayGame
{
	public List<Monkey> Monkeys { get; set; }


    public KeepAwayGame(string input, Func<long, long> worryLevelReliever)
	{
		Monkeys = ParseMonkeys(input, worryLevelReliever);
    }

    private List<Monkey> ParseMonkeys(string input, Func<long, long> worryLevelReliever)
    {
		var monkeys = new List<Monkey>();
		foreach (string monkeyInput in input.Split("\r\n\r\n")) 
		{
			var monkeyLines = monkeyInput.Split("\r\n").Select(x => x.Trim()).ToList();
			monkeys.Add(new Monkey(monkeyLines, worryLevelReliever));
		}

        foreach (var monkey in monkeys)
        {
            monkey.ModuloReliever = monkeys.Select(y => y.DivisibleBy).Aggregate(1, (a, b) => a * b);
        }

		return monkeys;
    }

	public List<long> PlayRaw(int iterations)
	{
        var totalInspections = new Dictionary<int, long>(Monkeys.Select((x, i) => i).ToDictionary(x => x, x => (long)0));
        for (int j = 0; j < iterations; j++)
        {
            for (int i = 0; i < Monkeys.Count; i++)
            {
                Monkey? monkey = Monkeys[i];
                var monkeyOutput = monkey.Process();
                totalInspections[i] += monkeyOutput.Count;
                monkey.Items = new();

                foreach (var (Item, MonkeyIndex) in monkeyOutput)
                {
                    Monkeys[MonkeyIndex].Items.Add(Item);
                }
            }
        }

        return totalInspections.Select(x => x.Value).ToList();
    }

	public long Play(int iterations)
	{
        var totalInspections = PlayRaw(iterations);

        return totalInspections.OrderByDescending(x => x).Take(2).Aggregate((a, b) => a * b);
	}
}

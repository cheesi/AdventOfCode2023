using AoCHelper;

namespace AdventOfCode2023;

public class Day09 : BaseDay
{
    private readonly string _input;

    public Day09()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day09(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var nextHistoryEntries = new List<long>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var historyCalculationStack = new Stack<long[]>();
            var currentHistoryCalculation = line.Split(' ').Select(long.Parse).ToArray();
            var temp = new long[currentHistoryCalculation.Length + 1];
            Array.Copy(currentHistoryCalculation, temp, currentHistoryCalculation.Length);
            currentHistoryCalculation = temp;
            historyCalculationStack.Push(currentHistoryCalculation);
            while (currentHistoryCalculation.Any(x => x != 0))
            {
                var parent = currentHistoryCalculation;
                currentHistoryCalculation = new long[currentHistoryCalculation.Length - 1];
                for (int i = 0; i < parent.Length - 2; i++)
                {
                    var number1 = parent[i];
                    var number2 = parent[i + 1];

                    currentHistoryCalculation[i] = number2 - number1;
                }

                historyCalculationStack.Push(currentHistoryCalculation);
            }

            while (historyCalculationStack.TryPop(out currentHistoryCalculation))
            {
                if (historyCalculationStack.TryPeek(out var parent))
                {
                    parent[^1] = currentHistoryCalculation[^1] + parent[^2];
                }
                else
                {
                    nextHistoryEntries.Add(currentHistoryCalculation[^1]);
                }
            }
        }

        return new ValueTask<string>(nextHistoryEntries.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var nextHistoryEntries = new List<long>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var historyCalculationStack = new Stack<long[]>();
            var currentHistoryCalculation = line.Split(' ').Select(long.Parse).ToArray();
            var temp = new long[currentHistoryCalculation.Length + 1];
            Array.Copy(currentHistoryCalculation, 0, temp, 1, currentHistoryCalculation.Length);
            currentHistoryCalculation = temp;
            historyCalculationStack.Push(currentHistoryCalculation);
            while (currentHistoryCalculation.Any(x => x != 0))
            {
                var parent = currentHistoryCalculation;
                currentHistoryCalculation = new long[currentHistoryCalculation.Length - 1];
                for (int i = 1; i < parent.Length - 1; i++)
                {
                    var number1 = parent[i];
                    var number2 = parent[i + 1];

                    currentHistoryCalculation[i] = number2 - number1;
                }

                historyCalculationStack.Push(currentHistoryCalculation);
            }

            while (historyCalculationStack.TryPop(out currentHistoryCalculation))
            {
                if (historyCalculationStack.TryPeek(out var parent))
                {
                    parent[0] = parent[1] - currentHistoryCalculation[0];
                }
                else
                {
                    nextHistoryEntries.Add(currentHistoryCalculation[0]);
                }
            }
        }

        return new ValueTask<string>(nextHistoryEntries.Sum().ToString());
    }
}

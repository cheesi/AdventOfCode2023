using AoCHelper;

namespace AdventOfCode2023;

public class Day06 : BaseDay
{
    private readonly string _input;

    public Day06()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day06(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var times = Array.Empty<int>();
        var distances = Array.Empty<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var indexOfColon = line.IndexOf(':');
            var truncatedInput = line[(indexOfColon + 1)..];
            var lineParts = truncatedInput.Split(' ',
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (line.StartsWith("Time"))
            {
                times = lineParts.Select(int.Parse).ToArray();
            }
            else if (line.StartsWith("Distance"))
            {
                distances = lineParts.Select(int.Parse).ToArray();
            }
        }

        var sum = 1;

        for (int i = 0; i < times.Length; i++)
        {
            var time = times[i];
            var distance = distances[i];

            var winCounter = 0;

            for (int j = 0; j <= time; j++)
            {
                var timeHoldingButton = j;
                var timeLeft = time - timeHoldingButton;
                var distanceTravel = timeLeft * timeHoldingButton;

                if (distanceTravel > distance)
                {
                    winCounter++;
                }
            }

            sum *= winCounter;
        }

        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        long time = 0;
        long distance = 0;

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var indexOfColon = line.IndexOf(':');
            var truncatedInput = line[(indexOfColon + 1)..];
            var spacesRemovedInput = truncatedInput.Replace(" ", string.Empty);

            if (line.StartsWith("Time"))
            {
                time = long.Parse(spacesRemovedInput);
            }
            else if (line.StartsWith("Distance"))
            {
                distance = long.Parse(spacesRemovedInput);
            }
        }

        long winCounter = 0;

        for (long j = 0; j <= time; j++)
        {
            var timeHoldingButton = j;
            var timeLeft = time - timeHoldingButton;
            var distanceTravel = timeLeft * timeHoldingButton;

            if (distanceTravel > distance)
            {
                winCounter++;
            }
        }

        return new ValueTask<string>(winCounter.ToString());
    }
}

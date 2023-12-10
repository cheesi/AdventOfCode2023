using AoCHelper;

namespace AdventOfCode2023;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day02(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var possibleGameIds = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var truncatedInput = line[5..];
            var indexOfColon = truncatedInput.IndexOf(':');
            var gameId = int.Parse(truncatedInput[..(indexOfColon)]);

            truncatedInput = truncatedInput[(indexOfColon + 2)..];
            var setsOfCubes = truncatedInput.Split("; ");

            var failedGame = false;

            foreach (var setOfCubes in setsOfCubes)
            {
                var cubeRecords = setOfCubes.Split(", ");
                foreach (var cubeRecord in cubeRecords)
                {
                    var cubeRecordParts = cubeRecord.Split(' ');
                    var numberOfCubes = int.Parse(cubeRecordParts[0]);
                    var cubeColor = cubeRecordParts[1];

                    var limit = cubeColor switch
                    {
                        "red" => 12,
                        "green" => 13,
                        "blue" => 14
                    };

                    if (numberOfCubes > limit)
                    {
                        failedGame = true;
                        break;
                    }
                }

                if (failedGame)
                {
                    break;
                }
            }

            if (!failedGame)
            {
                possibleGameIds.Add(gameId);
            }
        }

        return new(possibleGameIds.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var powerOfSets = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var truncatedInput = line[5..]; //1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var indexOfColon = truncatedInput.IndexOf(':');
            var gameId = int.Parse(truncatedInput[..(indexOfColon)]);

            truncatedInput = truncatedInput[(indexOfColon + 2)..]; //3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var setsOfCubes = truncatedInput.Split("; ");

            var minimumCubesRed = 0;
            var minimumCubesGreen = 0;
            var minimumCubesBlue = 0;

            foreach (var setOfCubes in setsOfCubes)
            {
                var cubeRecords = setOfCubes.Split(", ");
                foreach (var cubeRecord in cubeRecords)
                {
                    var cubeRecordParts = cubeRecord.Split(' ');
                    var numberOfCubes = int.Parse(cubeRecordParts[0]);
                    var cubeColor = cubeRecordParts[1];

                    switch (cubeColor)
                    {
                        case "red":
                            minimumCubesRed = Math.Max(minimumCubesRed, numberOfCubes);
                            break;
                        case "green":
                            minimumCubesGreen = Math.Max(minimumCubesGreen, numberOfCubes);
                            break;
                        case "blue":
                            minimumCubesBlue = Math.Max(minimumCubesBlue, numberOfCubes);
                            break;
                    }
                }
            }

            var powerOfCubes = minimumCubesRed * minimumCubesBlue * minimumCubesGreen;
            powerOfSets.Add(powerOfCubes);
        }

        return new(powerOfSets.Sum().ToString());
    }
}

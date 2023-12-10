using AoCHelper;

namespace AdventOfCode2023;

public class Day05 : BaseDay
{
    private readonly string _input;

    public Day05()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day05(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var seeds = Enumerable.Empty<long>();
        var currentSeedCalculation = new Dictionary<long, long>();

        var currentMapping = new List<MappingDefinition>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.StartsWith("seeds: "))
            {
                seeds = line[6..]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(long.Parse);

                foreach (var seed in seeds)
                {
                    currentSeedCalculation.Add(seed, seed);
                }
            }
            else if (line == string.Empty)
            {
                continue;
            }
            else if (line.Contains("map"))
            {
                // do transformation
                foreach (var seed in currentSeedCalculation)
                {
                    var mapping = currentMapping
                        .SingleOrDefault(mapping => seed.Value >= mapping.sourceRangeStart &&
                                                    seed.Value <= mapping.sourceRangeStart + mapping.rangeLength - 1);
                    if (mapping is not null)
                    {
                        var newValue = mapping.destinationRangeStart + (seed.Value - mapping.sourceRangeStart);
                        currentSeedCalculation[seed.Key] = newValue;
                    }
                }

                currentMapping.Clear();
            }
            else
            {
                var mappings = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(long.Parse).ToArray();

                currentMapping.Add(new MappingDefinition(mappings[0], mappings[1], mappings[2]));
            }
        }

        foreach (var seed in currentSeedCalculation)
        {
            var mapping = currentMapping
                .SingleOrDefault(mapping => seed.Value >= mapping.sourceRangeStart &&
                                            seed.Value <= mapping.sourceRangeStart + mapping.rangeLength - 1);
            if (mapping is not null)
            {
                var newValue = mapping.destinationRangeStart + (seed.Value - mapping.sourceRangeStart);
                currentSeedCalculation[seed.Key] = newValue;
            }
        }

        return new(currentSeedCalculation.Select(x => x.Value).Min().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var seeds = Array.Empty<long>();

        var mappings = new List<List<MappingDefinition>>();
        var currentMappings = new List<MappingDefinition>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (line.StartsWith("seeds: "))
            {
                seeds = line[6..]
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(long.Parse)
                    .ToArray();
            }
            else if (line == string.Empty)
            {
                continue;
            }
            else if (line.Contains("map"))
            {
                if (currentMappings.Count > 0)
                {
                    mappings.Add(currentMappings);
                }
                currentMappings = new List<MappingDefinition>();
                continue;
            }
            else
            {
                var currentMapping = line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(long.Parse).ToArray();

                currentMappings.Add(new MappingDefinition(currentMapping[0], currentMapping[1], currentMapping[2]));
            }
        }

        mappings.Add(currentMappings);

        var minValue = long.MaxValue;
        for (int i = 0; i < seeds.Count(); i = i + 2)
        {
            Parallel.For(seeds[i], seeds[i] + seeds[i + 1], j =>
            {
                foreach (var currentMapping in mappings)
                {
                    var mapping = currentMapping
                        .FirstOrDefault(mapping => j >= mapping.sourceRangeStart &&
                                                    j <= mapping.sourceRangeStart + mapping.rangeLength - 1);
                    if (mapping is not null)
                    {
                        var newValue = mapping.destinationRangeStart + (j - mapping.sourceRangeStart);
                        j = newValue;
                    }
                }

                if (j < minValue)
                {
                    minValue = j;
                }
            });
        }

        return new (minValue.ToString());
    }

    record MappingDefinition(long destinationRangeStart, long sourceRangeStart, long rangeLength);
}

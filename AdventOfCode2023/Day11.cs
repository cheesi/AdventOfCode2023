using AoCHelper;

namespace AdventOfCode2023;

public class Day11 : BaseDay
{
    private readonly string _input;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day11(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        throw new NotImplementedException();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {

        }

        return new(string.Empty);
    }

    public override ValueTask<string> Solve_2()
    {
        throw new NotImplementedException();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {

        }

        return new(string.Empty);
    }
}

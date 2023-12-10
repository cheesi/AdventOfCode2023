using AoCHelper;

namespace AdventOfCode2023;

public class Day08 : BaseDay
{
    private readonly string _input;

    public Day08()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day08(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var instructions = Array.Empty<char>();
        var directions = new Dictionary<string, Node>();

        var lineCounter = 1;
        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (lineCounter == 1)
            {
                instructions = line.ToCharArray();
            }
            else if (line == string.Empty)
            {
                continue;
            }
            else
            {
                var startingPoint = line[..3];
                var node = new Node(line[7..10], line[12..15]);
                directions.Add(startingPoint, node);
            }
            lineCounter++;
        }

        var repeatCounter = 0;
        var currentCounter = 0;
        var repeatLimit = instructions.Length;
        var currentNode = "AAA";
        while (currentNode is not "ZZZ")
        {
            var instruction = instructions[currentCounter];
            var node = directions[currentNode];
            currentNode = (instruction == 'L') ? node.leftNode : node.rightNode;

            currentCounter++;
            if (currentCounter == repeatLimit)
            {
                repeatCounter++;
                currentCounter = 0;
            }
        }

        return new ValueTask<string>((repeatCounter * repeatLimit + currentCounter).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var instructions = Array.Empty<char>();
        var directions = new Dictionary<string, Node>();

        var lineCounter = 1;
        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            if (lineCounter == 1)
            {
                instructions = line.ToCharArray();
            }
            else if (line == string.Empty)
            {
                continue;
            }
            else
            {
                var startingPoint = line[..3];
                var node = new Node(line[7..10], line[12..15]);
                directions.Add(startingPoint, node);
            }
            lineCounter++;
        }

        var repeatLimit = instructions.LongLength;

        var startingNodes = directions
            .Where(x => x.Key.EndsWith('A'))
            .Select(x => x.Key)
            .ToArray();

        var lengths = new List<long>();

        foreach (var startingNode in startingNodes)
        {
            var repeatCounter = 0l;
            var currentCounter = 0l;
            var currentNode = new string(startingNode);
            while (!currentNode.EndsWith("Z"))
            {
                var instruction = instructions[currentCounter];
                var node = directions[currentNode];
                currentNode = (instruction == 'L') ? node.leftNode : node.rightNode;

                currentCounter++;
                if (currentCounter == repeatLimit)
                {
                    repeatCounter++;
                    currentCounter = 0;
                }
            }
            lengths.Add(repeatCounter * repeatLimit + currentCounter);
        }

        var result = LeastCommonMultiple(lengths);

        return new ValueTask<string>(result.ToString());
    }

    private static long GreatestCommonFactor(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static long LeastCommonMultiple(long a, long b)
    {
        return (a / GreatestCommonFactor(a, b)) * b;
    }

    private static long LeastCommonMultiple(IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    record Node(string leftNode, string rightNode);
}

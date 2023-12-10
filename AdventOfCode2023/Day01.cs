using AoCHelper;

namespace AdventOfCode2023;

public class Day01 : BaseDay
{
    private char[] numbers = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

    private static readonly char[] one = "one".ToCharArray();
    private static readonly char[] two = "two".ToCharArray();
    private static readonly char[] three = "three".ToCharArray();
    private static readonly char[] four = "four".ToCharArray();
    private static readonly char[] five = "five".ToCharArray();
    private static readonly char[] six = "six".ToCharArray();
    private static readonly char[] seven = "seven".ToCharArray();
    private static readonly char[] eight = "eight".ToCharArray();
    private static readonly char[] nine = "nine".ToCharArray();

    private static readonly List<char[]> writtenNumbers = [one, two, three, four, five, six, seven, eight, nine];

    private readonly string _input;

    public Day01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day01(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var calibrationValues = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            char? firstNumber = null;
            char? lastNumber = null;

            var characters = line.ToCharArray();
            foreach (var character in characters)
            {
                if (numbers.Contains(character))
                {
                    firstNumber ??= character;
                    lastNumber = character;
                }
            }

            var calibrationValue = firstNumber + string.Empty + lastNumber;
            calibrationValues.Add(Convert.ToInt32(calibrationValue));
        }

        return new(calibrationValues.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var calibrationValues = new List<int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            char? firstNumber = null;
            char? lastNumber = null;

            var characters = line.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                if (numbers.Contains(characters[i]))
                {
                    firstNumber ??= characters[i];
                    lastNumber = characters[i];
                }
                else
                {
                    for (int j = 0; j < writtenNumbers.Count; j++)
                    {
                        var currentNumber = writtenNumbers[j];
                        var hit = true;
                        for (int k = 0; k < currentNumber.Length; k++)
                        {
                            var index = i + k;
                            if (index >= characters.Length || characters[index] != currentNumber[k])
                            {
                                hit = false;
                                break;
                            }
                        }

                        if (hit)
                        {
                            var number = j + 1;
                            var charNumber = (char)(number + '0');
                            firstNumber ??= charNumber;
                            lastNumber = charNumber;
                        }
                    }
                }
            }

            var calibrationValue = firstNumber + string.Empty + lastNumber;
            calibrationValues.Add(Convert.ToInt32(calibrationValue));
        }

        return new(calibrationValues.Sum().ToString());
    }
}

using System.Globalization;
using System.Text;
using AoCHelper;

namespace AdventOfCode2023;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day03(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var partNumbers = new List<int>();

        var lines = _input.Split(new[] { '\r', '\n' });
        var engineSchematic = ParseEngineSchematic(lines);

        for (int i = 0; i < engineSchematic.GetLength(0); i++)
        {
            var startIndex = -1;
            var numberBuilder = new StringBuilder();
            for (int j = 0; j < engineSchematic.GetLength(1); j++)
            {
                var currentCharacter = engineSchematic[i, j];
                var numericValue = char.GetNumericValue(currentCharacter);
                if (numericValue is not -1)
                {
                    if (startIndex is -1)
                    {
                        startIndex = j;
                    }
                    numberBuilder.Append(currentCharacter);
                }
                else if (numberBuilder.Length > 0)
                {
                    // number ends
                    var row = i;
                    var endIndex = j - 1;
                    if (IsAdjacentToSymbol(engineSchematic, row, startIndex, endIndex))
                    {
                        var number = int.Parse(numberBuilder.ToString());
                        partNumbers.Add(number);
                    }

                    startIndex = -1;
                    numberBuilder.Clear();
                }
            }

            if (numberBuilder.Length > 0)
            {
                var row = i;
                var endIndex = engineSchematic.GetLength(1) - 1;
                if (IsAdjacentToSymbol(engineSchematic, row, startIndex, endIndex))
                {
                    var number = int.Parse(numberBuilder.ToString());
                    partNumbers.Add(number);
                }
            }
        }

        return new ValueTask<string>(partNumbers.Sum().ToString());
    }

    private static char[] NotSymbols = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'];

    private bool IsAdjacentToSymbol(char[,] engineSchematic, int row, int startIndex, int endIndex)
    {
        for (int i = row - 1; i <= row + 1; i++)
        {
            if (i < 0 || i >= engineSchematic.GetLength(0))
            {
                continue;
            }

            for (int j = startIndex - 1; j <= endIndex + 1; j++)
            {
                if (j < 0 || j >= engineSchematic.GetLength(1))
                {
                    continue;
                }

                var character = engineSchematic[i, j];
                if (!NotSymbols.Contains(engineSchematic[i, j]))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static char[,] ParseEngineSchematic(string[] lines)
    {
        var engineSchematic = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var lineChars = lines[i].ToCharArray();
            for (int j = 0; j < lineChars.Length; j++)
            {
                engineSchematic[i, j] = lineChars[j];
            }
        }

        return engineSchematic;
    }

    public override ValueTask<string> Solve_2()
    {
        var gearRatios = new List<int>();

        var lines = _input.Split(new[] { '\r', '\n' });
        var engineSchematic = ParseEngineSchematic(lines);

        for (int i = 0; i < engineSchematic.GetLength(0); i++)
        {
            for (int j = 0; j < engineSchematic.GetLength(1); j++)
            {
                var currentCharacter = engineSchematic[i, j];
                if (currentCharacter is '*')
                {
                    var lookupResult = SearchForGear(engineSchematic, row: i, index: j);
                    if (lookupResult.isGear)
                    {
                        gearRatios.Add(lookupResult.partNumber1 * lookupResult.partNumber2);
                    }
                }
            }
        }

        return new ValueTask<string>(gearRatios.Sum().ToString());
    }

    private static readonly char[] Numbers = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

    private GearLookupResult SearchForGear(char[,] engineSchematic, int row, int index)
    {
        var partNumbers = new List<int>();

        for (int i = row - 1; i <= row + 1; i++)
        {
            if (i < 0 || i >= engineSchematic.GetLength(0))
            {
                continue;
            }

            for (int j = index - 1; j <= index + 1; j++)
            {
                if (j < 0 || j >= engineSchematic.GetLength(1))
                {
                    continue;
                }

                var character = engineSchematic[i, j];
                if (Numbers.Contains(engineSchematic[i, j]))
                {
                    var partNumberLookupResult = LookupPartNumber(engineSchematic, row: i, anchorIndex: j);
                    partNumbers.Add(partNumberLookupResult.number);
                    j = partNumberLookupResult.endIndex;
                }
            }
        }

        return partNumbers.Count == 2
            ? new GearLookupResult(isGear: true, partNumbers.First(), partNumbers.Last())
            : new GearLookupResult(isGear: false, 0, 0);
    }

    private PartNumberLookupResult LookupPartNumber(char[,] engineSchematic, int row, int anchorIndex)
    {
        var startIndex = anchorIndex;
        for (int i = anchorIndex; i >= 0; i--)
        {
            var currentCharacter = engineSchematic[row, i];
            if (Numbers.Contains(currentCharacter))
            {
                startIndex = i;
            }
            else
            {
                break;
            }
        }

        var numberBuilder = new StringBuilder();
        var endIndex = startIndex;
        for (int i = startIndex; i < engineSchematic.GetLength(1); i++)
        {
            var currentCharacter = engineSchematic[row, i];
            if (Numbers.Contains(currentCharacter))
            {
                numberBuilder.Append(currentCharacter);
                endIndex = i;
            }
            else
            {
                break;
            }
        }

        return new PartNumberLookupResult(int.Parse(numberBuilder.ToString()), endIndex);
    }

    record GearLookupResult(bool isGear, int partNumber1, int partNumber2);

    record PartNumberLookupResult(int number, int endIndex);
}

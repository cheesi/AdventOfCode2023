using AoCHelper;

namespace AdventOfCode2023;

public class Day04 : BaseDay
{
    private readonly string _input;

    public Day04()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day04(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var points = new List<double>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var indexOfColon = line.IndexOf(':');
            var truncatedInput = line[(indexOfColon + 1)..];

            var cardParts = truncatedInput.Split('|');
            var winningNumbers = cardParts[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse);
            var cardNumbers = cardParts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse);

            var winningNumbersFromCard = winningNumbers.Intersect(cardNumbers);
            if (winningNumbersFromCard.Any())
            {
                var pointsOfCard = Math.Pow(2, winningNumbersFromCard.Count() - 1);
                points.Add(pointsOfCard);
            }
        }

        return new(points.Sum().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var scratchCards = new Dictionary<int, int>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var truncatedInput = line[5..];
            var indexOfColon = truncatedInput.IndexOf(':');
            var cardId = int.Parse(truncatedInput[..indexOfColon]);

            if (!scratchCards.TryAdd(cardId, 1))
            {
                scratchCards[cardId] += 1;
            }

            truncatedInput = truncatedInput[(indexOfColon + 1)..];

            var cardParts = truncatedInput.Split('|');
            var winningNumbers = cardParts[0]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse);
            var cardNumbers = cardParts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse);

            var winningNumbersFromCard = winningNumbers.Intersect(cardNumbers);
            if (winningNumbersFromCard.Any())
            {
                for (int i = cardId + 1; i <= cardId + winningNumbersFromCard.Count(); i++)
                {
                    if (scratchCards.ContainsKey(i))
                    {
                        scratchCards[i] += scratchCards[cardId];
                    }
                    else
                    {
                        scratchCards.Add(i, scratchCards[cardId]);
                    }
                }
            }
        }

        return new(scratchCards.Select(x => x.Value).Sum().ToString());
    }
}

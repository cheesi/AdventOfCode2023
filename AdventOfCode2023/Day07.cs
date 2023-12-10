using System.Collections;
using AoCHelper;

namespace AdventOfCode2023;

public class Day07 : BaseDay
{
    private readonly string _input;

    public Day07()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day07(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var hands = new List<Hand>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var inputParts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cards = inputParts[0].ToCharArray();
            var bid = int.Parse(inputParts[1]);

            hands.Add(new Hand(cards, bid));
        }

        var handsRanked = hands.OrderBy(x => x, new HandComparer());
        var totalWinnings = 0;
        var counter = 1;
        foreach (var hand in handsRanked)
        {
            totalWinnings += hand.bid * counter;
            counter++;
        }

        return new ValueTask<string>(totalWinnings.ToString());
    }

    class HandComparer : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            var firstRankedHand = x.GetRankedHand();
            var secondRankedHand = y.GetRankedHand();

            var firstHandType = GetHandType(firstRankedHand);
            var secondHandType = GetHandType(secondRankedHand);

            if (firstHandType == secondHandType)
            {
                for (int i = 0; i < x.cards.Length; i++)
                {
                    var firstCardValue = GetCardValue(x.cards[i]);
                    var secondCardValue = GetCardValue(y.cards[i]);
                    if (firstCardValue > secondCardValue)
                    {
                        return 1;
                    }
                    else if (secondCardValue > firstCardValue)
                    {
                        return -1;
                    }
                }

                throw new Exception("Invalid data");
            }
            else if (firstHandType > secondHandType)
            {
                return 1;
            }

            return -1;
        }

        private CardValue GetCardValue(char card)
        {
            return card switch
            {
                >= '1' and <= '9' => (CardValue)card - '0',
                'T' => CardValue.Ten,
                'J' => CardValue.Jack,
                'Q' => CardValue.Queen,
                'K' => CardValue.King,
                'A' => CardValue.Ass,
                _ => throw new Exception("Input error")
            };
        }

        public HandType GetHandType(IEnumerable<RankedHand> rankedHand)
        {
            var firstGroup = rankedHand.First();
            var secondGroup = rankedHand.Count() > 1 ? rankedHand.Skip(1).First() : null;
            return firstGroup.count switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 when secondGroup.count == 2 => HandType.FullHouse,
                3 => HandType.ThreeOfAKind,
                2 when secondGroup.count == 2 => HandType.TwoPair,
                2 => HandType.OnePair,
                _ => HandType.HighestCard
            };
        }
    }

    public override ValueTask<string> Solve_2()
    {
        var hands = new List<Hand>();

        using var stringReader = new StringReader(_input);
        while (stringReader.ReadLine() is { } line)
        {
            var inputParts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var cards = inputParts[0].ToCharArray();
            var bid = int.Parse(inputParts[1]);

            hands.Add(new Hand(cards, bid));
        }

        var handsRanked = hands.OrderBy(x => x, new HandComparer2());
        var totalWinnings = 0;
        var counter = 1;
        foreach (var hand in handsRanked)
        {
            totalWinnings += hand.bid * counter;
            counter++;
        }

        return new ValueTask<string>(totalWinnings.ToString());
    }

    class HandComparer2 : IComparer<Hand>
    {
        public int Compare(Hand? x, Hand? y)
        {
            var firstRankedHand = x.GetRankedHand();
            var secondRankedHand = y.GetRankedHand();

            var firstHandType = GetHandType(firstRankedHand);
            var secondHandType = GetHandType(secondRankedHand);

            if (firstHandType == secondHandType)
            {
                for (int i = 0; i < x.cards.Length; i++)
                {
                    var firstCardValue = GetCardValue(x.cards[i]);
                    var secondCardValue = GetCardValue(y.cards[i]);
                    if (firstCardValue > secondCardValue)
                    {
                        return 1;
                    }
                    else if (secondCardValue > firstCardValue)
                    {
                        return -1;
                    }
                }

                throw new Exception("Invalid data");
            }
            else if (firstHandType > secondHandType)
            {
                return 1;
            }

            return -1;
        }

        private CardValue2 GetCardValue(char card)
        {
            return card switch
            {
                >= '1' and <= '9' => (CardValue2)card - '0',
                'T' => CardValue2.Ten,
                'J' => CardValue2.Joker,
                'Q' => CardValue2.Queen,
                'K' => CardValue2.King,
                'A' => CardValue2.Ass,
                _ => throw new Exception("Input error")
            };
        }

        private HandType GetHandType(IEnumerable<RankedHand> rankedHand)
        {
            var jokerGroup = rankedHand.SingleOrDefault(x => x.cardType is 'J');
            if (jokerGroup is not null && jokerGroup.count is 5)
            {
                return HandType.FiveOfAKind;
            }

            var firstGroup = rankedHand.First(x => x.cardType is not 'J');
            var secondGroup = rankedHand.Count() > 1 ? rankedHand.Skip(1).FirstOrDefault(x => x.cardType is not 'J') : null;

            var firstGroupCount = firstGroup.count + (jokerGroup?.count ?? 0);
            return firstGroupCount switch
            {
                5 => HandType.FiveOfAKind,
                4 => HandType.FourOfAKind,
                3 when secondGroup.count == 2 => HandType.FullHouse,
                3 => HandType.ThreeOfAKind,
                2 when secondGroup.count == 2 => HandType.TwoPair,
                2 => HandType.OnePair,
                _ => HandType.HighestCard
            };
        }
    }

    enum CardValue
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ass = 14
    }

    enum CardValue2
    {
        Joker = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Queen = 12,
        King = 13,
        Ass = 14
    }

    enum HandType
    {
        FiveOfAKind = 7,
        FourOfAKind = 6,
        FullHouse = 5,
        ThreeOfAKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighestCard = 1
    }

    record RankedHand(char cardType, int count);

    record Hand(char[] cards, int bid)
    {
        public IEnumerable<RankedHand> GetRankedHand()
            => cards.GroupBy(card => card)
                .Select(group => new RankedHand(group.Key, group.Count()))
                .OrderByDescending(x => x.count);

        public override string ToString()
        {
            return new string(cards);
        }
    }
}

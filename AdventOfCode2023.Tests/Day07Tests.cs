using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day07Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    32T3K 765
                    T55J5 684
                    KK677 28
                    KTJJT 220
                    QQQJA 483
                    """;
        var systemUnderTest = new Day07(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("6440");
    }

    [Fact]
    public async Task Part1_2()
    {
        // Arrange
        var input = """
                    2345A 1
                    Q2KJJ 13
                    Q2Q2Q 19
                    T3T3J 17
                    T3Q33 11
                    2345J 3
                    J345A 2
                    32T3K 5
                    T55J5 29
                    KK677 7
                    KTJJT 34
                    QQQJA 31
                    JJJJJ 37
                    JAAAA 43
                    AAAAJ 59
                    AAAAA 61
                    2AAAA 23
                    2JJJJ 53
                    JJJJ2 41
                    """;
        var systemUnderTest = new Day07(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("6592");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    32T3K 765
                    T55J5 684
                    KK677 28
                    KTJJT 220
                    QQQJA 483
                    """;
        var systemUnderTest = new Day07(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("5905");
    }

    [Fact]
    public async Task Part2_2()
    {
        // Arrange
        var input = """
                    2345A 1
                    Q2KJJ 13
                    Q2Q2Q 19
                    T3T3J 17
                    T3Q33 11
                    2345J 3
                    J345A 2
                    32T3K 5
                    T55J5 29
                    KK677 7
                    KTJJT 34
                    QQQJA 31
                    JJJJJ 37
                    JAAAA 43
                    AAAAJ 59
                    AAAAA 61
                    2AAAA 23
                    2JJJJ 53
                    JJJJ2 41
                    """;
        var systemUnderTest = new Day07(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("6839");
    }
}

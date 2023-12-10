using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day01Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    1abc2
                    pqr3stu8vwx
                    a1b2c3d4e5f
                    treb7uchet
                    """;
        var systemUnderTest = new Day01(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("142");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    two1nine
                    eightwothree
                    abcone2threexyz
                    xtwone3four
                    4nineeightseven2
                    zoneight234
                    7pqrstsixteen
                    """;
        var systemUnderTest = new Day01(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("281");
    }
}

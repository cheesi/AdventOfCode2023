using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day06Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    Time:      7  15   30
                    Distance:  9  40  200
                    """;
        var systemUnderTest = new Day06(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("288");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    Time:      71530
                    Distance:  940200
                    """;
        var systemUnderTest = new Day06(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("71503");
    }
}

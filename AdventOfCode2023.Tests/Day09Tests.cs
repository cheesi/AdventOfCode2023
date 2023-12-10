using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day09Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    0 3 6 9 12 15
                    1 3 6 10 15 21
                    10 13 16 21 30 45
                    """;
        var systemUnderTest = new Day09(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("114");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    0 3 6 9 12 15
                    1 3 6 10 15 21
                    10 13 16 21 30 45
                    """;
        var systemUnderTest = new Day09(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("2");
    }
}

using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day02Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                    """;
        var systemUnderTest = new Day02(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("8");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                    Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
                    Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
                    Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
                    Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
                    """;
        var systemUnderTest = new Day02(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().BeEquivalentTo("2286");
    }
}

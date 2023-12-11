using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day11Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    ...#......
                    .......#..
                    #.........
                    ..........
                    ......#...
                    .#........
                    .........#
                    ..........
                    .......#..
                    #...#.....
                    """;
        var systemUnderTest = new Day11(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("374");
    }
}

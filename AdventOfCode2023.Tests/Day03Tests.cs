using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day03Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    467..114..
                    ...*......
                    ..35..633.
                    ......#...
                    617*......
                    .....+.58.
                    ..592.....
                    ......755.
                    ...$.*....
                    .664.598..
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("4361");
    }

    [Fact]
    public async Task Part1_2()
    {
        // Arrange
        var input = """
                    12.......*..
                    +.........34
                    .......-12..
                    ..78........
                    ..*....60...
                    78..........
                    .......23...
                    ....90*12...
                    ............
                    2.2......12.
                    .*.........*
                    1.1.......56
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("413");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    467..114..
                    ...*......
                    ..35..633.
                    ......#...
                    617*......
                    .....+.58.
                    ..592.....
                    ......755.
                    ...$.*....
                    .664.598..
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("467835");
    }

    [Fact]
    public async Task Part2_2()
    {
        // Arrange
        var input = """
                    12.......*..
                    +.........34
                    .......-12..
                    ..78........
                    ..*....60...
                    78..........
                    .......23...
                    ....90*12...
                    ............
                    2.2......12.
                    .*.........*
                    1.1.......56
                    """;
        var systemUnderTest = new Day03(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("6756");
    }
}

using AdventOfCode2023;

namespace AdventOfCode2023.Tests;

public class Day08Tests
{
    [Fact]
    public async Task Part1()
    {
        // Arrange
        var input = """
                    RL
                    
                    AAA = (BBB, CCC)
                    BBB = (DDD, EEE)
                    CCC = (ZZZ, GGG)
                    DDD = (DDD, DDD)
                    EEE = (EEE, EEE)
                    GGG = (GGG, GGG)
                    ZZZ = (ZZZ, ZZZ)
                    """;
        var systemUnderTest = new Day08(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("2");
    }

    [Fact]
    public async Task Part1_2()
    {
        // Arrange
        var input = """
                    LLR
                    
                    AAA = (BBB, BBB)
                    BBB = (AAA, ZZZ)
                    ZZZ = (ZZZ, ZZZ)
                    """;
        var systemUnderTest = new Day08(input);

        // Act
        var result = await systemUnderTest.Solve_1();

        // Assert
        result.Should().Be("6");
    }

    [Fact]
    public async Task Part2()
    {
        // Arrange
        var input = """
                    LR
                    
                    11A = (11B, XXX)
                    11B = (XXX, 11Z)
                    11Z = (11B, XXX)
                    22A = (22B, XXX)
                    22B = (22C, 22C)
                    22C = (22Z, 22Z)
                    22Z = (22B, 22B)
                    XXX = (XXX, XXX)
                    """;
        var systemUnderTest = new Day08(input);

        // Act
        var result = await systemUnderTest.Solve_2();

        // Assert
        result.Should().Be("6");
    }
}

using System.Drawing;
using AoCHelper;

namespace AdventOfCode2023;

public class Day11 : BaseDay
{
    private readonly string _input;

    public Day11()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day11(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split('\r', '\n');
        var map = new char[lines.Length, lines[0].Length];
        var galaxies = new List<Point>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                var currentTile = line[j];
                map[i, j] = currentTile;
                if (currentTile == '#')
                {
                    galaxies.Add(new Point(i, j));
                }
            }
        }

        var emptyRows = new List<long>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            var isEmpty = true;
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] != '.')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                emptyRows.Add(i);
            }
        }

        var emptyColums = new List<long>();
        for (int i = 0; i < map.GetLength(1); i++)
        {
            var isEmpty = true;
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if (map[j, i] != '.')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                emptyColums.Add(i);
            }
        }

        var galaxyDistances = new List<long>();
        for (int i = 0; i < galaxies.Count; i++)
        {
            var statingGalaxy = galaxies[i];
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                var targetGalaxy = galaxies[j];
                galaxyDistances.Add(CalculateDistance(statingGalaxy, targetGalaxy, emptyRows, emptyColums));
            }
        }

        return new(galaxyDistances.Sum().ToString());
    }

    private long CalculateDistance(Point startingGalaxy, Point targetGalaxy, List<long> emptyRows, List<long> emptyColumns, long distance = 1)
    {
        return Math.Abs(targetGalaxy.X - startingGalaxy.X) + (emptyRows.Count(x => x > startingGalaxy.X && x < targetGalaxy.X || x > targetGalaxy.X && x < startingGalaxy.X) * distance) +
               Math.Abs(targetGalaxy.Y - startingGalaxy.Y) + (emptyColumns.Count(y => y > startingGalaxy.Y && y < targetGalaxy.Y || y > targetGalaxy.Y && y < startingGalaxy.Y) * distance);
    }


    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split('\r', '\n');
        var map = new char[lines.Length, lines[0].Length];
        var galaxies = new List<Point>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                var currentTile = line[j];
                map[i, j] = currentTile;
                if (currentTile == '#')
                {
                    galaxies.Add(new Point(i, j));
                }
            }
        }

        var emptyRows = new List<long>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            var isEmpty = true;
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] != '.')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                emptyRows.Add(i);
            }
        }

        var emptyColums = new List<long>();
        for (int i = 0; i < map.GetLength(1); i++)
        {
            var isEmpty = true;
            for (int j = 0; j < map.GetLength(0); j++)
            {
                if (map[j, i] != '.')
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                emptyColums.Add(i);
            }
        }

        var galaxyDistances = new List<long>();
        for (int i = 0; i < galaxies.Count; i++)
        {
            var statingGalaxy = galaxies[i];
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                var targetGalaxy = galaxies[j];
                galaxyDistances.Add(CalculateDistance(statingGalaxy, targetGalaxy, emptyRows, emptyColums, 999999));
            }
        }

        return new(galaxyDistances.Sum().ToString());
    }
}

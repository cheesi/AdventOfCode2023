using AoCHelper;

namespace AdventOfCode2023;

public class Day10 : BaseDay
{
    private readonly string _input;

    public Day10()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public Day10(string input)
    {
        _input = input;
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _input.Split('\r', '\n');
        var map = new char[lines.Length, lines[0].Length];
        var startingPoint = new Coordinate(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            var asciiInput = lines[i].Replace('|', '│')
                .Replace('-', '─')
                .Replace('L', '└')
                .Replace('J', '┘')
                .Replace('7', '┐')
                .Replace('F', '┌')
                .ToArray();
            for (int j = 0; j < asciiInput.Length; j++)
            {
                var currentTile = asciiInput[j];
                map[i, j] = currentTile;
                if (currentTile == 'S')
                {
                    startingPoint = new Coordinate(i, j);
                }
            }
        }

        var path = new LinkedList<Coordinate>();
        var currentCoordinate = startingPoint;
        var previousCoordinate = startingPoint;
        do
        {
            path.AddLast(currentCoordinate);

            var possibleCoordinates = GetPossibleCoordinates(map, currentCoordinate, previousCoordinate);
            previousCoordinate = currentCoordinate;
            currentCoordinate = possibleCoordinates.FirstOrDefault();
        }
        while (currentCoordinate is not null);

        // Visualization
        //PrintMap(map, path, new List<Coordinate>());

        return new ValueTask<string>(Math.Ceiling((double)path.Count / 2).ToString());
    }

    private Coordinate[] GetPossibleCoordinates(char[,] map, Coordinate currentCoordinate, Coordinate previousCoordinate)
    {
        // │
        // ─
        // └
        // ┐
        // ┘
        // ┌

        var currentTile = map[currentCoordinate.x, currentCoordinate.y];
        var possibleCoordinates = new List<Coordinate>();
        if (currentCoordinate.x - 1 >= 0 && (currentTile == '│' || currentTile == '└' || currentTile == '┘' || currentTile == 'S'))
        {
            var nextCoordinate = currentCoordinate with { x = currentCoordinate.x - 1 };
            var nextTile = map[nextCoordinate.x, nextCoordinate.y];
            if (nextTile == '│' || nextTile == '┐' || nextTile == '┌')
            {
                possibleCoordinates.Add(nextCoordinate);
            }
        }
        if (currentCoordinate.y - 1 >= 0 && (currentTile == '─' || currentTile == '┐' || currentTile == '┘' || currentTile == 'S'))
        {
            var nextCoordinate = currentCoordinate with { y = currentCoordinate.y -  1 };
            var nextTile = map[nextCoordinate.x, nextCoordinate.y];
            if (nextTile == '─' || nextTile == '└' || nextTile == '┌')
            {
                possibleCoordinates.Add(nextCoordinate);
            }
        }
        if (currentCoordinate.x + 1 < map.GetLength(0) && (currentTile == '│' || currentTile == '┐' || currentTile == '┌' || currentTile == 'S'))
        {
            var nextCoordinate = currentCoordinate with { x = currentCoordinate.x + 1 };
            var nextTile = map[nextCoordinate.x, nextCoordinate.y];
            if (nextTile == '│' || nextTile == '└' || nextTile == '┘')
            {
                possibleCoordinates.Add(nextCoordinate);
            }
        }
        if (currentCoordinate.y + 1 < map.GetLength(1) && (currentTile == '─' || currentTile == '└' || currentTile == '┌' || currentTile == 'S'))
        {
            var nextCoordinate = currentCoordinate with { y = currentCoordinate.y + 1 };
            var nextTile = map[nextCoordinate.x, nextCoordinate.y];
            if (nextTile == '─' || nextTile == '┐' || nextTile == '┘')
            {
                possibleCoordinates.Add(nextCoordinate);
            }
        }

        return possibleCoordinates
            .Where(coordinate => coordinate.x != previousCoordinate.x || coordinate.y != previousCoordinate.y)
            .ToArray();
    }

    public override ValueTask<string> Solve_2()
    {
        var lines = _input.Split('\r', '\n');
        var map = new char[lines.Length, lines[0].Length];
        var startingPoint = new Coordinate(0, 0);
        for (int i = 0; i < lines.Length; i++)
        {
            var asciiInput = lines[i].Replace('|', '│')
                .Replace('-', '─')
                .Replace('L', '└')
                .Replace('J', '┘')
                .Replace('7', '┐')
                .Replace('F', '┌')
                .ToArray();
            for (int j = 0; j < asciiInput.Length; j++)
            {
                var currentTile = asciiInput[j];
                map[i, j] = currentTile;
                if (currentTile == 'S')
                {
                    startingPoint = new Coordinate(i, j);
                }
            }
        }

        var path = new LinkedList<Coordinate>();
        var currentCoordinate = startingPoint;
        var previousCoordinate = startingPoint;
        do
        {
            path.AddLast(currentCoordinate);

            var possibleCoordinates = GetPossibleCoordinates(map, currentCoordinate, previousCoordinate);
            previousCoordinate = currentCoordinate;
            currentCoordinate = possibleCoordinates.FirstOrDefault();
        }
        while (currentCoordinate is not null);

        // Remove pipes not part of the loop
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (!path.Contains(new Coordinate(i, j)))
                {
                    map[i, j] = '.';
                }
            }
        }

        // Vertical pipes = switch between inside and outside
        var counter = 0;
        var coordinatesInside = new List<Coordinate>();
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                var currentTile = map[i, j];
                if (currentTile != '.')
                {
                    continue;
                }

                var inside = false;
                for (int k = 0; k <= j; k++)
                {
                    var tile = map[i, k];
                    if (tile == '│' || tile == '└' || tile == '┘')
                    {
                        inside = !inside;
                    }
                }

                if (inside)
                {
                    counter++;
                    coordinatesInside.Add(new Coordinate(i, j));
                }
            }
        }

        // Visualization
        //PrintMap(map, path, coordinatesInside);

        return new ValueTask<string>(counter.ToString());
    }

    private static void PrintMap(char[,] map, LinkedList<Coordinate> path, List<Coordinate> coordinatesInside)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (path.Contains(new Coordinate(i, j)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (coordinatesInside.Contains(new Coordinate(i, j)))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(map[i, j]);
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine();
        }
    }

    record Coordinate(long x, long y);
}

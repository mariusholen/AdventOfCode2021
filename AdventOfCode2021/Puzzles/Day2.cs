namespace AdventOfCode2021.Puzzles;
public class Day2
{
    public Day2(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var horizontal = 0;
        var depth = 0;
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;
            var command = value.Substring(0, value.IndexOf(' '));
            int.TryParse(value.Substring(value.IndexOf(' ')), out var units);
            switch (command)
            {
                case "forward":
                    horizontal += units;
                    break;
                case "up":
                    depth -= units;
                    break;
                case "down":
                    depth += units;
                    break;
                default:
                    break;
            }
        }
        Console.WriteLine($"{horizontal * depth}");
        Console.ReadKey();
    }
    public static void PuzzleB(string[] values)
    {
        var horizontal = 0;
        var depth = 0;
        var aim = 0;
        foreach (var value in values)
        {
            if (string.IsNullOrWhiteSpace(value))
                continue;
            var command = value.Substring(0, value.IndexOf(' '));
            int.TryParse(value.Substring(value.IndexOf(' ')), out var units);
            switch (command)
            {
                case "forward":
                    horizontal += units;
                    depth += aim * units;
                    break;
                case "up":
                    aim -= units;
                    break;
                case "down":
                    aim += units;
                    break;
                default:
                    break;
            }
        }
        Console.WriteLine($"{horizontal * depth}");
        Console.ReadKey();
    }
}

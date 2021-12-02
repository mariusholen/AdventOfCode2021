namespace AdventOfCode2021.Puzzles;
public class Day1
{
    public Day1(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var counter = 0;
        for(var i = 1; i < values.Length; i++)
        {
            int.TryParse(values[i], out int value);
            int.TryParse(values[i -1], out int previousValue);
            if (value > previousValue)
                counter++;
        }
        Console.WriteLine(counter);
        Console.ReadKey();
    }

    public static void PuzzleB(string[] values)
    {
        var counter = 0;
        for(var i = 3; i < values.Length; i++)
        {
            var value = int.Parse(values[i]) + int.Parse(values[i - 1]) + int.Parse(values[i - 2]);
            var previousValue = int.Parse(values[i - 1]) + int.Parse(values[i - 2]) + int.Parse(values[i - 3]);
            if (value > previousValue)
                counter++;
        }
        Console.WriteLine(counter);
        Console.ReadKey();
    }
}

namespace AdventOfCode2021.Puzzles;
public class Day7
{
    public Day7(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var crabs = values[0].Split(",");
        var horizontal = new List<int>();
        foreach (var value in crabs)
        {
            horizontal.Add(int.Parse(value));
        }
        var min = horizontal.Min();
        var max = horizontal.Max();
        var minfuel = 1000000000;

        for (int i = min; i < max; i++)
        {
            var movingValue = 0;
            foreach (var crab in horizontal)
            {
                if (crab < i)
                    movingValue += (i - crab);
                else
                    movingValue += (crab - i);
            }
            if (movingValue < minfuel)
                minfuel = movingValue;
        }
        Console.WriteLine(minfuel);
        Console.ReadKey();
    }
    public static void PuzzleB(string[] values)
    {
        var crabs = values[0].Split(",");
        var horizontal = new List<int>();
        foreach (var value in crabs)
        {
            horizontal.Add(int.Parse(value));
        }
        var min = horizontal.Min();
        var max = horizontal.Max();
        var minfuel = 1000000000;

        for (int i = min; i < max; i++)
        {
            var movingValue = 0;
            foreach (var crab in horizontal)
            {
                var newMovingValue = 0;
                if (crab < i)
                    newMovingValue += (i - crab);
                else
                    newMovingValue += (crab - i);
                movingValue += CountMovingValue(newMovingValue);
            }
            if (movingValue < minfuel)
                minfuel = movingValue;
        }
        Console.WriteLine(minfuel);
    }

    private static int CountMovingValue(int movingValue)
    {
        var total = 0;
        for (int i = 0; i <= movingValue; i++)
        {
            total += i;
        }
        return total;
    }
}

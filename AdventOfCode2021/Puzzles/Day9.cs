using System.Collections.Generic;

namespace AdventOfCode2021.Puzzles;
public class Day9
{
    public static int[,] HeightMap { get; set; } = new int[1, 1];

    public Day9(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        HeightMap = new int[values.Length,values[0].Length];
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            for (var j = 0; j < value.Length; j++)
            {
                HeightMap[i, j] = int.Parse(value[j].ToString());
            }
        }
        var lowpoint = 0;
        for (var i = 0; i < HeightMap.GetLength(0); i++)
        {
            for (var j = 0; j < HeightMap.GetLength(1); j++)
            {
                var value = HeightMap[i, j];
                var right = false;
                var left = false;
                var up = false;
                var down = false;
                var iCorner = i == 0 || i == HeightMap.GetLength(0) - 1;
                var jCorner = j == 0 || j == HeightMap.GetLength(1) - 1;
                if (i > 0)
                {
                    if (value < HeightMap[i - 1, j])
                        up = true;
                }
                if (i < HeightMap.GetLength(0) - 1)
                {
                    if (value < HeightMap[i + 1, j])
                        down = true;
                }
                if (j > 0)
                {
                    if (value < HeightMap[i, j - 1])
                        left = true;
                }
                if (j < HeightMap.GetLength(1) - 1)
                {
                    if (value < HeightMap[i, j + 1])
                        right = true;
                }
                if (right || left || up || down)
                {
                    if (iCorner)
                    {
                        if ((right && left) || (jCorner && right) || (jCorner && left))
                        {
                            if (up || down)
                                lowpoint += value + 1;
                        }
                    }
                    else if (jCorner)
                    {
                        if ((up && down) || (iCorner && up) || (iCorner && down))
                        {
                            if (right || left)
                                lowpoint += value + 1;
                        }
                    }
                    else if (right && left && up && down)
                        lowpoint += value + 1;
                    else
                        continue;
                }
            }
        }
        Console.WriteLine(lowpoint);
        Console.ReadKey();
    }

    public static void PuzzleB(string[] values)
    {
        HeightMap = new int[values.Length, values[0].Length];
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i];
            for (var j = 0; j < value.Length; j++)
            {
                HeightMap[i, j] = int.Parse(value[j].ToString());
            }
        }
        var topThreeBasin = new List<int>();
        for (var i = 0; i < HeightMap.GetLength(0); i++)
        {
            for (var j = 0; j < HeightMap.GetLength(1); j++)
            {
                var value = HeightMap[i, j];
                var right = false;
                var left = false;
                var up = false;
                var down = false;
                var iCorner = i == 0 || i == HeightMap.GetLength(0) - 1;
                var jCorner = j == 0 || j == HeightMap.GetLength(1) - 1;
                if (i > 0)
                {
                    if (value < HeightMap[i - 1, j])
                        up = true;
                }
                if (i < HeightMap.GetLength(0) - 1)
                {
                    if (value < HeightMap[i + 1, j])
                        down = true;
                }
                if (j > 0)
                {
                    if (value < HeightMap[i, j - 1])
                        left = true;
                }
                if (j < HeightMap.GetLength(1) - 1)
                {
                    if (value < HeightMap[i, j + 1])
                        right = true;
                }
                if (right || left || up || down)
                {
                    if (iCorner)
                    {
                        if ((right && left) || (jCorner && right) || (jCorner && left))
                        {
                            if (up || down)
                            {
                                var basin = GetBasinSize(i, j);
                                topThreeBasin = GetBasinTopThree(topThreeBasin, basin);
                            }
                        }
                    }
                    else if (jCorner)
                    {
                        if ((up && down) || (iCorner && up) || (iCorner && down))
                        {
                            if (right || left)
                            {
                                var basin = GetBasinSize(i, j);
                                topThreeBasin = GetBasinTopThree(topThreeBasin, basin);
                            }
                        }
                    }
                    else if (right && left && up && down)
                    {
                        var basin = GetBasinSize(i, j);
                        topThreeBasin = GetBasinTopThree(topThreeBasin, basin);
                    }
                    else
                        continue;
                }
            }
        }

        var topThreeMultiplied = 1;
        foreach (var basin in topThreeBasin)
        {
            topThreeMultiplied *= basin;
        }
        Console.WriteLine(topThreeMultiplied);
        Console.ReadKey();
    }

    private static int GetBasinSize(int i, int j, string direction = "")
    {
        var value = HeightMap[i, j];
        if (value == 9)
            return 0;

        HeightMap[i, j] = 9;
        var counter = 1;
        var iLength = HeightMap.GetLength(0) - 1;
        var jLength = HeightMap.GetLength(1) - 1;
        if (i > 0 && !direction.Equals("up"))
        {
            counter += GetBasinSize(i - 1, j, "down");
        }
        if (i < iLength && !direction.Equals("down"))
        {
            counter += GetBasinSize(i + 1, j, "up");
        }
        if (j > 0 && !direction.Equals("left"))
        {
            counter += GetBasinSize(i, j - 1, "right");
        }
        if (j < jLength && !direction.Equals("right"))
        {
            counter += GetBasinSize(i, j + 1, "left");
        }
        return counter;
    }

    private static List<int> GetBasinTopThree(List<int> currentTopThree, int contender)
    {
        currentTopThree = currentTopThree.OrderBy(x => x).ToList();
        var newTopThree = new List<int>(currentTopThree);
        if (newTopThree.Count < 3)
        {
            newTopThree.Add(contender);
            return newTopThree;
        }
        for (var i = 0; i < currentTopThree.Count; i++)
        {
            if (contender > currentTopThree[i])
            {
                newTopThree[i] = contender;
                return newTopThree;
            }
        }
        return newTopThree;
    }
}
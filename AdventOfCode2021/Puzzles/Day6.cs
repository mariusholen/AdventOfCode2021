namespace AdventOfCode2021.Puzzles;
public class Day6
{
    public Day6(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var fishStream = values[0].Split(",");
        var fishList = new List<LanternFish>();
        foreach (var fish in fishStream)
        {
            fishList.Add(new LanternFish(fish));
        }

        for (int i = 0; i < 80; i++)
        {
            var updatedFishList = new List<LanternFish>();
            foreach (var fish in fishList)
            {
                if (fish.TimeToBirth())
                {
                    //Child
                    updatedFishList.Add(new LanternFish());
                    //Parent
                    fish.ResetTimer();
                }
                else
                {
                    fish.DecreaseTimer();
                }
                updatedFishList.Add(fish);
            }
            fishList = new List<LanternFish>();
            fishList.AddRange(updatedFishList);
        }
        Console.WriteLine($"{fishList.Count()}");
        Console.ReadKey();
    }

    // Got caught in OutOfMemory trap..
    // Had to take a look at Reddit after spending too many hours on Part Two. Great learning.
    public static void PuzzleB(string[] values)
    {
        var ageWhenNewFish = 8;
        var simulationDays = 256;
        var schoolOfFish = values[0].Split(",");
        var fishAge = new long[ageWhenNewFish + 1];
        foreach (var fish in schoolOfFish)
        {
            var age = long.Parse(fish);
            fishAge[age]++;
        }

        for (int i = 0; i < simulationDays; i++)
        {
            long newLanternFish = fishAge[0];
            for (int j = 1; j <= ageWhenNewFish; j++)
            {
                fishAge[j - 1] = fishAge[j];
                fishAge[j] = 0;
            }
            fishAge[6] += newLanternFish;
            fishAge[8] += newLanternFish;
        }
        long fishTotal = 0;
        for (int age = 0; age <= ageWhenNewFish; age++)
        {
            fishTotal += fishAge[age];
        }
        Console.WriteLine(fishTotal);
        Console.ReadKey();
    }
}

public class LanternFish
{
    public LanternFish(string timer = "8")
    {
        InternalTimer = int.Parse(timer);
    }
    public int InternalTimer { get; set; }
    public void ResetTimer() => InternalTimer = 6;
    public void DecreaseTimer() => InternalTimer--;
    public bool TimeToBirth() => InternalTimer == 0;
}

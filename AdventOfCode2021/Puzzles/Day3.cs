namespace AdventOfCode2021.Puzzles;
public class Day3
{
    public Day3(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var bitsarray = new Bits[values.First().Length];
        for (int i = 0; i < bitsarray.Length; i++)
        {
            bitsarray[i] = new Bits();
        }
        foreach (string value in values)
        {
            for (int i = 0; i < value.Length; i++)
            {
                var item = bitsarray[i];
                var val = value[i].ToString();
                item.IncreaseCount(val);
            }
        }
        var gammabits = string.Join("", bitsarray.Select(a => a.Gammabit()));
        var gammarate = Convert.ToInt32(gammabits, 2);

        var epsilonbits = string.Join("", bitsarray.Select(a => a.Epsilonbit()));
        var epsilonrate = Convert.ToInt32(epsilonbits, 2);

        var powerconsumption = epsilonrate * gammarate;
        Console.WriteLine($"{powerconsumption}");
        Console.ReadKey();
    }

    public static void PuzzleB(string[] values)
    {
        var oxygenRating = Looper(values, true);
        var co2Rating = Looper(values, false);
        var lifeSupportRating = Convert.ToInt32(oxygenRating, 2) * Convert.ToInt32(co2Rating, 2);
        Console.WriteLine($"{lifeSupportRating}");
        Console.ReadKey();
    }

    private static string Looper(string[] values, bool isOxygen, int index = 0)
    {
        if (values.Length == 1)
            return values[0];

        var bits2 = new Bits2();
        foreach (string value in values)
        {
            var val = value[index].ToString();
            bits2.IncreaseCount(val, value);
        }
        if (isOxygen)
            return Looper(bits2.OxygenRating(), isOxygen, index+1);
        else
            return Looper(bits2.CO2Rating(), isOxygen, index+1);
    }
}

public class Bits
{
    public int Bit0 { get; set; }
    public int Bit1 { get; set; }
    public string Gammabit() => Bit0 > Bit1 ? "0" : "1";
    public string Epsilonbit() => Bit0 < Bit1 ? "0" : "1";
    public void IncreaseCount(string val)
    {
        if (val == "0")
            Bit0++;
        else
            Bit1++;
    }
}

public class Bits2
{
    public List<string> Bit0 { get; set; } = new List<string>();
    public List<string> Bit1 { get; set; } = new List<string>();
    public void IncreaseCount(string val, string value)
    {
        if (val == "0")
            Bit0.Add(value);
        else
            Bit1.Add(value);
    }
    public string[] OxygenRating()
    {
        if (Bit1.Count >= Bit0.Count)
            return Bit1.ToArray();
        else
            return Bit0.ToArray();
    }
    public string[] CO2Rating()
    {
        if (Bit0.Count <= Bit1.Count)
            return Bit0.ToArray();
        else
            return Bit1.ToArray();
    }
}

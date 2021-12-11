namespace AdventOfCode2021.Puzzles;
public class Day8
{
    public Day8(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var counter = 0;
        foreach (var value in values)
        {
            var outputvalues = value.Split(" | ").Last().Split(" ");
            foreach (var output in outputvalues)
            {
                if (output.Length == 2 || output.Length == 3 || output.Length == 4 || output.Length == 7)
                    counter++;
            }
        }
        Console.WriteLine(counter);
        Console.ReadKey();
    }
    public static void PuzzleB(string[] values)
    {
        var totalSum = 0;
        var arrayOfNumbers = new char[10][];
        arrayOfNumbers[0] = new char[] { 'a', 'b', 'c', 'e', 'f', 'g' };
        arrayOfNumbers[1] = new char[] { 'c', 'f' };
        arrayOfNumbers[2] = new char[] { 'a', 'c', 'd', 'e', 'g' };
        arrayOfNumbers[3] = new char[] { 'a', 'c', 'd', 'f', 'g' };
        arrayOfNumbers[4] = new char[] { 'b', 'c', 'd', 'f' };
        arrayOfNumbers[5] = new char[] { 'a', 'b', 'd', 'f', 'g' };
        arrayOfNumbers[6] = new char[] { 'a', 'b', 'd', 'e', 'f', 'g' };
        arrayOfNumbers[7] = new char[] { 'a', 'c', 'f' };
        arrayOfNumbers[8] = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
        arrayOfNumbers[9] = new char[] { 'a', 'b', 'c', 'd', 'f', 'g' };
        foreach (var value in values)
        {
            var sevenSegment = new Dictionary<char, char>() { { 'a' , ' ' }, { 'b' , ' '},
                { 'c', ' ' }, { 'd' , ' ' }, { 'e', ' ' }, { 'f' , ' ' }, { 'g' , ' ' } };
            var signalpattern = value.Split(" | ").First().Split(" ");
            var outputvalues = value.Split(" | ").Last().Split(" ");

            var one = signalpattern.First(x => x.Length == 2);
            var seven = signalpattern.First(x => x.Length == 3);
            var four = signalpattern.First(x => x.Length == 4);
            var three = signalpattern.First(x => x.Length == 5 && x.Contains(one[0]) && x.Contains(one[1]));
            var eight = signalpattern.First(x => x.Length == 7);

            var a = seven.First(x => !one.Contains(x));
            var bAndD = four.Where(x => !one.Contains(x)).ToArray();
            var b = bAndD.First(x => !three.Contains(x));
            var d = bAndD.First(x => !x.Equals(b));

            var two = signalpattern.First(x => x.Length == 5 && !x.Contains(b) && !x.Equals(three));
            var five = signalpattern.First(x => x.Length == 5 && x.Contains(b));
            var zero = signalpattern.First(x => x.Length == 6 && !x.Contains(d));

            var c = one.First(x => two.Contains(x));
            var f = one.First(x => !x.Equals(c));

            var six = signalpattern.First(x => x.Length == 6 && !x.Contains(c));
            var e = six.First(x => !five.Contains(x));
            var g = three.First(x => !seven!.Contains(x) && !x.Equals(d));
            var nine = signalpattern.First(x => x.Length == 6 && !x.Contains(e));

            sevenSegment['a'] = a;
            sevenSegment['b'] = b;
            sevenSegment['c'] = c;
            sevenSegment['d'] = d;
            sevenSegment['e'] = e;
            sevenSegment['f'] = f;
            sevenSegment['g'] = g;

            var outputvaluestring = "";
            foreach (var output in outputvalues)
            {
                var outputreal = new char[output.Length];
                
                for (var o = 0; o < output.Length; o++)
                {
                    outputreal[o] = sevenSegment.Single(x => x.Value == output[o]).Key;
                }
                outputreal = outputreal.OrderBy(x => x).ToArray();
                for (var number = 0; number < arrayOfNumbers.Length; number++)
                {
                    if (outputreal.SequenceEqual(arrayOfNumbers[number]))
                    {
                        outputvaluestring += number.ToString();
                        break;
                    }
                }
            }
            totalSum += int.Parse(outputvaluestring);
        }
        Console.WriteLine(totalSum);
        Console.ReadKey();
    }
}
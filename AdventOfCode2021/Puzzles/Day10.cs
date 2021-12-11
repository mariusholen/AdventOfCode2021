namespace AdventOfCode2021.Puzzles;
public class Day10
{
    public Day10(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var points = 0;
        var openings = new char[4] { '(', '[', '{', '<' };
        var closings = new char[4] { ')', ']', '}', '>' };
        var pairs = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        var scoretable = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        foreach (var value in values)
        {
            var opens = new Stack<char>();
            for (var i = 0; i < value.Length; i++)
            {
                if (openings.Contains(value[i]))
                {
                    opens.Push(value[i]);
                }
                else
                {
                    if (opens.TryPeek(out var expectedOpen))
                    {
                        var actual = pairs.First(x => x.Value == value[i]);
                        if (expectedOpen.Equals(actual.Key))
                            opens.Pop();
                        else
                        {
                            points += scoretable.First(x => x.Key == value[i]).Value;
                            break;
                        }
                    }
                }
            }
        }
        Console.WriteLine(points);
        Console.ReadKey();
    }
    public static void PuzzleB(string[] values)
    {
        var totalScore = new List<long>();
        var openings = new char[4] { '(', '[', '{', '<' };
        var closings = new char[4] { ')', ']', '}', '>' };
        var pairs = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        var scoretable = new Dictionary<char, int> { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };
        foreach (var value in values)
        {
            long score = 0;
            var opens = new Stack<char>();
            for (var i = 0; i < value.Length; i++)
            {
                if (openings.Contains(value[i]))
                {
                    opens.Push(value[i]);
                }
                else
                {
                    if (opens.TryPeek(out var expectedOpen))
                    {
                        var actual = pairs.First(x => x.Value == value[i]);
                        if (expectedOpen.Equals(actual.Key))
                            opens.Pop();
                        else
                        {
                            opens.Clear();
                            break;
                        }
                    }
                }
            }
            foreach (var open in opens)
            {
                var closing = pairs.First(x => x.Key == open).Value;
                var points = scoretable.First(x => x.Key == closing).Value;
                score *= 5;
                score += points;
            }
            totalScore.Add(score);
        }
        var totalArr = totalScore.Where(x => x != 0).OrderBy(x => x).ToArray();
        var middle = (totalArr.Count() / 2);
        Console.WriteLine(totalArr[middle]);
        Console.ReadKey();
    }
}
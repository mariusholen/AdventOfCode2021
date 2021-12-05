namespace AdventOfCode2021.Puzzles;
public class Day5
{
    public Day5(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var diagram = new Dictionary<(int, int), int>();
        foreach (var value in values)
        {
            var startIndex = 0;
            var endIndex = 0;
            var ventline = new VentLine(value);
            if (ventline.IsXAxis)
            {
                if (ventline.FirstYIsSmallest)
                {
                    startIndex = ventline.Y1;
                    endIndex = ventline.Y2;
                }
                else
                {
                    startIndex = ventline.Y2;
                    endIndex = ventline.Y1;
                };
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var xAxis = ventline.X1;
                    if (diagram.ContainsKey((xAxis, i)))
                        diagram[(xAxis, i)]++;
                    else
                        diagram.Add((xAxis, i), 1);
                }
            }
            if (ventline.IsYAxis)
            {
                if (ventline.FirstXIsSmallest)
                {
                    startIndex = ventline.X1;
                    endIndex = ventline.X2;
                }
                else
                {
                    startIndex = ventline.X2;
                    endIndex = ventline.X1;
                };
                for (int i = startIndex; i <= endIndex; i++)
                {
                    var yAxis = ventline.Y1;
                    if (diagram.ContainsKey((i, yAxis)))
                        diagram[(i, yAxis)]++;
                    else
                        diagram.Add((i, yAxis), 1);
                }   
            }
        }
        var moreThanTwoLines = diagram.Where(x => x.Value >= 2);
        Console.WriteLine($"{moreThanTwoLines.Count()}");
        Console.ReadKey();
    }
    public static void PuzzleB(string[] values)
    {
        var diagram = new Dictionary<(int, int), int>();
        foreach (var value in values)
        {
            var xStartIndex = 0;
            var xEndIndex = 0;
            var yStartIndex = 0;
            var yEndIndex = 0;
            var directionUp = true;
            var ventline = new VentLine(value);
            if (ventline.IsXAxis)
            {
                if (ventline.FirstYIsSmallest)
                {
                    yStartIndex = ventline.Y1;
                    yEndIndex = ventline.Y2;
                }
                else
                {
                    yStartIndex = ventline.Y2;
                    yEndIndex = ventline.Y1;
                };
                for (int i = yStartIndex; i <= yEndIndex; i++)
                {
                    var xAxis = ventline.X1;
                    if (diagram.ContainsKey((xAxis, i)))
                        diagram[(xAxis, i)]++;
                    else
                        diagram.Add((xAxis, i), 1);
                }
            }
            else if (ventline.IsYAxis)
            {
                if (ventline.FirstXIsSmallest)
                {
                    xStartIndex = ventline.X1;
                    xEndIndex = ventline.X2;
                }
                else
                {
                    xStartIndex = ventline.X2;
                    xEndIndex = ventline.X1;
                };
                for (int i = xStartIndex; i <= xEndIndex; i++)
                {
                    var yAxis = ventline.Y1;
                    if (diagram.ContainsKey((i, yAxis)))
                        diagram[(i, yAxis)]++;
                    else
                        diagram.Add((i, yAxis), 1);
                }
            }
            else
            {
                if (ventline.FirstXIsSmallest)
                {
                    xStartIndex = ventline.X1;
                    xEndIndex = ventline.X2;
                    yStartIndex = ventline.Y1;
                    yEndIndex = ventline.Y2;
                    directionUp = yStartIndex < yEndIndex;
                }
                else
                {
                    xStartIndex = ventline.X2;
                    xEndIndex = ventline.X1;
                    yStartIndex = ventline.Y2;
                    yEndIndex = ventline.Y1;
                    directionUp = yStartIndex < yEndIndex;
                };
                if (directionUp)
                {
                    int y = yStartIndex;
                    for (int i = xStartIndex; i <= xEndIndex; i++)
                    {
                        if (diagram.ContainsKey((i, y)))
                            diagram[(i, y)]++;
                        else
                            diagram.Add((i, y), 1);
                        if (y >= yEndIndex)
                            break;
                        y++;
                    }
                }
                else
                {
                    int y = yStartIndex;
                    for (int i = xStartIndex; i <= xEndIndex; i++)
                    {
                        if (diagram.ContainsKey((i, y)))
                            diagram[(i, y)]++;
                        else
                            diagram.Add((i, y), 1);
                        if (y <= yEndIndex)
                            break;
                        y--;
                    }
                }
            }
        }
        var moreThanTwoLines = diagram.Where(x => x.Value >= 2);
        Console.WriteLine($"{moreThanTwoLines.Count()}");
        Console.ReadKey();
    }
}

public class VentLine
{
    public VentLine(string value)
    {
        var beginning = value.Split(" -> ").First();
        var end = value.Split(" -> ").Last();
        X1 = GetAxisLineFromPoint(beginning, true);
        X2 = GetAxisLineFromPoint(end, true);
        Y1 = GetAxisLineFromPoint(beginning, false);
        Y2 = GetAxisLineFromPoint(end, false);
    }
    public int X1 { get; set; }
    public int X2 { get; set; }
    public int Y1 { get; set; }
    public int Y2 { get; set; }

    public bool IsXAxis => X1 == X2;
    public bool IsYAxis => Y1 == Y2;
    public bool FirstXIsSmallest => X1 < X2;
    public bool FirstYIsSmallest => Y1 < Y2;

    private static int GetAxisLineFromPoint(string point, bool isFirst)
    {
        var axis = point.Split(",");
        return isFirst ? int.Parse(axis.First()) : int.Parse(axis.Last());
    }
}
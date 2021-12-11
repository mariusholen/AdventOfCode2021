namespace AdventOfCode2021;
public class Initialize
{
    public Initialize(int day)
    {
        var uri = ConfigureUri(day);
        var cookie = ConfigureCookie();
        InitializePuzzle(uri, cookie, day);
    }
    public static string ConfigureUri(int day) => $"https://adventofcode.com/2021/day/{day}/input";

    public static string ConfigureCookie()
    {
        string? cookie = Environment.GetCommandLineArgs().LastOrDefault();

        if (string.IsNullOrWhiteSpace(cookie) || !cookie.Contains("session="))
        {
            Console.WriteLine("Please insert your cookie from https://adventofcode.com/ in the format 'session=xyz123'");
            cookie = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cookie) || !cookie.Contains("session="))
            {
                Console.WriteLine("No cookie inserted. Please restart and try again");
                Environment.Exit(1);
            }
        }
        return cookie;
    }

    public static void InitializePuzzle(string uri, string cookie, int day)
    {
        Uri adventOfCodeUri = new(uri);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", cookie);
        var response = client.GetStringAsync(adventOfCodeUri).Result;
        string[] values = response.Split("\n")
            .Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        switch (day)
        {
            case 1:
                new Day1(values);
                break;
            case 2:
                new Day2(values);
                break;
            case 3:
                new Day3(values);
                break;
            case 4:
                new Day4(values);
                break;
            case 5:
                new Day5(values);
                break;
            case 6:
                new Day6(values);
                break;
            case 7:
                new Day7(values);
                break;
            case 8:
                new Day8(values);
                break;
            default:
                Console.WriteLine($"Puzzle has not yet been created for day {day}. Please come back later");
                break;

        };
    }
}

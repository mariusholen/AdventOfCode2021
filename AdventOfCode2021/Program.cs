Console.WriteLine("Welcome to this Advent of Code 2021 solver! Please enter the date (1-24) you want to explore");
var isValidInput = int.TryParse(Console.ReadLine(), out int day);
if (!isValidInput && day < 1 && day > 24)
{
    Console.WriteLine("Inserted value is either not a number, or it is outside of valid range (1-24). Please restart and try again");
    Environment.Exit(1);
}

new Initialize(day);

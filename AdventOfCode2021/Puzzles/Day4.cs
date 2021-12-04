namespace AdventOfCode2021.Puzzles;
public class Day4
{
    public Day4(string[] values)
    {
        PuzzleA(values);
        PuzzleB(values);
    }
    public static void PuzzleA(string[] values)
    {
        var drawnumbers = values[0].Split(",");
        var boards = SeedBoards(values);
        foreach (var drawnumber in drawnumbers)
        {
            boards = MarkBoards(boards, drawnumber);
            var winnerIndex = CheckForWinner(boards);
            if (winnerIndex.HasValue)
            {
                CalculateScore(boards[winnerIndex.Value], drawnumber);
                break;
            }
        }
    }


    public static void PuzzleB(string[] values)
    {
        var drawnumbers = values[0].Split(",");
        var boards = SeedBoards(values);
        var lastWinner = new Board();
        var winnerDrawNumber = string.Empty;
        foreach (var drawnumber in drawnumbers)
        {
            boards = MarkBoards(boards, drawnumber);
            var winnerIndex = CheckForWinner(boards);
            if (winnerIndex.HasValue)
            {
                lastWinner = boards[winnerIndex.Value];
                winnerDrawNumber = drawnumber;
                var boardsList = boards.ToList();
                boardsList.RemoveAt(winnerIndex.Value);
                boards = boardsList.ToArray();
                (boards, lastWinner) = LoopForMoreWinners(boards, lastWinner);
            }
        }
        CalculateScore(lastWinner, winnerDrawNumber);
    }

    private static (Board[], Board) LoopForMoreWinners(Board[] boards, Board lastwinnerIn)
    {
        var lastWinner = lastwinnerIn;
        var winnerIndex = CheckForWinner(boards);
        if (winnerIndex.HasValue)
        {
            lastWinner = boards[winnerIndex.Value];
            var boardsList = boards.ToList();
            boardsList.RemoveAt(winnerIndex.Value);
            boards = boardsList.ToArray();
            LoopForMoreWinners(boards, lastWinner);
        }
        return (boards, lastWinner);
    }


    private static Board[] SeedBoards(string[] values)
    {
        var length = (values.Length - 1) / 5;
        var boardArray = new Board[length];
        for (int i = 0; i < length; i++)
        {
            var board = new Board();
            var boardValues = values.Skip((i * 5) + 1).Take(5).ToArray();
            for (int j = 0; j < boardValues.Length; j++)
            {
                var row = boardValues[j].Split(" ");
                row = row.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                for (int k = 0; k < row.Length; k++)
                {
                    board.FiveXFive[j, k] = row[k];
                }
            }
            boardArray[i] = board;
        }
        return boardArray;
    }

    private static Board[] MarkBoards(Board[] boards, string drawnumber)
    {
        foreach (var board in boards)
        {
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 5; j++)
                {
                    var value = board.FiveXFive[i, j];
                    if (drawnumber.Equals(value))
                    {
                        board.FiveXFive[i, j] = string.Empty;
                    }
                }
            }
        }
        return boards;
    }

    private static int? CheckForWinner(Board[] boards)
    {
        for (var i = 0; i < boards.Length; i++)
        {
            //Rowcheck
            for (var r = 0; r < 5; r++)
            {
                var numbers = "";
                for (var c = 0; c < 5; c++)
                {
                    numbers += boards[i].FiveXFive[r, c];
                }
                if (string.IsNullOrWhiteSpace(numbers))
                    return i;
            }
            //Columncheck
            for (var c = 0; c < 5; c++)
            {
                var numbers = "";
                for (var r = 0; r < 5; r++)
                {
                    numbers += boards[i].FiveXFive[r, c];
                }
                if (string.IsNullOrWhiteSpace(numbers))
                    return i;
            }
        }
        return null;
    }

    private static void CalculateScore(Board winner, string drawnumber)
    {
        var remainingSum = 0;
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                var value = winner.FiveXFive[i, j];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    remainingSum += int.Parse(value);
                }
            }
        }
        var finalScore = remainingSum * int.Parse(drawnumber);
        Console.WriteLine($"{finalScore}");
        Console.ReadKey();
    } 
}

public class Board
{
    public string[,] FiveXFive { get; set; } = new string[5, 5];
}

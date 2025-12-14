using System.Text;

namespace AoC_2025.Day_4;

public class Day4
{
    public void Run()
    {
        var result = 0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 4", Constants.INPUT_PATH));

        var paperArr = new List<List<int>>();

        foreach (var line in lines)
        {
            var row = new List<int>();
            foreach (var ch in line) row.Add(ch == '@' ? 1 : 0);
            paperArr.Add(row);
        }

        for (var i = 0; i < paperArr.Count; i++)
        for (var j = 0; j < paperArr[i].Count; j++)
        {
            var pCount = 0;

            if (paperArr[i][j] != 1) continue;

            if (i > 0 && j > 0) pCount += paperArr[i - 1][j - 1];
            if (i > 0) pCount += paperArr[i - 1][j];
            if (i > 0 && j < paperArr[i].Count - 1) pCount += paperArr[i - 1][j + 1];
            if (j > 0) pCount += paperArr[i][j - 1];
            if (j < paperArr[i].Count - 1) pCount += paperArr[i][j + 1];
            if (i < paperArr.Count - 1 && j > 0) pCount += paperArr[i + 1][j - 1];
            if (i < paperArr.Count - 1) pCount += paperArr[i + 1][j];
            if (i < paperArr.Count - 1 && j < paperArr[i].Count - 1) pCount += paperArr[i + 1][j + 1];

            if (pCount < 4) result++;
        }

        Console.WriteLine(result);
    }

    public void RunTwo()
    {
        var result = 0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 4", Constants.INPUT_PATH));

        var paperArr = new List<List<int>>();

        foreach (var line in lines)
        {
            var row = new List<int>();
            foreach (var ch in line) row.Add(ch == '@' ? 1 : 0);
            paperArr.Add(row);
        }

        while (true)
        {
            var allCount = 0;
            for (var i = 0; i < paperArr.Count; i++)
            for (var j = 0; j < paperArr[i].Count; j++)
            {
                var pCount = 0;

                if (paperArr[i][j] != 1) continue;

                if (i > 0 && j > 0) pCount += paperArr[i - 1][j - 1];
                if (i > 0) pCount += paperArr[i - 1][j];
                if (i > 0 && j < paperArr[i].Count - 1) pCount += paperArr[i - 1][j + 1];
                if (j > 0) pCount += paperArr[i][j - 1];
                if (j < paperArr[i].Count - 1) pCount += paperArr[i][j + 1];
                if (i < paperArr.Count - 1 && j > 0) pCount += paperArr[i + 1][j - 1];
                if (i < paperArr.Count - 1) pCount += paperArr[i + 1][j];
                if (i < paperArr.Count - 1 && j < paperArr[i].Count - 1) pCount += paperArr[i + 1][j + 1];

                if (pCount < 4)
                {
                    allCount++;
                    paperArr[i][j] = 0;
                }
            }

            result += allCount;
            if (allCount == 0) break;
        }

        Console.WriteLine(result);
    }
}
using System.Text;

namespace AoC_2025.Day_5;

public class Day5
{
    public void Run()
    {
        var result = 0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 5", Constants.INPUT_PATH));

        var isRangeMode = true;

        var ranges = new List<(long, long)>();
        var nums = new List<long>();

        foreach (var line in lines)
        {
            if (line.Trim().Length == 0)
            {
                isRangeMode = false;
                continue;
            }

            if (isRangeMode)
            {
                var a = line.Split('-');
                ranges.Add((Convert.ToInt64(a[0]), Convert.ToInt64(a[1])));
            }
            else
            {
                nums.Add(Convert.ToInt64(line));
            }
        }

        foreach (var num in nums)
        foreach (var range in ranges)
        {
            if (num < range.Item1 || num > range.Item2) continue;
            result++;
            break;
            ;
        }

        Console.WriteLine(result);
    }

    public void RunTwo()
    {
        var result = (long)0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 5", Constants.INPUT_PATH));

        var ranges = new List<(long, long)>();

        foreach (var line in lines)
        {
            if (line.Trim().Length == 0) break;

            var a = line.Split('-');
            ranges.Add((Convert.ToInt64(a[0]), Convert.ToInt64(a[1])));
        }

        ranges.Sort();

        var mergedRanges = new List<(long, long)>();

        for (var i = 0; i < ranges.Count; i++)
        {
            var currentStart = ranges[i].Item1;
            var currentEnd = ranges[i].Item2;

            while (i + 1 < ranges.Count && ranges[i + 1].Item1 <= currentEnd)
            {
                if (ranges[i + 1].Item2 > currentEnd) currentEnd = ranges[i + 1].Item2;
                i++;
            }

            mergedRanges.Add((currentStart, currentEnd));
        }

        foreach (var mergedRange in mergedRanges) result += mergedRange.Item2 - mergedRange.Item1 + 1;

        Console.WriteLine(result);
    }
}
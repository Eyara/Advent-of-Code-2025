using System.Text;

namespace AoC_2025.Day_9;

public class Day9
{
    public void Run()
    {
        var result = (long)0;

        var points = new List<(long, long)>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 9", Constants.INPUT_PATH));

        foreach (var line in lines)
        {
            var lineSplit = line.Split(',');
            points.Add((Convert.ToInt64(lineSplit[0]), Convert.ToInt64(lineSplit[1])));
        }

        for (var i = 0; i < points.Count; i++)
        for (var j = 1; j < points.Count; j++)
        {
            var currentArea = CalculateArea(points[i], points[j]);
            if (currentArea > result
                && !points.Exists(x => x == (points[i].Item1, points[j].Item2))
                && !points.Exists(x => x == (points[j].Item1, points[i].Item2)))
                result = currentArea;
        }

        Console.WriteLine(result);
    }
    private long CalculateArea((long, long) a, (long, long) b)
    {
        return (Math.Abs(b.Item1 - a.Item1) + 1) * (Math.Abs(b.Item2 - a.Item2) + 1);
    }
}
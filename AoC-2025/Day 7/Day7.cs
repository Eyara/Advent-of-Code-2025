using System.Text;

namespace AoC_2025.Day_7;

public class Day7
{
    public void Run()
    {
        var result = 0;
        var beams = new List<int>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 7", Constants.INPUT_PATH));

        beams.Add(lines[0].IndexOf('S'));

        for (var i = 1; i < lines.Length - 1; i++)
        {
            var idxs = FindAllIndexesOf(lines[i], "^");
            var newBeams = new List<int>();
            for (var j = 0; j < beams.Count; j++)
            {
                var val = beams[j];
                var occur = idxs.IndexOf(beams[j]);
                if (occur >= 0)
                {
                    if (val > 0) newBeams.Add(val - 1);
                    if (val < lines[i].Length - 1) newBeams.Add(val + 1);

                    result++;
                    continue;
                }

                newBeams.Add(beams[j]);
            }

            newBeams.Sort();
            beams = newBeams.Distinct().ToList();
        }

        Console.WriteLine(result);
    }

    public void RunTwo()
    {
        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 7", Constants.INPUT_PATH));

        var beams = new Dictionary<int, long>();

        beams[lines[0].IndexOf('S')] = 1;

        for (var i = 1; i < lines.Length - 1; i++)
        {
            var idxs = FindAllIndexesOf(lines[i], "^");
            var nextBeams = new Dictionary<int, long>();

            foreach (var (pos, count) in beams)
                if (idxs.Contains(pos))
                {
                    if (pos > 0)
                        nextBeams[pos - 1] = nextBeams.GetValueOrDefault(pos - 1) + count;

                    if (pos < lines[i].Length - 1)
                        nextBeams[pos + 1] = nextBeams.GetValueOrDefault(pos + 1) + count;
                }
                else
                {
                    nextBeams[pos] = nextBeams.GetValueOrDefault(pos) + count;
                }

            beams = nextBeams;
        }

        var result = beams.Values.Sum();
        Console.WriteLine(result);
    }

    public static List<int> FindAllIndexesOf(string source, string searchString)
    {
        var indexes = new List<int>();
        var index = 0;
        while ((index = source.IndexOf(searchString, index)) != -1)
        {
            indexes.Add(index);
            index += searchString.Length;
        }

        return indexes;
    }
}
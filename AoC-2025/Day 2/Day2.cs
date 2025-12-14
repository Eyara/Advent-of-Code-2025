namespace AoC_2025.Day_2;

public class Day2
{
    public void Run()
    {
        var result = (long)0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 2", Constants.INPUT_PATH));

        foreach (var pair in lines[0].Split(','))
        {
            var pairArr = pair.Split('-');
            var a = Convert.ToInt64(pairArr[0]);
            var b = Convert.ToInt64(pairArr[1]);

            while (a <= b)
            {
                var aStr = a.ToString();
                if (a.ToString().Length % 2 != 0)
                {
                    a++;
                    continue;
                }

                var halfLength = aStr.Length / 2;

                if (aStr.Substring(0, halfLength) == aStr.Substring(halfLength, halfLength))
                    result += a;

                a++;
            }
        }

        Console.WriteLine(result);
    }

    public void RunTwo()
    {
        var result = new List<long>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 2", Constants.INPUT_PATH));

        foreach (var pair in lines[0].Split(','))
        {
            var pairArr = pair.Split('-');
            var a = Convert.ToInt64(pairArr[0]);
            var b = Convert.ToInt64(pairArr[1]);

            while (a <= b)
            {
                var s = a.ToString();
                var len = s.Length;

                for (var step = 1; step <= len / 2; step++)
                {
                    if (len % step != 0)
                        continue;

                    var pattern = s.Substring(0, step);
                    var ok = true;

                    for (var j = step; j < len; j += step)
                        if (s.Substring(j, step) != pattern)
                        {
                            ok = false;
                            break;
                        }

                    if (ok)
                        result.Add(a);
                }

                a++;
            }
        }

        Console.WriteLine(result.Distinct().Sum());
    }
}
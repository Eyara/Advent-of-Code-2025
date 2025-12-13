using System.Text;

namespace AoC_2025.Day_3;

public class Day3
{
    public void Run()
    {
        var result = 0;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 3", Constants.INPUT_PATH));

        foreach (var line in lines)
        {
            var highest = -1;
            var secondHighest = -1;
            
            for (var i = 0; i < line.Length; i++)
            {
                var val = Convert.ToInt32(line[i].ToString());

                if (val > highest && i < line.Length - 1)
                {
                    highest = val;
                    secondHighest = -1;
                }
                else if (val > secondHighest)
                {
                    secondHighest = val;
                }
            }

            result += 10 * highest + secondHighest;
        }
        
        Console.WriteLine(result);
    }
    
    public void RunTwo()
    {
        var result = (long)0;
        var n = 12;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 3", Constants.INPUT_PATH));

        foreach (var line in lines)
        {
            var resultStr = string.Empty;
            var l = line.Length;

            var lastOccurIdx = -1;
            while (resultStr.Length < 12)
            {
                var maxVal = line.Substring(lastOccurIdx + 1, l - lastOccurIdx - (n - resultStr.Length)).Max();
                resultStr += maxVal;
                lastOccurIdx = line.IndexOf(maxVal, lastOccurIdx + 1);;
            }

            result += Convert.ToInt64(resultStr);
            Console.WriteLine(Convert.ToInt64(resultStr));
        }
        
        Console.WriteLine(result);
    }
}
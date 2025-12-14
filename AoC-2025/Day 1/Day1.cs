namespace AoC_2025.Day_1;

public class Day1
{
    public void Run()
    {
        var result = 0;
        var start = 50;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 1", Constants.INPUT_PATH));
        foreach (var line in lines)
        {
            var instruction = line[0];
            var val = Convert.ToInt32(line.Substring(1, line.Length - 1));

            val %= 100;

            var multiplier = instruction == 'L' ? -1 : 1;

            start += multiplier * val;

            switch (start)
            {
                case 0 or 100:
                    result++;
                    break;
                case < 0:
                    start = 100 - Math.Abs(start);
                    break;
                case > 100:
                    start = Math.Abs(start);
                    break;
            }

            if (start >= 100) start -= 100;

            Console.WriteLine(start);
        }

        Console.WriteLine($"Answer: {result}");
    }

    public void RunPartTwo()
    {
        var result = 0;
        var start = 50;
        var lastInstruction = 'X';

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 1", Constants.INPUT_PATH));
        foreach (var line in lines)
        {
            var instruction = line[0];
            var val = Convert.ToInt32(line.Substring(1, line.Length - 1));

            if (start >= 100)
                start -= 100;

            result += val / 100;
            val %= 100;

            var multiplier = instruction == 'L' ? -1 : 1;

            start += multiplier * val;

            switch (start)
            {
                case 0 or 100:
                    result++;
                    lastInstruction = instruction;
                    break;
                case < 0:
                    start = 100 - Math.Abs(start);
                    if (instruction == lastInstruction) result++;

                    break;
                case > 100:
                    start = Math.Abs(start);
                    result++;
                    break;
            }
        }

        Console.WriteLine($"Answer: {result}");
    }
}
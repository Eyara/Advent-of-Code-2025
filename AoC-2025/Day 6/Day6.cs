using System.Text;

namespace AoC_2025.Day_6;

public class Day6
{
    public void Run()
    {
        var result = (long)0;

        var nums = new List<List<long>>();
        var operations = new List<string>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 6", Constants.INPUT_PATH));

        for (var i = 0; i < lines.Length; i++)
            if (i < lines.Length - 1)
                nums.Add(lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt64(x.Trim())).ToList());
            else
                operations = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

        var columnCount = nums[0].Count;

        for (var col = 0; col < columnCount; col++)
        {
            var columnValues = nums.Select(row => row[col]).ToList();
            var operation = operations[col];
            var colResult = operation == "+" ? (long)0 : 1;

            for (var i = 0; i < columnValues.Count; i++)
                switch (operation)
                {
                    case "+":
                        colResult += columnValues[i];
                        break;
                    case "*":
                        colResult *= columnValues[i];
                        break;
                }

            result += colResult;
        }

        Console.WriteLine(result);
    }

    public void RunTwo()
    {
        var result = (long)0;

        var rows = new List<string>();
        var numParsedStrings = new List<List<string>>();
        var nums = new List<List<long>>();
        var operations = new List<string>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 6", Constants.INPUT_PATH));

        for (var i = 0; i < lines.Length; i++)
            if (i < lines.Length - 1)
            {
                var currentLine = lines[i];
                rows.Add(currentLine);
                nums.Add(currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToInt64(x.Trim())).ToList());
            }
            else
            {
                operations = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim())
                    .ToList();
            }

        var columnCount = nums[0].Count;

        var startIdx = 0;
        for (var col = 0; col < columnCount; col++)
        {
            var columnMaxLength = nums.Select(row => row[col].ToString()).MaxBy(row => row.Length).Length;
            var tmpList = new List<string>();
            foreach (var row in rows) tmpList.Add(row.Substring(startIdx, columnMaxLength));
            numParsedStrings.Add(tmpList);
            startIdx += columnMaxLength + 1;
        }

        for (var i = 0; i < operations.Count; i++)
        {
            var operation = operations[i];
            var colResult = operation == "+" ? (long)0 : 1;

            var groupedNumbers = GetColumnGroupedNumbers(numParsedStrings[i]);

            foreach (var number in groupedNumbers)
                switch (operation)
                {
                    case "+":
                        colResult += number;
                        break;
                    case "*":
                        colResult *= number;
                        break;
                }

            result += colResult;
        }

        Console.WriteLine(result);
    }

    private static List<long> GetColumnGroupedNumbers(List<string> strings)
    {
        if (strings == null || strings.Count == 0)
            return new List<long>();

        long maxLength = strings.Max(s => s.Length);

        var resultBuilders = new StringBuilder[maxLength];
        for (var i = 0; i < maxLength; i++) resultBuilders[i] = new StringBuilder();

        foreach (var current in strings)
            for (var col = 0; col < current.Length; col++)
            {
                var ch = current[col];
                if (char.IsDigit(ch)) resultBuilders[col].Append(ch);
            }

        var result = new List<long>();
        foreach (var builder in resultBuilders)
        {
            var combinedDigits = builder.ToString();
            if (!string.IsNullOrEmpty(combinedDigits))
                if (long.TryParse(combinedDigits, out var number))
                    result.Add(number);
        }

        return result;
    }
}
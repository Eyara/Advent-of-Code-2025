namespace AoC_2025.Day_2;

public class Day2
{
    public void Run()
    {
        var result = new List<int>();

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 2", Constants.TEST_PATH));

        foreach (var pair in lines[0].Split(','))
        {
            var pairArr = pair.Split('-');
            var a = Convert.ToInt64(pairArr[0]);
            var b = Convert.ToInt64(pairArr[1]);
            
            // var aa = a.ToString()
            Console.WriteLine(b - a);
        }
        
        Console.WriteLine();
    }
}
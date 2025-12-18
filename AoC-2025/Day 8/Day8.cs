using System.Text;

namespace AoC_2025.Day_8;

public class Day8
{
    public void Run()
    {
        var result = (long)1;
        var pairThreshold = 1000;

        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 8", Constants.INPUT_PATH));

        var points = new List<Point3>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');

            var point = new Point3
            {
                X = int.Parse(parts[0]),
                Y = int.Parse(parts[1]),
                Z = int.Parse(parts[2])
            };

            points.Add(point);
        }

        var pointInfos = new List<PointInfo>();

        for (var i = 0; i < points.Count; i++)
            pointInfos.Add(new PointInfo
            {
                Point = points[i],
                ClusterId = i
            });

        for (var i = 0; i < pointInfos.Count; i++)
        for (var j = 0; j < pointInfos.Count; j++)
        {
            if (i == j) continue;

            var dist = Distance(pointInfos[i].Point, pointInfos[j].Point);
            pointInfos[i].Distances[j] = dist;
        }

        var directConnections = new HashSet<(int, int)>();

        while (pairThreshold > 0)
        {
            var minDist = double.MaxValue;
            var idxA = -1;
            var idxB = -1;

            for (var i = 0; i < pointInfos.Count; i++)
            for (var j = i + 1; j < pointInfos.Count; j++)
            {
                if (directConnections.Contains((i, j)))
                    continue;

                var d = pointInfos[i].Distances[j];
                if (d < minDist)
                {
                    minDist = d;
                    idxA = i;
                    idxB = j;
                }
            }

            if (idxA == -1 || idxB == -1)
                break;

            var oldCluster = pointInfos[idxB].ClusterId;
            var newCluster = pointInfos[idxA].ClusterId;

            if (oldCluster == newCluster)
            {
                directConnections.Add((idxA, idxB));
                pairThreshold--;
                continue;
            }

            for (var k = 0; k < pointInfos.Count; k++)
                if (pointInfos[k].ClusterId == oldCluster)
                    pointInfos[k].ClusterId = newCluster;

            directConnections.Add((idxA, idxB));
            pairThreshold--;
        }


        var clusterSizes = pointInfos
            .GroupBy(p => p.ClusterId)
            .Select(g => g.Count())
            .OrderByDescending(x => x)
            .Take(3)
            .ToList();

        foreach (var size in clusterSizes)
            result *= size;

        Console.WriteLine(result);
    }

    public void RunPartTwo()
    {
        var lines = File.ReadAllLines(Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)
                .Parent
                .Parent
                .Parent!.FullName, "Day 8", Constants.INPUT_PATH));

        var points = new List<(int X, int Y, int Z)>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(',');
            points.Add((
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            ));
        }

        var distances = new List<(long Distance, int IndexA, int IndexB)>();

        for (var i = 0; i < points.Count; i++)
        for (var j = i + 1; j < points.Count; j++)
        {
            var dist = DistanceSquared(points[i], points[j]);
            distances.Add((dist, i, j));
        }

        distances.Sort((a, b) => a.Distance.CompareTo(b.Distance));

        var parent = new int[points.Count];
        var rank = new int[points.Count];

        for (var i = 0; i < points.Count; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }

        var lastIndexA = -1;
        var lastIndexB = -1;
        var connectionsMade = 0;

        foreach (var (_, indexA, indexB) in distances)
        {
            var rootA = Find(parent, indexA);
            var rootB = Find(parent, indexB);

            if (rootA != rootB)
            {
                Union(parent, rank, indexA, indexB);
                connectionsMade++;

                lastIndexA = indexA;
                lastIndexB = indexB;
                if (connectionsMade == points.Count - 1)
                    break;
            }
        }

        var result = (long)points[lastIndexA].X * points[lastIndexB].X;
        Console.WriteLine($"Part Two: {result}");
    }

    private static long DistanceSquared((int X, int Y, int Z) p, (int X, int Y, int Z) q)
    {
        long dx = p.X - q.X;
        long dy = p.Y - q.Y;
        long dz = p.Z - q.Z;
        return dx * dx + dy * dy + dz * dz;
    }

    private static int Find(int[] parent, int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent, parent[x]);
        return parent[x];
    }

    private static void Union(int[] parent, int[] rank, int x, int y)
    {
        var rootX = Find(parent, x);
        var rootY = Find(parent, y);
        if (rootX == rootY)
            return;

        if (rank[rootX] < rank[rootY])
        {
            parent[rootX] = rootY;
        }
        else if (rank[rootX] > rank[rootY])
        {
            parent[rootY] = rootX;
        }
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }

    private static double Distance(Point3 a, Point3 b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        var dz = a.Z - b.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }

    private struct Point3
    {
        public int X;
        public int Y;
        public int Z;
    }

    private class PointInfo
    {
        public Point3 Point;
        public int ClusterId;
        public Dictionary<int, double> Distances = new();
    }
}
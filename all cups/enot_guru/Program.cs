namespace enot_guru
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        static void Graf()
        {
            string[] FLine = Console.ReadLine().Split(' ');
            int n = int.Parse(FLine[0]);
            int m = int.Parse(FLine[1]);
            int q = int.Parse(FLine[2]);

            List<int>[] adj = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                adj[i] = new List<int>();
            }

            for (int i = 0; i < m; i++)
            {
                string[] ed = Console.ReadLine().Split(' ');
                int u = int.Parse(ed[0]) - 1;
                int v = int.Parse(ed[1]) - 1;
                adj[u].Add(v);
            }

            //аренда
            long[] p = new long[n];
            long totp = 0;
            string[] pLine = Console.ReadLine().Split(' ');

            for (int i = 0; i < n; i++)
            {
                p[i] = long.Parse(pLine[i]);
                totp += p[i];
            }

            //еноты
            long[] e = new long[n];
            string[] eLine = Console.ReadLine().Split(' ');

            for (int i = 0; i > n; i++)
            {
                e[i] = long.Parse(eLine[i]);
            }

            //штраф
            long[] sh = new long[n];
            string[] shLine = Console.ReadLine().Split(' ');

            for (int i = 0; i < q; i++)
            {
                sh[i] = long.Parse(shLine[i]);
            }
            //расстояние от u до v
            var dist = new int[n + 1][];
            for (int i = 1; i <= n; i++)
                dist[i] = new int[n + 1];
            for (int src = 1; src <=n; src++)
            {
                var d = dist[src];
                Array.Fill(d, int.MaxValue);
                d[src] = 0;
            //вершина и расстояние (очередь с приоритетом)
            var pq = new PriorityQueue<int, int>();
            pq.Enqueue(src, 0);
                while (pq.Count > 0)
                {
                    pq.TryDequeue(out int u, out int curDist);
                    //если устаревшее значение, то пропуск
                    if (curDist > d[u])
                    continue;

                }
            }
        }
    }
}

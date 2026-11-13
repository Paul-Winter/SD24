using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<int>[] adj;
    static List<int>[] revAdj;
    static int[] pathIndex;

    static void Main()
    {
        // Быстрый ввод-вывод для больших тестов
        var reader = new StreamReader(Console.OpenStandardInput());
        var firstLine = reader.ReadLine();
        if (firstLine == null) return;

        var token = firstLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int n = int.Parse(token[0]);
        int m = int.Parse(token[1]);
        int a = int.Parse(token[2]);
        int b = int.Parse(token[3]);

        adj = new List<int>[n + 1];
        revAdj = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            adj[i] = new List<int>();
            revAdj[i] = new List<int>();
        }

        for (int i = 0; i < m; i++)
        {
            var edgeLine = reader.ReadLine();
            if (edgeLine == null) continue;
            var edge = edgeLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int u = int.Parse(edge[0]);
            int v = int.Parse(edge[1]);
            adj[u].Add(v);
            revAdj[v].Add(u);
        }

        int k = int.Parse(reader.ReadLine());
        var pathLine = reader.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int[] p = new int[k];
        for (int i = 0; i < k; i++)
        {
            p[i] = int.Parse(pathLine[i]);
        }

        pathIndex = new int[n + 1];
        for (int i = 0; i <= n; i++) pathIndex[i] = -1;
        for (int i = 0; i < k; i++)
        {
            pathIndex[p[i]] = i;
        }

        // Шаг 1: BFS от финала назад, чтобы узнать, из каких вершин вообще достижим B
        bool[] canReachB = new bool[n + 1];
        Queue<int> qB = new Queue<int>();
        qB.Enqueue(b);
        canReachB[b] = true;
        while (qB.Count > 0)
        {
            int u = qB.Dequeue();
            foreach (int v in revAdj[u])
            {
                if (!canReachB[v])
                {
                    canReachB[v] = true;
                    qB.Enqueue(v);
                }
            }
        }

        // Шаг 2: Вычисляем R[u] — максимальный индекс на пути P, который может дойти до u.
        // Заполняем BFS по уровням от конца пути к началу, чтобы максимизировать значения.
        int[] R = new int[n + 1];
        for (int i = 0; i <= n; i++) R[i] = -1;

        Queue<int> qR = new Queue<int>();
        // Добавляем вершины пути в порядке УБЫВАНИЯ индекса
        for (int i = k - 1; i >= 0; i--)
        {
            if (R[p[i]] == -1)
            {
                R[p[i]] = i;
                qR.Enqueue(p[i]);
            }
        }

        while (qR.Count > 0)
        {
            int u = qR.Dequeue();
            foreach (int v in adj[u])
            {
                if (R[v] < R[u])
                {
                    R[v] = R[u];
                    qR.Enqueue(v);
                }
            }
        }

        // Шаг 3: Вычисляем L[u] — минимальный индекс на пути P, до которого u может дойти.
        // Заполняем BFS по уровням от начала пути к концу, чтобы минимизировать значения.
        int[] L = new int[n + 1];
        for (int i = 0; i <= n; i++) L[i] = int.MaxValue;

        Queue<int> qL = new Queue<int>();
        // Добавляем вершины пути в порядке ВОЗРАСТАНИЯ индекса
        for (int i = 0; i < k; i++)
        {
            if (L[p[i]] == int.MaxValue)
            {
                L[p[i]] = i;
                qL.Enqueue(p[i]);
            }
        }

        while (qL.Count > 0)
        {
            int u = qL.Dequeue();
            foreach (int v in revAdj[u])
            {
                if (L[v] > L[u])
                {
                    L[v] = L[u];
                    qL.Enqueue(v);
                }
            }
        }

        // Шаг 4: Проверяем наличие альтернативного перехода (моста/обхода)
        for (int u = 1; u <= n; u++)
        {
            if (R[u] == -1 || !canReachB[u]) continue;

            foreach (int v in adj[u])
            {
                if (!canReachB[v] || L[v] == int.MaxValue) continue;

                // Пропускаем ребра, которые и так являются частью привычной программы
                if (pathIndex[u] != -1 && pathIndex[v] != -1 && pathIndex[v] == pathIndex[u] + 1)
                    continue;

                // Если нашли прыжок «вперёд» или ветвление в обход участка пути
                if (R[u] < L[v])
                {
                    Console.WriteLine("YES");
                    return;
                }
            }
        }

        Console.WriteLine("NO");
    }
}

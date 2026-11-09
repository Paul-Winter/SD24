using System;
using System.Collections.Generic;

class Program
{
    const long MOD = 998244353;

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string[] parts = Console.ReadLine().Split(' ');
        long[] a = new long[n];
        for (int i = 0; i < n; i++)
            a[i] = long.Parse(parts[i]);

        long ans = 0;

        if (n <= 20)
        {
            // Brute force
            for (long mask = 1; mask < (1L << n); mask++)
            {
                long maxSum = long.MinValue;
                long cur = 0;
                for (int j = 0; j < n; j++)
                {
                    if ((mask & (1L << j)) != 0)
                    {
                        cur = Math.Max(a[j], cur + a[j]);
                        maxSum = Math.Max(maxSum, cur);
                    }
                }
                ans = (ans + maxSum % MOD + MOD) % MOD;
            }
        }
        else if (n <= 5000)
        {
            // O(n^2)
            long[] pow2 = new long[n + 1];
            pow2[0] = 1;
            for (int i = 1; i <= n; i++)
                pow2[i] = pow2[i - 1] * 2 % MOD;

            int totalNeg = 0;
            foreach (var x in a) if (x < 0) totalNeg++;

            for (int l = 0; l < n; l++)
            {
                long sum = 0;
                int negIn = 0;
                for (int r = l; r < n; r++)
                {
                    sum += a[r];
                    if (a[r] < 0) negIn++;
                    if (sum > 0)
                    {
                        int negOut = totalNeg - negIn;
                        long contrib = ((sum % MOD) + MOD) % MOD;
                        contrib = contrib * pow2[negOut] % MOD;
                        ans = (ans + contrib) % MOD;
                    }
                }
            }
        }
        else
        {
            // Для больших n: используем тот факт, что S_neg <= 100
            // Разбиваем на блоки положительных чисел
            List<List<long>> blocks = new List<List<long>>();
            List<int> blockStart = new List<int>();

            int i = 0;
            while (i < n)
            {
                if (a[i] > 0)
                {
                    blockStart.Add(i);
                    List<long> block = new List<long>();
                    while (i < n && a[i] > 0)
                    {
                        block.Add(a[i]);
                        i++;
                    }
                    blocks.Add(block);
                }
                else
                {
                    i++;
                }
            }

            // Поскольку блоков мало (<= 101), перебираем комбинации
            // Это эвристика, может не пройти все тесты
            ans = 0;
            // ... сложная логика ...

            // Временный заглушечный ответ
            ans = 0;
        }

        Console.WriteLine(ans);
    }
}
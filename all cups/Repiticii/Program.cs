using System;

public class Program
{
    public static void Main()
    {
        // Быстрое чтение входного потока
        string[] tokens = Console.ReadLine().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length < 4) return;

        long a = long.Parse(tokens[0]);
        long b = long.Parse(tokens[1]);
        long r = long.Parse(tokens[2]);
        long n = long.Parse(tokens[3]);

        long mod = 998244353;
        long mask = r - 1; // Так как r — степень двойки, это заменяет операцию % r

        // Создаем массив для подсчета количества вхождений каждого числа
        // Размер: r элементов. Для r = 2^26 это ровно 256 МБ памяти.
        int[] counts = new int[r];

        long curr = 0;
        counts[curr] = 1; // p_1 = 0

        // Случай 1: b чётное. Последовательность стабилизируется максимум за 30 шагов
        if ((b & 1) == 0)
        {
            for (long i = 2; i <= n; i++)
            {
                long next = (a + b * curr) & mask;
                if (next == curr)
                {
                    // Последовательность зациклилась на одном числе, прибавляем все оставшиеся шаги
                    counts[curr] = (int)((counts[curr] + (n - i + 1)) % mod);
                    break;
                }
                counts[next] = (counts[next] + 1) % (int)mod;
                curr = next;
            }
        }
        // Случай 2: b нечётное. Чисто периодическая последовательность
        else
        {
            // Находим длину периода L. Она гарантированно <= r.
            long L = 0;
            long checkCurr = 0;
            for (long i = 1; i <= r; i++)
            {
                checkCurr = (a + b * checkCurr) & mask;
                if (checkCurr == 0)
                {
                    L = i;
                    break;
                }
            }

            long K = n / L;
            long m = n % L;

            // Заполняем массив частот на основе структуры периода
            long currGen = 0;
            // Первый элемент p_1 = 0 (это 1-й шаг в периоде)
            counts[0] = (int)((K + (1 <= m ? 1 : 0)) % mod);

            for (int i = 2; i <= L; i++)
            {
                currGen = (a + b * currGen) & mask;
                counts[currGen] = (int)((K + (i <= m ? 1 : 0)) % mod);
            }
        }

        // Подсчитываем количество пар (x, x - 2)
        long totalPairs = 0;
        for (int x = 2; x < r; x++)
        {
            if (counts[x] > 0 && counts[x - 2] > 0)
            {
                long freqX = counts[x];
                long freqY = counts[x - 2];
                long prod = (freqX * freqY) % mod;
                totalPairs = (totalPairs + prod) % mod;
            }
        }

        Console.WriteLine(totalPairs);
    }
}

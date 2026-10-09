namespace Recurse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Факториалы, рассчитанные итерационным методом:");
            Console.WriteLine($"Факториал 3 равен {FactorialC(3)}");
            Console.WriteLine($"Факториал 4 равен {FactorialC(4)}");
            Console.WriteLine($"Факториал 5 равен {FactorialC(5)}");

            Console.WriteLine("Факториалы, рассчитанные рекурсивным методом:");
            Console.WriteLine($"Факториал 3 равен {FactorialR(3)}");
            Console.WriteLine($"Факториал 4 равен {FactorialR(4)}");
            Console.WriteLine($"Факториал 5 равен {FactorialR(5)}");

            string s = "Это тестовая строка!";
            Console.WriteLine($"Исходная строка: {s}");
            Console.Write($"Перевёрнутая строка: ");
            ReverseString(s);
        }
        public static int FactorialC(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        public static int FactorialR(int n)
        {
            int result;
            Console.WriteLine($"n = {n}");
            if (n == 1) return 1;
            result = FactorialR(n - 1) * n;
            return result;
        }
        public static void ReverseString(string str)
        {
            if (str.Length > 0)
            {
                ReverseString(str.Substring(1, str.Length - 1));
            }
            else
                return;
            Console.Write(str[0]);
        }
    }
}

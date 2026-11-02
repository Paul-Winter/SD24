using System.Threading.Channels;

namespace all_cups
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите n");
            long n1 = long.Parse(Console.ReadLine());
            if (n1 < 1 || n1 > Math.Pow(10, 6))
            {
                return;

            }
            
            long[] all_cups = new long[n1];
            
            for (int i = 0; i < n1; i++) 
            {
                Console.Write($"Введите {i + 1} - ");
                all_cups[i] = long.Parse(Console.ReadLine());
                if (all_cups[i] < -100 || all_cups[i] > Math.Pow(10, 8))
                {
                    return;
                }
            }

            //Разбить последовательность на подпоследовательности
            //Забрать не пустые
            //Посчитать их ёмкость
            //Сумму поделить на 998 244 353
            //Вывести результат
        }

        static long SubsegmentLengthSum(long[] arr)
        {
            int n = arr.Length;
            int a = 0;
            for (int i = 0; i < n; i++)
            {
                if (n - a > 1)
                {
                    for (int j = n - a; j > i; j--, a++)
                    {

                    }
                }
            }
        }

        static long SumOfSubsegments(long[] arr)
        {

            long maxSum = long.MinValue;
            long n = arr.Length;

            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    long sum = 0;
                    for (int k = i; k <= j; k++)
                    {
                        sum += arr[k];
                    }
                    maxSum = Math.Max(maxSum, sum);
                }
            }
            return maxSum;
        }


    }



    }



























using System.ComponentModel;
using System.Globalization;

namespace Unsafe
{
    internal class Program
    {
        unsafe static void Main(string[] args)
        {
            Console.WriteLine("\n________________________________________Указатели________________________________________\n");

            // объявление указателей:
            // тип* имя_переменной;
            int* ip;
            float* fp;
            //int* x, y;

            // & возвращает адрес
            int* p;
            int num = 12;
            p = &num;
            Console.WriteLine($"num = {num}");
            // * обращается к значению переменной по адресу
            Console.WriteLine($"*p = {*p}");

            int count = 99;
            int* pointer;

            pointer = &count;
            Console.WriteLine($"Исходное значение переменной count: {count}");
            *pointer = 12;
            Console.WriteLine($"Новое значение переменной count: {count}");

            Test test = new Test(42);
            fixed (int* p1 = &test.num)
            {
                Console.WriteLine($"Исходное значение переменной test.num: {*p1}");
                *p1 = 33;
                Console.WriteLine($"Новое значение переменной test.num: {*p1}");
            }

            MyStruct ms = new MyStruct();
            MyStruct* p2;
            p2 = &ms;
            p2->x = 13;
            p2->y = 24;
            Console.WriteLine($"Сумма равна: {p2->Sum()}");

            int x = 1;
            int y;
            double d;
            int* py = &y;
            double* pd = &d;
            Console.WriteLine(" int\t\tdouble\n");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine((uint)py + "     " + (uint)pd);
                py++;
                pd++;
                x += x;
            }

            int* px;
            int** q;
            x = 10;
            px = &x;
            q = &px;
            Console.WriteLine($"х = {**q}");

            // sizeof(тип)
            // тип *p = stackalloc тип[размер]
            int* ptrs = stackalloc int[3];
            ptrs[0] = 1;
            ptrs[1] = 2;
            ptrs[2] = 3;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"ptrs{i} = {ptrs[i]}");
            }

            Console.WriteLine("\n________________________________________Обнуляемые_типы________________________________________\n");

            // 1 вариант
            System.Nullable<int> i1;
            System.Nullable<float> f1;

            // 2 вариант
            int? i2;
            float? f2;

            int? count2 = null;
            if (count2.HasValue)
            {
                Console.WriteLine($"Переменная имеет значение: {count2.Value}");
            }
            else
            {
                Console.WriteLine($"У переменной отсутствует значение");
            }
            count2 = 99;
            if (count2.HasValue)
            {
                Console.WriteLine($"Переменная имеет значение: {count2.Value}");
            }
            else
            {
                Console.WriteLine($"У переменной отсутствует значение");
            }

            // обнуляемый_объект ?? значение_по_умолчанию;
            double? balance = 123.456;
            double newBalance;
            newBalance = balance ?? GetZeroBalance();
            Console.WriteLine($"Старое значение: {newBalance}");
            balance = null;
            newBalance = balance ?? 0.0;
            Console.WriteLine($"Новое значение: {newBalance}");

        }
        static double GetZeroBalance()
        {
            Console.WriteLine("В методе GetZeroBalance(): ");
            return 0.0;
        }
    }
    class Test
    {
        public int num;
        public Test(int n)
        {
            num = n;
        }
    }
    struct MyStruct
    {
        public int x;
        public int y;
        public int Sum()
        {
            return x + y;
        }
    }
}

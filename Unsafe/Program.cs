using System.ComponentModel;
using System.Globalization;

namespace Unsafe
{
    internal class Program
    {
        public static readonly int SIZE = 10;
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

            Console.WriteLine("\n________________________________________Частичные_типы________________________________________\n");
            XY xy = new XY(9, 12);
            Console.WriteLine($"xy.X = {xy.X}; xy.Y = {xy.Y}");

            Console.WriteLine("\n________________________________________Динамический_тип________________________________________\n");
            dynamic str;
            dynamic value;
            str = "this is a string";
            value = 33;
            Console.WriteLine($"Переменная str содержит: {str}");
            Console.WriteLine($"Переменная value содержит: {value}");

            string str2 = str.ToUpper(CultureInfo.CurrentCulture);
            int a = value * 2;
            Console.WriteLine($"Переменная str2 содержит: {str2}");
            Console.WriteLine($"Переменная a содержит: {a}");

            Console.WriteLine("\n________________________________________Разные_ключевые_слова________________________________________\n");
            int[] mass1 = new int[Program.SIZE];
            int[] mass2 = new int[Program.SIZE];
            for (int i = 0; i < mass1.Length; i++)
            {
                mass1[i] = i;
            }
            foreach (int i in mass1)
            {
                Console.Write($"{i}, ");
            }
            Console.WriteLine();
            for (int i = Program.SIZE - 1, j = 0; i > 0; i--, j++)
            {
                mass2[j] = mass1[i];
            }
            foreach (int i in mass2)
            {
                Console.Write($"{i}, ");
            }
            // Program.SIZE = 100; - ERROR!!!
        }
        static double GetZeroBalance()
        {
            Console.WriteLine("В методе GetZeroBalance(): ");
            return 0.0;
        }
        const int i2 = 3;
        volatile int i3 = 11; // можно изменять двумя и более параллельно выполняющимися потоками

        // extern возвращаемый_тип имя_метода(список_аргументов); 
    }
    partial class XY
    {
        public XY(int x, int y)
        {
            X = x;
            Y = y;
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

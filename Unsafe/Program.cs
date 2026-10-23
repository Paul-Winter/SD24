namespace Unsafe
{
    internal class Program
    {
        unsafe static void Main(string[] args)
        {            
            // объявление указателей:
            // тип* имя_переменной;
            int* ip;
            float* fp;
            int* x, y;

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
}

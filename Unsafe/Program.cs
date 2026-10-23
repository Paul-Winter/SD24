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


        }
    }
}

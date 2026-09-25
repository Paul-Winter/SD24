using System.Net.Http.Headers;

namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("====================Создание_строк====================");
            // 1-й способ
            string str1 = "строка в C#";

            // 2-й способ
            char[] charray = { 't', 'e', 'x', 't' };
            string str2 = new string(charray);

            Console.WriteLine($"строки 1 и 2 равны? {String.Compare(str1, str2)}");
            Console.WriteLine(str1[0]);
            Console.WriteLine(str1[10]);
            Console.WriteLine(str2[2]);

            Console.WriteLine("====================Свойство_Length====================");
            string strUp, strLow;
            Console.WriteLine($"Длина строки 1: {str1.Length}");
            Console.WriteLine($"Длина строки 2: {str2.Length}");

            Console.WriteLine("====================Сравнение_строк====================");
            if (str1 == str2)
            {
                Console.WriteLine("str1 == str2");
            }
            else
                Console.WriteLine("str1 != str2");

            strUp = str2.ToUpper();
            strLow = str1.ToLower();
            Console.WriteLine(strUp);
            Console.WriteLine(strLow);

            Console.WriteLine("====================Использование_строк_в_switch====================");
            string[] week = { "sunday", "monday", "tuesday", "wednesday", "thursday", "friday", "saturday" };
            foreach(string str in week)
            {
                switch (str)
                {
                    case "monday": Console.WriteLine("понедельник"); break;
                    case "tuesday": Console.WriteLine("вторник"); break;
                    case "wednesday": Console.WriteLine("среда"); break;
                    case "thursday": Console.WriteLine("четверг"); break;
                    case "friday": Console.WriteLine("пятница"); break;
                    default:
                        Console.WriteLine("выходной");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("====================Сравнение_строк====================");
            string str3 = "alpha";
            string str4 = "Alpha";
            string str5 = "Beta";
            string str6 = "alpha";
            string str7 = "alpha, beta";
            int result;

            result = String.Compare(str3, str4, StringComparison.CurrentCulture);
            Console.Write("Сравнение строк с учётом культурной среды: ");
            if (result < 0)
                Console.Write(str3 + " меньше " + str4);
            else if (result > 0)
                Console.Write(str3 + " больше " + str4);
            else
                Console.Write(str3 + " равно " + str4);
            Console.WriteLine();


            result = String.Compare(str3, str4, StringComparison.Ordinal);
            Console.Write("Порядковое сравнение строк: ");
            if (result < 0)
                Console.Write(str3 + " меньше " + str4);
            else if (result > 0)
                Console.Write(str3 + " больше " + str4);
            else
                Console.Write(str3 + " равно " + str4);
            Console.WriteLine();

            Console.WriteLine("Порядковое сравнение строк с помощью операторов:");
            if (str3 == str6)
                Console.WriteLine(str3 + " == " + str6);
            if (str3 != str5)
                Console.WriteLine(str3 + " != " + str5);
            if (str3 != str4)
                Console.WriteLine(str3 + " != " + str4);
            Console.WriteLine();

            Console.Write("Сравнение строк методом Equals без учёта регистра: ");
            if (String.Equals(str3, str4, StringComparison.OrdinalIgnoreCase))
                Console.Write(str3 + " равно " + str4);
            Console.WriteLine();

            Console.WriteLine("====================Сцепление_строк====================");

        }
    }
}

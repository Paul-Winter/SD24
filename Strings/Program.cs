using System.Net.Http.Headers;

namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n====================Создание_строк====================");
            // 1-й способ
            string str1 = "строка в C#";

            // 2-й способ
            char[] charray = { 't', 'e', 'x', 't' };
            string str2 = new string(charray);

            Console.WriteLine($"строки 1 и 2 равны? {String.Compare(str1, str2)}");
            Console.WriteLine(str1[0]);
            Console.WriteLine(str1[10]);
            Console.WriteLine(str2[2]);

            Console.WriteLine("\n====================Свойство_Length====================");
            string strUp, strLow;
            Console.WriteLine($"Длина строки 1: {str1.Length}");
            Console.WriteLine($"Длина строки 2: {str2.Length}");

            Console.WriteLine("\n====================Сравнение_строк====================");
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

            Console.WriteLine("\n====================Использование_строк_в_switch====================");
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
            Console.WriteLine("\n====================Сравнение_строк====================");
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

            Console.WriteLine("\n====================Сцепление_строк====================");
            string resultStr = String.Concat("значение равно " + 13);
            Console.WriteLine("Результат: " + resultStr);
            resultStr = String.Concat("привет ", 88, " ", 20.0, " ", true, " " + 12.345M);
            Console.WriteLine("Результат: " + resultStr);
            NewClass nc = new NewClass();
            resultStr = String.Concat(nc, " текущий счёт равен ", NewClass.Count);
            Console.WriteLine("Результат: " + resultStr);
            Console.WriteLine();

            Console.WriteLine("\n====================Поиск_в_строке====================");
            string str8 = "С# обладает эффективными средствами обработки строк.";
            int index;
            Console.WriteLine("Строка str8: " + str8);
            index = str8.IndexOf('о');
            Console.WriteLine("Индекс первого вхождения символа 'o': " + index);
            index = str8.LastIndexOf('о');
            Console.WriteLine("Индекс последнего вхождения символа 'o': " + index);
            index = str8.IndexOf("ми");
            Console.WriteLine("Индекс первого вхождения подстроки 'ми': " + index);
            index = str8.LastIndexOf('о');
            Console.WriteLine("Индекс последнего вхождения подстроки 'ми': " + index);
            char[] chars = { 'а', 'б', 'в' };
            index = str8.IndexOfAny(chars);
            Console.WriteLine("Индекс первого вхождения символов 'а','б' или 'в': " + index);
            Console.WriteLine();

            Console.WriteLine("\n====================Разделение_и_соединение_строк====================");
            string str9 = "Ты на суше, я - на море.";
            char[] seps = { ' ', '.', ',' };
            string[] parts = str9.Split(seps);
            Console.WriteLine("Результат разделения строки: ");
            for (int i = 0; i < parts.Length; i++)
            {
                Console.WriteLine(parts[i]);
            }
            string whole = String.Join(" | ", parts);
            Console.WriteLine("Результат соединения частей строки: ");
            Console.WriteLine(whole);
            Console.WriteLine();

            Console.WriteLine("\n====================Обрезка_и_заполнение_строк====================");
            string strTest = "тест";
            Console.WriteLine("Исходная строка: " + strTest);
            strTest = strTest.PadLeft(10);
            Console.WriteLine("|" + strTest + "|");
            strTest = strTest.PadRight(20);
            Console.WriteLine("|" + strTest + "|");
            strTest = strTest.Trim();
            Console.WriteLine("|" + strTest + "|");
            strTest = strTest.PadLeft(10, '#');
            Console.WriteLine("|" + strTest + "|");
            strTest = strTest.PadRight(20, '#');
            Console.WriteLine("|" + strTest + "|");
            strTest = strTest.Trim('#');
            Console.WriteLine("|" + strTest + "|");
            Console.WriteLine();

            Console.WriteLine("\n====================Спецификаторы_числового_формата====================");
            double d1 = 17688.65849;
            double d2 = 0.15;
            int x1 = 21;
            Console.WriteLine("{0:F2}", d1);
            Console.WriteLine("{0:N5}", d1);
            Console.WriteLine("{0:e}", d1);
            Console.WriteLine("{0:r}", d1);
            Console.WriteLine("{0:p}", d2);
            Console.WriteLine("{0:X}", x1);
            Console.WriteLine("{0:D12}", x1);
            Console.WriteLine("{0:C}", 189.99);
            Console.WriteLine("{0:F2} {0:F3} {0:e}", d1);
            Console.WriteLine("{2:d} {0:d} {1:d}", 1, 2, 3);

            Console.WriteLine("\n");
            int summ = 0;
            int prod = 1;
            string str10;
            for (int i = 1; i <= 10; i++)
            {
                summ += i;
                prod *= i;
                str10 = String.Format("Сумма: {0,3:D}\tПроизведение: {1,8:D}", summ, prod);
                Console.WriteLine(str10);
            }
            
            Console.WriteLine("\n");
            Console.WriteLine("{0:00##.#00}", 21.3);
            Console.WriteLine("{0:#,###.#}", 3421.3);

            Console.WriteLine("\n====================Спецификаторы_формата_даты_и_времени====================");
            DateTime dt = DateTime.Now;
            Console.WriteLine("Формат d: {0:d}", dt);
            Console.WriteLine("Формат D: {0:D}", dt);
            Console.WriteLine("Формат t: {0:t}", dt);
            Console.WriteLine("Формат T: {0:T}", dt);
            Console.WriteLine("Формат f: {0:f}", dt);
            Console.WriteLine("Формат F: {0:F}", dt);
            Console.WriteLine("Формат g: {0:g}", dt);
            Console.WriteLine("Формат G: {0:G}", dt);
            Console.WriteLine("Формат m: {0:m}", dt);
            Console.WriteLine("Формат M: {0:M}", dt);
            Console.WriteLine("Формат o: {0:o}", dt);
            Console.WriteLine("Формат O: {0:O}", dt);
            Console.WriteLine("Формат r: {0:r}", dt);
            Console.WriteLine("Формат R: {0:R}", dt);
            Console.WriteLine("Формат s: {0:s}", dt);
            Console.WriteLine("Формат u: {0:u}", dt);
            Console.WriteLine("Формат U: {0:U}", dt);
            Console.WriteLine("Формат y: {0:y}", dt);
            Console.WriteLine("Формат Y: {0:Y}", dt);
            Console.WriteLine("\n");

        }
    }

    class NewClass
    {
        public static int Count = 0;
        public NewClass()
        {
            Count++;
        }
    }
}

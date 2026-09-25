namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1-й способ
            string str1 = "строка в C#";

            // 2-й способ
            char[] charray = { 't', 'e', 'x', 't' };
            string str2 = new string(charray);
        }
    }
}

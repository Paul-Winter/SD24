using Greeter;

namespace Modules
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dialog dialog = new Dialog();
            int result = 0;
            while (true)
            {
                Menu();
                result = Convert.ToInt32(Console.ReadLine());
                if (result == 0)
                {
                    dialog.SayBye();
                    return;
                }
                else if (result == 1)
                {

                }
                else
                {
                    Console.WriteLine("__________НЕВЕРНЫЙ_ВВОД__________");
                }
                dialog.SayHello();
                dialog.GetName();
                dialog.SayBirthday();
                dialog.GetBirthday();
                dialog.SayAge();
            }
        }
        static void Menu()
        {
            Console.WriteLine("0 - ВЫХОД");
            Console.WriteLine("1 - ДАЛЕЕ");
        }
    }
}

